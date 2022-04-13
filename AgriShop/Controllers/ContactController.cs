using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Controllers
{
    public class ContactController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Send(string name, string phone, string email, string address, string content)
        {
            var feedback = new PhanHoi();
            feedback.Ten = name;
            feedback.SoDT = phone;
            feedback.Email = email;
            feedback.NgayTao = DateTime.Now;
            feedback.DiaChi = address;
            feedback.NoiDung = content;

            var id = new ContactDao(_context).InsertFeedBack(feedback);
            if (id !=Guid.Empty)
                return Json(new
                {
                    status = true
                });
            else
                return Json(new
                {
                    status = false
                });
        }
    }
}
