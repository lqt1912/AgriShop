using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageProductController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManageProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string searchString)
        {
            var sanphams = _context.SanPhams.Include(x => x.DanhMuc).Where(x => !x.IsDeleted).ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                sanphams = sanphams.Where(p => p.TenSP.Contains(searchString)).ToList();
            }
            return View(sanphams.OrderByDescending(p => p.MaSP).ToList());
        }
        public IActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(_context.DanhMucs, "Id", "TenDanhMuc");
            ViewBag.MaTH = new SelectList(_context.ThuongHieux, "Id", "TenHuongHieu");
            return View();
        }

        public IActionResult Details(Guid id)
        {

            var sanPhams = _context.SanPhams.Include(x => x.DanhMuc).Include(x => x.ThuongHieu);
            var sanPham = sanPhams.FirstOrDefault(x => x.Id == id);
            if (sanPham == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            ViewBag.MaDanhMuc = new SelectList(_context.DanhMucs, "Id", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaTH = new SelectList(_context.ThuongHieux, "Id", "TenThuongHieu", sanPham.MaTH);
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            try
            {
                var _sanPham = new SanPham()
                {
                    Gia = sanPham.Gia,
                    GiaKM = sanPham.GiaKM, 
                    MaDanhMuc = sanPham.MaDanhMuc,
                    MaSP = sanPham.MaSP,
                    MaTH = sanPham.MaTH,
                    MoTa = sanPham.MoTa,
                    NgayCapNhat = DateTime.Now,
                    NgayTao = DateTime.Now,
                    TrongLuong = sanPham.TrongLuong,
                    TenSP = sanPham.TenSP, 
                    SoLuong = sanPham.SoLuong
                };
                sanPham.HinhAnh = "";
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
                        path = Path.Combine(webRootPath, "products");
                        string UploadPath = path + '\\' + FileName;

                        using (Stream fileStream = new FileStream(UploadPath, FileMode.Create))
                        {
                            await f.CopyToAsync(fileStream);
                        }
                        sanPham.HinhAnh = FileName;
                        _sanPham.HinhAnh = FileName;
                    }
                }
                _context.SanPhams.Add(_sanPham);
                _context.SaveChanges();

                setAlert("Thêm mới sản phẩm thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.MaDanhMuc = new SelectList(_context.DanhMucs, "Id", "TenDanhMuc", sanPham.MaDanhMuc);
                ViewBag.MaTH = new SelectList(_context.ThuongHieux, "Id", "TenThuongHieu", sanPham.MaTH);
                ViewBag.Error = "Dữ liệu không hợp lệ" + ex.Message;
                return View(sanPham);
            }
        }

        public IActionResult Edit(Guid id)
        {
            var product = _context.SanPhams.Find(id);
            if (product == null)
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            product.MoTa = HttpUtility.HtmlDecode(product.MoTa);

            ViewBag.MaDanhMuc = new SelectList(_context.DanhMucs, "Id", "TenDanhMuc", product.MaDanhMuc);
            ViewBag.MaTH = new SelectList(_context.ThuongHieux, "Id", "TenHuongHieu", product.MaTH);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SanPham sanPham)
        {
            try
            {
                var _sanPham = _context.SanPhams.Find(sanPham.Id);
                if (_sanPham != null)
                {
                    _sanPham.TenSP = sanPham.TenSP;
                    _sanPham.MoTa = sanPham.MoTa;
                    _sanPham.MaTH = sanPham.MaTH;
                    _sanPham.MaDanhMuc = sanPham.MaDanhMuc;
                    _sanPham.Gia = sanPham.Gia;
                    _sanPham.GiaKM = sanPham.GiaKM;
                    _sanPham.SoLuong = sanPham.SoLuong;
                    _sanPham.MaTH = sanPham.MaTH;
                    _sanPham.NgayCapNhat = DateTime.Now;
                    _sanPham.NgayTao = sanPham.NgayTao;
                    _sanPham.TrongLuong = sanPham.TrongLuong;

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
                            path = Path.Combine(webRootPath, "products");
                            string UploadPath = path + '\\' + FileName;

                            using (Stream fileStream = new FileStream(UploadPath, FileMode.Create))
                            {
                                await f.CopyToAsync(fileStream);
                            }
                            _sanPham.HinhAnh = FileName;
                        }
                    }
                    _context.SanPhams.Update(_sanPham);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.MaDanhMuc = new SelectList(_context.DanhMucs, "Id", "TenDanhMuc", sanPham.MaDanhMuc);
                ViewBag.MaTH = new SelectList(_context.ThuongHieux, "Id", "TenHuongHieu", sanPham.MaTH);
                ViewBag.Error = "Dữ liệu không hợp lệ" + ex.Message;
                return View(sanPham);
            }
        }

        public IActionResult Delete(Guid id)
        {
            var sanPhams = _context.SanPhams.Include(x => x.DanhMuc).Include(x => x.ThuongHieu);
            var sanPham = sanPhams.FirstOrDefault(x => x.Id == id);
            if (sanPham == null)
            {
                return RedirectToAction("Index", "NotFound", new { Area = "Admin" });

            }
            return View(sanPham);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var sanPham = _context.SanPhams.Find(id);
            try
            {
                if (sanPham != null)
                {
                    sanPham.IsDeleted = true;
                    _context.SanPhams.Update(sanPham);
                    _context.SaveChanges();
                }
                setAlert("Xoá sản phẩm thành công!", "success");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Dữ liệu không hợp lệ" + ex.Message;
                return View(sanPham);
            }


        }
    }
}
