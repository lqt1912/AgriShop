﻿using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    public class ManageNewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
