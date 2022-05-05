using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult DetailReceipt(Guid MaHD)
        {
            var hd = _context.HoaDons.Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan)
                .Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).ThenInclude(x=>x.SanPham)
                .FirstOrDefault(x => x.Id == MaHD);
            return View(hd);
        }
    }
}
