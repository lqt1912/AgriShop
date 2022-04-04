using Microsoft.AspNetCore.Mvc;

namespace AgriShop.Areas.Admin.Controllers
{
    public class ManageReceiptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
