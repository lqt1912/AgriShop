using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageNewsController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageNewsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var tinTucs = _context.TinTucs.Where(x => !x.IsDDeleted);
            tinTucs = tinTucs.OrderByDescending(t => t.NgayTao);
            return View(tinTucs.ToList());
        }

        public ActionResult Create()
        {
            var tenTK = _context.Users.Where(t => t.Quyen == 1 && t.IsActive == true).ToList();
            ViewBag.TenTaiKhoan = new SelectList(tenTK, "Id", "UserName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TinTuc tinTuc)
        {
            try
            {
                var _tinTuc = new TinTuc()
                {
                    MoTaChiTiet = tinTuc.MoTaChiTiet,
                    MoTaNgan = tinTuc.MoTaNgan,
                    NgayCapNhat = DateTime.Now,
                    NgayTao = DateTime.Now,
                    TieuDe = tinTuc.TieuDe,
                    TenTaiKhoan = Guid.Parse(_userManager.GetUserId(User))
                };
                tinTuc.HinhAnh = "";
                var fileArray = Request.Form.Files;
                if (fileArray != null)
                {
                    var f = fileArray[0];
                    if (f != null && f.Length > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string contentRootPath = _webHostEnvironment.ContentRootPath;

                        string path = "";
                        path = Path.Combine(webRootPath, "news");
                        string UploadPath = path + '\\' + FileName;

                        using (Stream fileStream = new FileStream(UploadPath, FileMode.Create))
                        {
                            await f.CopyToAsync(fileStream);
                        }
                        tinTuc.HinhAnh = FileName;
                        _tinTuc.HinhAnh = FileName;
                    }
                }

                _context.TinTucs.Add(_tinTuc);
                _context.SaveChanges();
                setAlert("Thêm mới tin tức thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //ViewBag.TenTaiKhoan = new SelectList(db.TaiKhoans, "TenTaiKhoan", "MatKhau", tinTuc.TenTaiKhoan);

                ViewBag.Error = "Dữ liệu không hợp lệ" + ex.Message;
                return View(tinTuc);
            }
        }

        public IActionResult Details(Guid id)
        {


            var tinTuc = _context.TinTucs.Find(id);
            if (tinTuc != null)
            {
                ViewBag.NguoiViet = _context.Users.Find(tinTuc.TenTaiKhoan);

            }
            if (tinTuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(tinTuc);
        }
        public IActionResult Edit(Guid id)
        {
            var tinTucs = _context.TinTucs.Include(x => x.TaiKhoan).ToList();

            var tinTuc = tinTucs.FirstOrDefault(x => x.Id == id && !x.IsDDeleted);

            if (tinTuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            tinTuc.MoTaChiTiet = HttpUtility.HtmlDecode(tinTuc.MoTaChiTiet);

            return View(tinTuc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TinTuc tinTuc)
        {
            try
            {
                var _tinTuc = _context.TinTucs.FirstOrDefault(x => x.Id == tinTuc.Id && !x.IsDDeleted);

                if (_tinTuc != null)
                {
                    _tinTuc.MoTaChiTiet = tinTuc.MoTaChiTiet;
                    _tinTuc.MoTaNgan = tinTuc.MoTaNgan;
                    _tinTuc.NgayCapNhat = DateTime.Now;
                    _tinTuc.NgayTao = tinTuc.NgayTao;
                    _tinTuc.TieuDe = tinTuc.TieuDe;
                    _tinTuc.TenTaiKhoan = tinTuc.TenTaiKhoan;
                    var fileArray = Request.Form.Files;
                    if (fileArray.Count > 0)
                    {
                        var f = fileArray[0];
                        if (f != null && f.Length > 0)
                        {
                            string FileName = System.IO.Path.GetFileName(f.FileName);
                            string webRootPath = _webHostEnvironment.WebRootPath;
                            string contentRootPath = _webHostEnvironment.ContentRootPath;

                            string path = "";
                            path = Path.Combine(webRootPath, "news");
                            string UploadPath = path + '\\' + FileName;

                            using (Stream fileStream = new FileStream(UploadPath, FileMode.Create))
                            {
                                await f.CopyToAsync(fileStream);
                            }
                            _tinTuc.HinhAnh = FileName;
                        }
                    }
                    _context.TinTucs.Update(_tinTuc);
                    _context.SaveChanges();
                }

               
                setAlert("Cập nhật tin tức thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!" + ex.Message;
                return View(tinTuc);
            }
        }
        public IActionResult Delete(Guid id)
        {

            var tinTuc = _context.TinTucs.Find(id);
            if (tinTuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            ViewBag.NguoiViet = _context.Users.Find(tinTuc.TenTaiKhoan);
            return View(tinTuc);
        }

        // POST: Admin/TinTucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var tinTuc = _context.TinTucs.Find(id);
                if(tinTuc != null)
                {
                    tinTuc.IsDDeleted = true;
                    _context.Update(tinTuc);
                    _context.SaveChanges();
                }
                setAlert("Xoá tin tức thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Xoá thất bại" + ex.Message;
                return View();
            }

        }
    }
}
