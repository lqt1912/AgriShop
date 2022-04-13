using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AnBinhMarket.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            var tinTucs = _context.TinTucs.Include(x=>x.TaiKhoan).Select(t => t);
            tinTucs = tinTucs.OrderByDescending(t => t.NgayCapNhat);
            int pageNumber = (page ?? 1);

            return View(tinTucs.ToPagedList(pageNumber, 5));
        }

        public IActionResult Details(Guid id)
        {
          
            var tinTuc = _context.TinTucs.Include(X=>X.TaiKhoan).FirstOrDefault(x=>x.Id ==id);
            if (tinTuc == null)
            {
                return NotFound();
            }
            return View(tinTuc);
        }
    }
}
