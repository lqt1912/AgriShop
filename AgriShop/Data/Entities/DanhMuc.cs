using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnBinhMarket.Data.Entities
{
    public class DanhMuc :BaseEntity
    {
        public DanhMuc() :base()
        {
            SanPhams = new HashSet<SanPham>();
        }
        public int? MaDanhMuc { get; set; }

        [Required(ErrorMessage = ("Tên danh mục không được để trống"))]
        [StringLength(100)]
        public string  TenDanhMuc { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<SanPham> SanPhams { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
