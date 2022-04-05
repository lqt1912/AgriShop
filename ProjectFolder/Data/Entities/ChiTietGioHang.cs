using System.ComponentModel.DataAnnotations.Schema;

namespace AgriShop.Data.Entities
{
    public class ChiTietGioHang :BaseEntity
    {


        public Decimal Gia { get; set; }
        public int SoLuong { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public Guid MaSP { get; set; }


        [ForeignKey("MaSP")]
        public virtual SanPham SanPham { get; set; }


        public Guid MaGioHang { get; set; }

        [ForeignKey("MaGioHang")]
        public virtual GioHang GioHang { get; set; }

    }
}
