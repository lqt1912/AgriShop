using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
