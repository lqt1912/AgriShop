
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    public class CustomController : Controller
    {
        protected void setAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

    }
}
