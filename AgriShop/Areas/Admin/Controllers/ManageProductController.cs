using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            var sanphams = _context.SanPhams.Include(x=>x.DanhMuc).ToList();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            try
            {
                var _sanPham = new SanPham()
                {
                    Gia = sanPham.Gia,
                    MaDanhMuc = sanPham.MaDanhMuc,
                    MaSP = sanPham.MaSP,
                    MaTH = sanPham.MaTH,
                    MoTa = sanPham.MoTa,
                    NgayCapNhat = DateTime.Now,
                    NgayTao = DateTime.Now,
                    TrongLuong = sanPham.TrongLuong,
                    TenSP = sanPham.TenSP
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
    }
}
