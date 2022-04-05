using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    public class ManageProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
