using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
