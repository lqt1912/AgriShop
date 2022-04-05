using AnBinhMarket.Areas.Admin.Models;
using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserStore<ApplicationUser> _userStore;


        public ManageAdminController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        public IActionResult Index()
        {   

            var user = _userManager.GetUserName(User);
            ViewBag.CurrentUserName = user;
            var taiKhoans = _context.Users.ToList().OrderBy(x=>x.UserName);
            return View(taiKhoans.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoan taiKhoan)
        {
            var a = taiKhoan;

            var user = CreateUser();
            user.CreatedDate = DateTime.Now;
            user.Email = a.Email;
            user.Quyen = a.Quyen;
            user.PhoneNumber = a.SoDienThoai;
            user.UserName = a.TenTaiKhoan;
            user.PhoneNumberConfirmed = true;
            user.IsActive = a.TrangThai;
            user.HoTen = a.HoTen;
            user.DiaChi = a.DiaChi;

            await _userStore.SetUserNameAsync(user, taiKhoan.TenTaiKhoan, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, taiKhoan.MatKhau);
            setAlert("Thêm mới tài khoản thành công!", "success");
            return RedirectToAction("Index");

            ///*taiKhoan.Quyen = 1;*/
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.TaiKhoans.Add(taiKhoan);
            //        db.SaveChanges();
            //    }
            //    setAlert("Thêm mới tài khoản thành công!", "success");
            //    return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
            //    return View(taiKhoan);
            //}
            return RedirectToAction("Index");
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

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
