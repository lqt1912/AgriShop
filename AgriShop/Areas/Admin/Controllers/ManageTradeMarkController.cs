using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AnBinhMarket.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ManageTradeMarkController : CustomController
    {
        private readonly ApplicationDbContext _context;

        public ManageTradeMarkController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var thuongHieus = _context.ThuongHieux.Where(x=>!x.IsDeleted).OrderByDescending(th => th.Id).ToList();
            return View(thuongHieus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ThuongHieu thuongHieu)
        {
            try
            {
                var _thuongHieu = new ThuongHieu()
                {
                    TenHuongHieu = thuongHieu.TenHuongHieu
                };
                _context.ThuongHieux.Add(_thuongHieu);
                _context.SaveChanges();
                setAlert("Thêm mới thương hiệu thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!" + ex.Message;
                return View(thuongHieu);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var thuongHieu = _context.ThuongHieux.Find(id);

            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ThuongHieu thuongHieu)
        {
            try
            {
                var _thuongHieu = _context.ThuongHieux.Find(thuongHieu.Id);
                if (_thuongHieu != null)
                {
                    _thuongHieu.TenHuongHieu = thuongHieu.TenHuongHieu;
                    _thuongHieu.NgayCapNhat = DateTime.Now;
                    _context.ThuongHieux.Update(_thuongHieu);
                    _context.SaveChanges();
                }
                setAlert("Sửa thương hiệu thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ" + ex.Message;
                return View(thuongHieu);
            }
        }

        public IActionResult Delete(Guid id)
        {
            var thuongHieu = _context.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ManageTradeMark/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var thuongHieu = _context.ThuongHieux.Find(id);
                if (thuongHieu != null)
                {
                    thuongHieu.IsDeleted = true;
                    _context.ThuongHieux.Update(thuongHieu);
                    _context.SaveChanges();
                }

                setAlert("Xóa thương hiệu thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ!" + ex.Message;
                return View();
            }

        }
    }
}
