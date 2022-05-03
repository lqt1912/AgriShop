using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Models.Components
{
    public class _MenuPCViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public _MenuPCViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = _context.DanhMucs.Where(x => !x.IsDeleted).ToList();
            return View(categories);
        }
    }
}

