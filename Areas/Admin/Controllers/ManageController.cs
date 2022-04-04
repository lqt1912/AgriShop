using Microsoft.AspNetCore.Mvc;

namespace AgriShop.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
