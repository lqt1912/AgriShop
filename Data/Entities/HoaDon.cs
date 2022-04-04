using System.ComponentModel.DataAnnotations.Schema;

namespace AgriShop.Data.Entities
{
    public class HoaDon :BaseEntity
    {
        public int  MaHoaDon { get; set; }
        public string TrangThai { get; set; }

        public decimal PhiShip { get; set; }

        public string ChuY { get; set; }

        public string DiaChi { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

        public Guid MaGioHang { get; set; }
        [ForeignKey("MaGioHang")]
        public virtual GioHang GioHang { get; set; }


    }
}
