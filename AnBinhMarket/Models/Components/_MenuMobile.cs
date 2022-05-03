using AnBinhMarket.Data;
using AnBinhMarket.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Models.Components
{
    public class _MenuMobileViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public _MenuMobileViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carItem = HttpContext.Session.Get<List<CartItem>>("Cartsession");

            if (carItem != null)
            {
                ViewBag.cartpronumber = carItem.Count;
            }
            else
            {
                ViewBag.cartpronumber = 0;
            }

            var categories = _context.DanhMucs.Where(x => !x.IsDeleted).ToList();
            return View(categories);
        }
    }
}
