using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
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
            var hoaDons = _context.HoaDons.Include(x=>x.GioHang).ThenInclude(x=>x.TaiKhoan).Select(h => h);
            hoaDons = hoaDons.OrderByDescending(s => s.NgayTao);
            return View(hoaDons.ToList());
        }

        public IActionResult Details(Guid id)
        {
            var hoaDon = _context.HoaDons
                .Include(x=>x.GioHang).ThenInclude(x=>x.ChiTietGioHangs).ThenInclude(x=>x.SanPham)
                .Include(x=>x.GioHang).ThenInclude(x=>x.TaiKhoan).FirstOrDefault(x=>x.Id == id);

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
                var _hoaDon = _context.HoaDons.FirstOrDefault(x=>x.Id ==hoaDon.Id);
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
