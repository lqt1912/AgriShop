using Microsoft.AspNetCore.Mvc;

namespace AgriShop.Areas.Admin.Controllers
{
    public class ManageNewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
