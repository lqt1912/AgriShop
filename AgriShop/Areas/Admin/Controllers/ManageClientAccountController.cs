using Microsoft.AspNetCore.Mvc;

namespace AgriShop.Areas.Admin.Controllers
{
    public class ManageClientAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
