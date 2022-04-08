using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnBinhMarket.Data.Entities
{
    public class TinTuc : BaseEntity
    {
        public int? MaTinTuc { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(255)]
        public string TieuDe { get; set; }

        [Required(ErrorMessage = "Mô tả ngắn không được để trống")]
        [StringLength(300)]
        public string MoTaNgan { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Mô tả chi tiết không được để trống")]
        public string MoTaChiTiet { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [StringLength(100)]
        public string HinhAnh { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }


        public Guid  TenTaiKhoan { get; set; }

        [ForeignKey("TenTaiKhoan")]
        public virtual ApplicationUser TaiKhoan { get; set; }
        public bool IsDDeleted { get; set; } = false;
    }
}
