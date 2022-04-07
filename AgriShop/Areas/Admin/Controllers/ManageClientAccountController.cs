using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageClientAccountController : CustomController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserStore<ApplicationUser> _userStore;

        public ManageClientAccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _userStore = userStore;
        }

        public IActionResult Index()
        {
            var taiKhoans = _context.Users.Where(t => t.Quyen == 0 && !t.IsDeleted).OrderBy(x=>x.UserName);
            return View(taiKhoans);
        }
        public bool toggleStatus(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                _context.Update(user);
                _context.SaveChanges();
                setAlert("Thay đổi trạng thái thành công!", "success");
                return user.IsActive;
            }
            return false;
        }

        [HttpPost]
        public JsonResult ChangeStatus(Guid id)
        {
            var result = toggleStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public IActionResult Details(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult Edit(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationUser _user)
        {
            var user = _context.Users.Find(_user.Id);
            try
            {
                if (user !=null)
                {
                    user.IsActive = _user.IsActive;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    setAlert("Sửa trạng thái thành công!", "success");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(_user);
            }
        }
    }
}
