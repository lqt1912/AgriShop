using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Models.Components
{
    public class _OrderViewViewComponent: ViewComponent
    {
        ApplicationDbContext _context;

        public _OrderViewViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid TenTK)
        {
            var carts = _context.GioHangs.Where(g => g.TenTaiKhoan.Equals(TenTK)).ToList();
            var hds = _context.HoaDons;
            var _receipts = carts.Join(hds, x => x.Id, y => y.MaGioHang, (x, y) => y);
            return View(_receipts);
        }

    }
}
