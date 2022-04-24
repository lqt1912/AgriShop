using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AnBinhMarket.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Detail(Guid id, string order, decimal? fromPrice, decimal? toPrice, int? page)
        {

            var category = _context.DanhMucs.Find(id);
            if (category == null)
            {
                return Redirect("/NotFound/Index");
            }

            var products = _context.SanPhams.Include(x=>x.DanhMuc).Where(p => p.MaDanhMuc == id && !p.IsDeleted && p.SoLuong >0).OrderByDescending(p => p.Id);

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
                    case "az":
                        products = products.OrderBy(p => p.TenSP);
                        ViewBag.order = "az";
                        break;
                    case "za":
                        products = products.OrderByDescending(p => p.TenSP);
                        ViewBag.order = "za";
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

            int pageNumber = (page ?? 1);
            ViewBag.Category = category.TenDanhMuc;

            return View(products.ToPagedList(pageNumber, 8));
        }
    }
}
