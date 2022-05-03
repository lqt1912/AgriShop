using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var currentUser = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            return View(currentUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(IFormCollection frm)
        {

            //id
            string tenTaiKhoan = frm["TenTaiKhoan"];

            string hoTen = frm["HoTen"];

            string soDienThoai = frm["PhoneNumber"];
            string Email = frm["Email"];
            string diaChi = frm["DiaChi"];
            var user = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(tenTaiKhoan));
            if (user != null)
            {
                user.HoTen = hoTen;
                user.PhoneNumber = soDienThoai;
                user.DiaChi = diaChi;
                user.Email = Email;
                user.NormalizedEmail = Email.ToUpper();
               
                _context.Users.Update(user);
                _context.SaveChanges();
                ViewBag.Information = "Cập nhật thành công";
            }
            else
            {
                ViewBag.Information = "Có lỗi xảy ra khi cập nhật";
            }

            return View("Index", user);
        }
    }
}
