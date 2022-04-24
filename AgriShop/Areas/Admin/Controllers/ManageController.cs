using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        ApplicationDbContext db;

        public ManageController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Manage
        [Authorize]
        public IActionResult Index()
        {

            var products = db.SanPhams.Where(x=>!x.IsDeleted).ToList();
            var categories = db.DanhMucs.Where(x => !x.IsDeleted).ToList();
            var receipts = db.HoaDons.Include(x=>x.GioHang).ThenInclude(x=>x.ChiTietGioHangs).Where(x => !x.IsDeleted).ToList();
            var trademarks = db.ThuongHieux.Where(x => !x.IsDeleted).ToList();
            var news = db.TinTucs.Where(x => !x.IsDDeleted).ToList();
            var feedbacks = db.PhanHois.ToList();
            DateTime today = DateTime.Today;

            List<HoaDon> hd = db.HoaDons.Where(h => h.TrangThai.Equals("Đã giao") && !h.IsDeleted).ToList();
            List<HoaDon> hds = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).Where(h => h.NgayTao.Month == today.Month &&
                        h.NgayTao.Year == today.Year && h.TrangThai.Equals("Đã giao") && !h.IsDeleted).ToList();

            decimal tongTienNum = 0;
            foreach (var item in hds)
            {
                tongTienNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuong * c.Gia).Sum();
            }

            List<HoaDon> hd_trong_nam = receipts.Where(h => h.NgayTao.Year == today.Year
                    && h.TrangThai.Equals("Đã giao")).ToList();
            decimal tongTienTrongNamNum = 0;
            foreach (var item in hd_trong_nam)
            {
                tongTienTrongNamNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuong * c.Gia).Sum();
            }

            var accounts = db.Users.Where(t => t.Quyen == 0 && !t.IsDeleted).ToList();

            ViewBag.soLuongSp = products.Count;
            ViewBag.soLuongDm = categories.Count;
            ViewBag.soLuongDh = hds.Count;
            ViewBag.soLuongTk = accounts.Count;
            ViewBag.soLuongTT = news.Count;
            ViewBag.soLuongTH = trademarks.Count;
            ViewBag.soLuongPH = feedbacks.Count;
            ViewBag.soHD = hd.Count;
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string tongTien = string.Format("{0:0,000}", tongTienNum);
            string tongTienTrongNam = string.Format("{0:0,000}", tongTienTrongNamNum);
            ViewBag.tongTienThangNay = tongTien;
            ViewBag.tongTienTrongNam = tongTienTrongNam;
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }


        public IActionResult ProductDetails(Guid id)
        {
            var category = db.DanhMucs.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (category == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = db.SanPhams.Where(x=>!x.IsDeleted).Include(x => x.DanhMuc).Where(p => p.MaDanhMuc == id).OrderByDescending(p => p.Id);


            ViewBag.Category = category.TenDanhMuc;

            return View(products.ToList());
        }

        public IActionResult AccountAdmin()
        {
            // select only admin account
            var taiKhoans = db.Users.Where(t => t.Quyen == 0 && !t.IsDeleted).Select(t => t);
            taiKhoans = taiKhoans.OrderBy(t => t.UserName);
            return View(taiKhoans);
        }
        public IActionResult AdminAccountDetail(Guid id)
        {

            var user = db.Users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (user == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(user);
        }

        public IActionResult SPBanChayNhat()
        {
            return View();
        }

        public IActionResult News()
        {
            var tinTucs = db.TinTucs.Where(x=>!x.IsDDeleted).Select(t => t);
            tinTucs = db.TinTucs.OrderByDescending(t => t.NgayTao);
            return View(tinTucs.ToList());
        }

        public IActionResult NewDetails(Guid id)
        {

            var tinTuc = db.TinTucs.Include(x => x.TaiKhoan).FirstOrDefault(x => x.Id == id && !x.IsDDeleted);

            if (tinTuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            tinTuc.MoTaChiTiet = HttpUtility.HtmlDecode(tinTuc.MoTaChiTiet);

            return View(tinTuc);
        }

        public IActionResult TradeMarks()
        {
            var thuongHieus = db.ThuongHieux.Where(x=>!x.IsDeleted).OrderByDescending(th => th.Id).ToList();
            return View(thuongHieus);
        }

        public IActionResult TradeMarksDetails(Guid id)
        {
            var trademark = db.ThuongHieux.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (trademark == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = db.SanPhams.Include(x => x.ThuongHieu).Where(p => p.MaTH == id).OrderByDescending(p => p.MaSP);


            ViewBag.TradeMark = trademark.TenHuongHieu;

            return View(products.ToList());
        }

        public IActionResult Feedback()
        {
            return View(db.PhanHois.ToList());
        }

        public IActionResult FeedbackDetails(Guid id)
        {
            var phanHoi = db.PhanHois.FirstOrDefault(x => x.Id == id);

            if (phanHoi == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(phanHoi);
        }

        public IActionResult DonHang()
        {
            var hoaDons = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs)
                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).Where(h => h.TrangThai.Equals("Đã giao") && !h.IsDeleted).ToList();


            var result = new List<HoaDonViewModel>();
            foreach (var hoaDon in hoaDons)
            {
                decimal thanhTien = 0;

                foreach (var ctgh in hoaDon.GioHang.ChiTietGioHangs)
                {
                    thanhTien += ctgh.Gia * ctgh.SoLuong;
                }
                result.Add(new HoaDonViewModel()
                {
                    ChuY = hoaDon.ChuY,
                    DiaChi = hoaDon.DiaChi,
                    MaHoaDon = hoaDon.MaHoaDon,
                    NgayCapNhat = hoaDon.NgayCapNhat,
                    NgayTao = hoaDon.NgayTao,
                    ThanhTien = thanhTien + hoaDon.PhiShip,
                    TrangThai = hoaDon.TrangThai,
                    Id = hoaDon.Id,
                    HoTen = hoaDon.GioHang.TaiKhoan.HoTen
                });
            }
            
            return View(result);
        }

        // GET: Admin/ManageReceipt/Details/5
        public IActionResult ChiTietDonHang(Guid id)
        {

            var hoaDon = db.HoaDons

                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham)
               .FirstOrDefault(x => x.Id == id &&!x.IsDeleted);
            if (hoaDon == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(hoaDon);
        }

        public IActionResult ThongKeTheoThang()
        {
            //GioHang.TaiKhoan
            DateTime today = DateTime.Today;
            var hds = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).Where(h => h.NgayTao.Month == today.Month &&
                        h.NgayTao.Year == today.Year && h.TrangThai.Equals("Đã giao") && !h.IsDeleted).ToList();

            var result = new List<HoaDonViewModel>();
            foreach (var hoaDon in hds)
            {
                decimal thanhTien = 0;

                foreach (var ctgh in hoaDon.GioHang.ChiTietGioHangs)
                {
                    thanhTien += ctgh.Gia * ctgh.SoLuong;
                }
                result.Add(new HoaDonViewModel()
                {
                    ChuY = hoaDon.ChuY,
                    DiaChi = hoaDon.DiaChi,
                    MaHoaDon = hoaDon.MaHoaDon,
                    NgayCapNhat = hoaDon.NgayCapNhat,
                    NgayTao = hoaDon.NgayTao,
                    ThanhTien = thanhTien + hoaDon.PhiShip,
                    TrangThai = hoaDon.TrangThai,
                    Id = hoaDon.Id,
                    HoTen = hoaDon.GioHang.TaiKhoan.HoTen
                });
            }

            return View(result);
        }
        public IActionResult ChiTietDonTrongThang(Guid id)
        {
            var hoaDon = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (hoaDon == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(hoaDon);
        }


        public IActionResult ThongKeTrongNam(DateTime? fromDate, DateTime? toDate)
        {
            DateTime today = DateTime.Today;
            var receipts = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham).Where(x => !x.IsDeleted);
            var hd_trong_nam = receipts.Where(h => h.NgayTao.Year == today.Year
                   && h.TrangThai.Equals("Đã giao") && !h.IsDeleted).ToList();
            if(fromDate.HasValue && toDate.HasValue)
            {
                hd_trong_nam = receipts.Where(x=>x.NgayTao >= fromDate && x.NgayTao <= toDate).ToList();    
            }
            var result = new List<HoaDonViewModel>();
            foreach (var hoaDon in hd_trong_nam)
            {
                decimal thanhTien = 0;

                foreach (var ctgh in hoaDon.GioHang.ChiTietGioHangs)
                {
                    thanhTien += ctgh.Gia * ctgh.SoLuong;
                }
                result.Add(new HoaDonViewModel()
                {
                    ChuY = hoaDon.ChuY,
                    DiaChi = hoaDon.DiaChi,
                    MaHoaDon = hoaDon.MaHoaDon,
                    NgayCapNhat = hoaDon.NgayCapNhat,
                    NgayTao = hoaDon.NgayTao,
                    ThanhTien = thanhTien + hoaDon.PhiShip,
                    TrangThai = hoaDon.TrangThai,
                    Id = hoaDon.Id,
                    HoTen = hoaDon.GioHang.TaiKhoan.HoTen
                });
            }

            return View(result);
        }
        public IActionResult DonHangTrongNam(Guid id)
        {
            var hoaDon = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham).FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (hoaDon == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(hoaDon);
        }
    }
}
