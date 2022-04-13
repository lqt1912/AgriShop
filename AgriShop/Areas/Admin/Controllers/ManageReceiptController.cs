using AnBinhMarket.Data;
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
                return NotFound();
            }
            return View(hoaDon);
        }
    }
}
