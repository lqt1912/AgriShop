using System.ComponentModel.DataAnnotations.Schema;

namespace AnBinhMarket.Data.Entities
{
    public class GioHang :BaseEntity
    {
        public int  MaGioHang { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public Guid TenTaiKhoan { get; set; }
        [ForeignKey("TenTaiKhoan")]
        public virtual ApplicationUser TaiKhoan { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }

    }
}
