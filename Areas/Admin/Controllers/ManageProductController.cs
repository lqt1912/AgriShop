using Microsoft.AspNetCore.Mvc;

namespace AgriShop.Areas.Admin.Controllers
{
    public class ManageProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
