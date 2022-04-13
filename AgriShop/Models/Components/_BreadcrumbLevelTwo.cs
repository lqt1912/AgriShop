using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Models.Components
{
    public class _BreadcrumbLevelTwoViewComponent : ViewComponent
    {
        ApplicationDbContext _context;

        public _BreadcrumbLevelTwoViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var product = _context.SanPhams.Include(x=>x.DanhMuc).Where(c => c.Id == id).SingleOrDefault();

            return View(product);
        }
    }
}
