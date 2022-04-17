using AnBinhMarket.Data;
using AnBinhMarket.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageController : Controller
    {
        ApplicationDbContext db;

        public ManageController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Manage
        [Authorize]
        public IActionResult Index()
        {
          
            var products = db.SanPhams.ToList();
            var categories = db.DanhMucs.ToList();
            var receipts = db.HoaDons.ToList();
            var trademarks = db.ThuongHieux.ToList();
            var news = db.TinTucs.ToList();
            var feedbacks = db.PhanHois.ToList();
            DateTime today = DateTime.Today;

            List<HoaDon> hd = db.HoaDons.Where(h => h.TrangThai.Equals("Đã giao")).ToList();
            List<HoaDon> hds = db.HoaDons.Include(x=>x.GioHang).ThenInclude(x=>x.ChiTietGioHangs).Where(h => h.NgayTao.Month == today.Month &&
                    h.NgayTao.Year == today.Year && h.TrangThai.Equals("Đã giao")).ToList();

            decimal tongTienNum = 0;
            foreach (var item in hds)
            {
                tongTienNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuong * c.Gia).Sum();
            }

            List<HoaDon> hd_trong_nam = receipts.Where(h => h.NgayTao.Year == today.Year
                    && h.TrangThai.Equals("Đã giao")).ToList();
            decimal tongTienTrongNamNum = 0;
            foreach (var item in hd_trong_nam)
            {
                tongTienTrongNamNum += item.GioHang.ChiTietGioHangs.Select(c => c.SoLuong * c.Gia).Sum();
            }

            var accounts = db.Users.Where(t => t.Quyen == 0).ToList();

            ViewBag.soLuongSp = products.Count;
            ViewBag.soLuongDm = categories.Count;
            ViewBag.soLuongDh = hds.Count;
            ViewBag.soLuongTk = accounts.Count;
            ViewBag.soLuongTT = news.Count;
            ViewBag.soLuongTH = trademarks.Count;
            ViewBag.soLuongPH = feedbacks.Count;
            ViewBag.soHD = hd.Count;
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string tongTien = string.Format("{0:0,000}", tongTienNum);
            string tongTienTrongNam = string.Format("{0:0,000}", tongTienTrongNamNum);
            ViewBag.tongTienThangNay = tongTien;
            ViewBag.tongTienTrongNam = tongTienTrongNam;
            return View();
        }

    }
}
