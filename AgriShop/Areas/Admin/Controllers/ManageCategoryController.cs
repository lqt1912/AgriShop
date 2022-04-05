using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    public class ManageCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
