using AnBinhMarket.Data;
using AnBinhMarket.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Models.Components
{
    public class _HeaderViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public _HeaderViewComponent(ApplicationDbContext context)
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
            return View();
        
        }
    }
}
