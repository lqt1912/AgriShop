using AnBinhMarket.Areas.Admin.Models;
using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ManageAdminController : CustomController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserStore<ApplicationUser> _userStore;


        public ManageAdminController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore
            )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserName(User);
            ViewBag.CurrentUserName = user;
            var taiKhoans = _context.Users.Where(x => (x.Quyen == 1 || x.Quyen == 2) && !x.IsDeleted).ToList().OrderBy(x => x.UserName);
            return View(taiKhoans.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _context.Roles.ToList();
            var selectListItems = roles.Select(x => new SelectListItem()
            {
                Text = x.NormalizedName,
                Value = x.Id.ToString()
            }).ToList();

            var selectList = new SelectList(roles, "Id", "NormalizedName");
            ViewBag.RoleList = selectList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiKhoan taiKhoan)
        {
            try
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

                switch (a.Quyen)
                {
                    case 1:
                        {
                            _context.UserRoles.Add(new IdentityUserRole<Guid>
                            {
                                RoleId = Guid.Parse("745599de-2faf-4989-b33b-9b5e0905c07d"),
                                UserId = user.Id
                            });
                            _context.SaveChanges();
                            break;
                        }
                    case 2:
                        {
                            _context.UserRoles.Add(new IdentityUserRole<Guid>
                            {
                                RoleId = Guid.Parse("745599de-2faf-4989-b33b-9b5e0905c07e"),
                                UserId = user.Id
                            });
                            _context.SaveChanges();
                            break;
                        }
                    default:
                        break;
                }
                setAlert("Thêm mới tài khoản thành công!", "success");
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu! " + ex.Message;
                return View(taiKhoan);
            }
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

        public IActionResult Details(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        public IActionResult Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    user.IsDeleted = true;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
                setAlert("Xoá tài khoản thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Xóa thất bại" + ex.Message;
                return View();
                //throw;
            }
        }
    }
}
