using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
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

            var products = db.SanPhams.ToList();
            var categories = db.DanhMucs.ToList();
            var receipts = db.HoaDons.ToList();
            var trademarks = db.ThuongHieux.ToList();
            var news = db.TinTucs.ToList();
            var feedbacks = db.PhanHois.ToList();
            DateTime today = DateTime.Today;

            List<HoaDon> hd = db.HoaDons.Where(h => h.TrangThai.Equals("Đã giao")).ToList();
            List<HoaDon> hds = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).Where(h => h.NgayTao.Month == today.Month &&
                        h.NgayTao.Year == today.Year && h.TrangThai.Equals("Đã giao")).ToList();

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

            var accounts = db.Users.Where(t => t.Quyen == 0).ToList();

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
            var category = db.DanhMucs.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = db.SanPhams.Include(x => x.DanhMuc).Where(p => p.MaDanhMuc == id).OrderByDescending(p => p.Id);


            ViewBag.Category = category.TenDanhMuc;

            return View(products.ToList());
        }

        public IActionResult AccountAdmin()
        {
            // select only admin account
            var taiKhoans = db.Users.Where(t => t.Quyen == 0).Select(t => t);
            taiKhoans = taiKhoans.OrderBy(t => t.UserName);
            return View(taiKhoans);
        }
        public IActionResult AdminAccountDetail(Guid id)
        {

            var user = db.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult SPBanChayNhat()
        {
            return View();
        }

        public IActionResult News()
        {
            var tinTucs = db.TinTucs.Select(t => t);
            tinTucs = db.TinTucs.OrderByDescending(t => t.NgayTao);
            return View(tinTucs.ToList());
        }

        public IActionResult NewDetails(Guid id)
        {

            var tinTuc = db.TinTucs.Include(x => x.TaiKhoan).FirstOrDefault(x => x.Id == id);

            if (tinTuc == null)
            {
                return NotFound();
            }
            tinTuc.MoTaChiTiet = HttpUtility.HtmlDecode(tinTuc.MoTaChiTiet);

            return View(tinTuc);
        }

        public IActionResult TradeMarks()
        {
            var thuongHieus = db.ThuongHieux.OrderByDescending(th => th.Id).ToList();
            return View(thuongHieus);
        }

        public IActionResult TradeMarksDetails(Guid id)
        {
            var trademark = db.ThuongHieux.FirstOrDefault(x => x.Id == id);

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
                return NotFound();
            }
            return View(phanHoi);
        }

        public IActionResult DonHang()
        {
            var hoaDons = db.HoaDons
                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).Where(h => h.TrangThai.Equals("Đã giao")).ToList();
            return View(hoaDons);
        }

        // GET: Admin/ManageReceipt/Details/5
        public IActionResult ChiTietDonHang(Guid id)
        {

            var hoaDon = db.HoaDons

                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham)
               .FirstOrDefault(x => x.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return View(hoaDon);
        }

        public IActionResult ThongKeTheoThang()
        {
            //GioHang.TaiKhoan
            DateTime today = DateTime.Today;
            var hds = db.HoaDons.Include(x=>x.GioHang).ThenInclude(x=>x.TaiKhoan).Where(h => h.NgayTao.Month == today.Month &&
                    h.NgayTao.Year == today.Year && h.TrangThai.Equals("Đã giao")).ToList();
            return View(hds);
        }
        public IActionResult ChiTietDonTrongThang(Guid id)
        {
            var hoaDon = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham).FirstOrDefault(x => x.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return View(hoaDon);
        }


        public IActionResult ThongKeTrongNam()
        {
            DateTime today = DateTime.Today;
            var receipts = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).ToList();
            var hd_trong_nam = receipts.Where(h => h.NgayTao.Year == today.Year
                   && h.TrangThai.Equals("Đã giao")).ToList();
            return View(hd_trong_nam);
        }
        public IActionResult DonHangTrongNam(Guid id)
        {
            var hoaDon = db.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
               .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham).FirstOrDefault(x => x.Id == id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            return View(hoaDon);
        }
    }
}
