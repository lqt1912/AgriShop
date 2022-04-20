using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageReceiptController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageReceiptController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hoaDons = _context.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).Include(x=>x.GioHang).ThenInclude(x=>x.TaiKhoan).Select(h => h);

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
            result = result.OrderByDescending(x => x.NgayTao).ToList();
            return View(result);
        }

        public IActionResult Details(Guid id)
        {
            var hoaDon = _context.HoaDons
                .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham)
                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).FirstOrDefault(x => x.Id == id);

            if (hoaDon == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(hoaDon);
        }
        public IActionResult Edit(Guid id)
        {

            var hoaDon = _context.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x => x.SanPham)
                .Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan).FirstOrDefault(x => x.Id == id);
            if (hoaDon == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(hoaDon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HoaDon hoaDon)
        {
            try
            {
                var _hoaDon = _context.HoaDons.FirstOrDefault(x => x.Id == hoaDon.Id);
                if (_hoaDon != null)
                {
                    _hoaDon.TrangThai = hoaDon.TrangThai;
                    _context.HoaDons.Update(_hoaDon);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
                return View(hoaDon);
            }

        }
    }
}
