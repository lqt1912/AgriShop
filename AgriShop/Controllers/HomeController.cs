using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace AnBinhMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            var products = _context.SanPhams.Where(x => !x.IsDeleted && 0 < x.GiaKM && x.GiaKM < x.Gia).OrderByDescending(p => p.Id);
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, 10));
        }

        [HttpGet]
        public IActionResult CancelOrder(Guid MaHD)
        {
            var hd = _context.HoaDons.FirstOrDefault(x => x.Id == MaHD);
            if (hd != null)
            {
                hd.TrangThai = "Đã hủy";
                _context.HoaDons.Update(hd);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Profile", new { Area = "" });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}