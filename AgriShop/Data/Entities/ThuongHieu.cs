using System.ComponentModel.DataAnnotations;

namespace AnBinhMarket.Data.Entities
{
    public class ThuongHieu: BaseEntity
    {

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(150)]
        public string  TenHuongHieu { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<SanPham> SanPhams { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
