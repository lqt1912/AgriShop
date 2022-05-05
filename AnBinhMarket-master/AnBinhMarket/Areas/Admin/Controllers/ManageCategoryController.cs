using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using AnBinhMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManageCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var danhmucs = _context.DanhMucs.Where(dm => !dm.IsDeleted).Include(x => x.SanPhams).OrderByDescending(dm => dm.Id).ToList();
            var result = new List<DanhMucViewModel>();
            foreach (var item in danhmucs)
            {
                var _a = new DanhMucViewModel()
                {
                    TenDanhMuc = item.TenDanhMuc,
                    Id = item.Id,
                    NgayCapNhat = item.NgayCapNhat,
                    SoLuongSanPham = item.SanPhams.Count(),
                };
                result.Add(_a);
            }

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Detail(Guid id)
        {
            var danhmuc = _context.DanhMucs.FirstOrDefault(x => x.Id == id);
            if (danhmuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });
            }
            ViewBag.TenDanhMuc = danhmuc.TenDanhMuc;
            var sanphams = _context.SanPhams.Where(x => !x.IsDeleted && x.MaDanhMuc == id).ToList();
            return View(sanphams);

        }
        // POST: Admin/ManageCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DanhMuc danhMuc)
        {
            try
            {
                var danhmuc = new DanhMuc()
                {
                    TenDanhMuc = danhMuc.TenDanhMuc
                };
                _context.DanhMucs.Add(danhmuc);
                _context.SaveChanges();
                setAlert("Thêm mới danh mục thành công!", "success");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(danhMuc);

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

        public IActionResult Edit(Guid id)
        {

            var danhMuc = _context.DanhMucs.Find(id);

            if (danhMuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });
            }
            return View(danhMuc);
        }

        // POST: Admin/ManageCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DanhMuc danhMuc)
        {
            try
            {
                var _danhMuc = _context.DanhMucs.Find(danhMuc.Id);
                if (_danhMuc != null)
                {
                    _danhMuc.NgayCapNhat = DateTime.Now;
                    _danhMuc.TenDanhMuc = danhMuc.TenDanhMuc;
                    _context.Update(_danhMuc);
                    _context.SaveChanges();
                }
                setAlert("Sửa danh mục thành công!", "success");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!" + ex.Message;
                return View(danhMuc);
            }

        }

        public ActionResult Delete(Guid id)
        {
            var danhMuc = _context.DanhMucs.Find(id);

            if (danhMuc == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(danhMuc);
        }

        // POST: Admin/ManageCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var danhMuc = _context.DanhMucs.Find(id);
            try
            {
                if (danhMuc != null)
                {
                    danhMuc.IsDeleted = true;
                    _context.Update(danhMuc);
                    _context.SaveChanges();
                }
                setAlert("Xoá danh mục thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!" + ex.Message;
                return View(danhMuc);
            }

        }
    }
}
