using AnBinhMarket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnBinhMarket.Models.Components
{
    public class _OrderViewViewComponent: ViewComponent
    {
        ApplicationDbContext _context;

        public _OrderViewViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid TenTK)
        {
            var carts = _context.GioHangs.Where(g => g.TenTaiKhoan.Equals(TenTK)).ToList();
            var hds = _context.HoaDons.Where(x=>!x.IsDeleted).Include(x => x.GioHang).ThenInclude(x => x.ChiTietGioHangs).Include(x => x.GioHang).ThenInclude(x => x.TaiKhoan);
            var _receipts = carts.Join(hds, x => x.Id, y => y.MaGioHang, (x, y) => y).OrderByDescending(x=>x.NgayTao);
            if (_receipts != null)
            {
                var result = new List<HoaDonViewModel>();

                foreach (var hoaDon in _receipts)
                {
                    decimal thanhTien = 0;

                    foreach (var ctgh in hoaDon.GioHang.ChiTietGioHangs)
                    {
                        thanhTien += ctgh.Gia * ctgh.SoLuong;
                    }
                    result.Add(new HoaDonViewModel()
                    {
                        ChuY = hoaDon.ChuY,
                        DiaChi = hoaDon.DiaChi,
                        MaHoaDon = hoaDon.MaHoaDon,
                        NgayCapNhat = hoaDon.NgayCapNhat,
                        NgayTao = hoaDon.NgayTao,
                        ThanhTien = thanhTien + hoaDon.PhiShip,
                        TrangThai = hoaDon.TrangThai,
                        Id = hoaDon.Id,
                        HoTen = hoaDon.GioHang.TaiKhoan.HoTen
                    });
                }
                return View(result.OrderByDescending(x=>x.NgayCapNhat));
            }
          
            return View(new List<HoaDonViewModel>());
        }

    }
}
