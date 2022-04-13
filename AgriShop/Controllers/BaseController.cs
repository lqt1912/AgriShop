using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Extensions;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IActionResult Index()
        {
            var a = HttpContext.Session.Get<string>("AAA");
            HttpContext.Session.Set<string>("Key", "value");

            return View();
        }

        [AllowAnonymous]
        public PartialViewResult _Header()
        {
            var carItem = HttpContext.Session.Get<List<CartItem>>("Cartsession") ;

            if (carItem != null)
            {
                ViewBag.cartpronumber = carItem.Count;
            }
            else
            {
                ViewBag.cartpronumber = 0;
            }
            return PartialView();
        }


        [AllowAnonymous]
        public PartialViewResult _MenuPC()
        {
            var categories = _context.DanhMucs.Where(x=>!x.IsDeleted).ToList();
            return PartialView(categories);
        }

        [AllowAnonymous]
        public PartialViewResult _MenuMobile()
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
            var categories = _context.DanhMucs.Where(x=>!x.IsDeleted).ToList();
            return PartialView(categories);
        }

        [AllowAnonymous]
        public PartialViewResult _BreadcrumbLevelOne(Guid id)
        {
            var category =_context.DanhMucs.FirstOrDefault(c => c.Id == id);
            return PartialView(category);
        }


        [AllowAnonymous]
        public PartialViewResult _BreadcrumbLevelTwo(Guid id)
        {
            var product = _context.SanPhams.Where(c => c.Id == id).FirstOrDefault();

            return PartialView(product);
        }


        public PartialViewResult _OrderView(Guid TenTK)
        {
            var carts = _context.GioHangs.Where(g => g.TenTaiKhoan.Equals(TenTK)).ToList();
            var hds = _context.HoaDons;
            var _receipts = carts.Join(hds, x => x.Id, y => y.MaGioHang, (x, y) => y);
            return PartialView(_receipts);
        }
    }
}
