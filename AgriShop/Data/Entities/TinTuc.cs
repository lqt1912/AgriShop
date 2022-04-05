using System.ComponentModel.DataAnnotations.Schema;

namespace AnBinhMarket.Data.Entities
{
    public class TinTuc : BaseEntity
    {
        public int MaTinTuc { get; set; }
        public string TieuDe { get; set; }
        public string MoTaNgan { get; set; }
        public string MoTaChiTiet { get; set; }
        public string HinhAnh { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }


        public Guid  TenTaiKhoan { get; set; }

        [ForeignKey("TenTaiKhoan")]
        public virtual ApplicationUser TaiKhoan { get; set; }
    }
}
