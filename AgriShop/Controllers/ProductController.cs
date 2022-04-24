using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AnBinhMarket.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Detail(Guid id)
        {

            var product = _context.SanPhams.Include(x=>x.DanhMuc).Include(x=>x.ThuongHieu).Where(x=>x.Id ==id).FirstOrDefault();

            if (product == null)
            {
                return Redirect("/NotFound/Index");
            }

            return View(product);
        }
        public ActionResult Search(string keyword, string order, decimal? fromPrice, decimal? toPrice, string category, int page = 1)
        {
            if (keyword == null)
            {
                keyword = "";
            }
            var products = _context.SanPhams.Where(p => p.TenSP.Contains(keyword) && !p.IsDeleted && p.SoLuong >0) .OrderByDescending(p => p.Id);

            if (order != null)
                switch (order)
                {
                    case "desc":
                        products = products.OrderByDescending(p => p.Gia);
                        ViewBag.order = "desc";
                        break;
                    case "asc":
                        products = products.OrderBy(p => p.Gia);
                        ViewBag.order = "asc";
                        break;
                    default:
                        ViewBag.order = "default";
                        break;
                }

            if (fromPrice != null)
            {
                ViewBag.from = fromPrice;
                products = (IOrderedQueryable<SanPham>)products.Where(p => p.Gia >= fromPrice);
            }

            if (toPrice != null)
            {
                ViewBag.to = toPrice;
                products = (IOrderedQueryable<SanPham>)products.Where(p => p.Gia <= toPrice);
            }

            if (category != null)
            {
                string[] ids = category.Split(',');
                ViewBag.category = category;
                products = (IOrderedQueryable<SanPham>)products.Where(p => ids.Contains(p.MaDanhMuc.ToString()));
            }

            ViewBag.keyword = keyword;
            ViewBag.Categories = _context.DanhMucs.OrderBy(c => c.MaDanhMuc).ToList();
            return View(products.ToPagedList(page, 12));
        }
        public JsonResult Index(Guid id)
        {

            var product = _context.SanPhams.Find(id);
            if (product == null)
            {
                return Json("NOT_FOUND");
            }
            return Json(
                new
                {
                    id = product.Id,
                    name = product.TenSP,
                    price = product.Gia,
                    quantity = product.SoLuong,
                    image = product.HinhAnh
                });
        }
        public JsonResult List(string ids)
        {
            if (ids == null)
            {
                return Json("NOT_FOUND");
            }
            string[] listId = ids.Split(',');
            var product = _context.SanPhams.Where(p => listId.Contains(p.Id.ToString())).ToList();
            if (product == null)
            {
                return Json("NOT_FOUND");
            }

            List<object> list = new List<object>();

            foreach (var item in product)
            {
                list.Add(new
                {
                    id = item.Id,
                    name = item.TenSP,
                    image = item.HinhAnh,
                    price = item.Gia,
                    quantity = item.SoLuong
                });
            }

            return Json(
                list);
        }
    }
}
