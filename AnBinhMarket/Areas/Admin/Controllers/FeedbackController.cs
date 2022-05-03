using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : CustomController
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.PhanHois.ToList();
            return View(data);
        }

        public IActionResult Detail(Guid id)
        {
            var phanHoi = _context.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(phanHoi);
        }
    }
}
