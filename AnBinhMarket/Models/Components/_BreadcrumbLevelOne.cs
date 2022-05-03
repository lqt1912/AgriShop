using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Models.Components
{
    public class _BreadcrumbLevelOneViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public _BreadcrumbLevelOneViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id) 
        {
            var category = _context.DanhMucs.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
    }
}
