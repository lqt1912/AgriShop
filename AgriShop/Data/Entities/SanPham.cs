using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnBinhMarket.Data.Entities
{
    public class SanPham
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? MaSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200)]
        public string TenSP { get; set; }

        [Required(ErrorMessage = "Trọng lượng không được để trống")]
        [StringLength(100)]
        public string TrongLuong { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        //[DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống!")]
        [RegularExpression("^[0-9]*\\.?[0-9]*$", ErrorMessage = "Giá sản phẩm phải là một số.")]
        //[DisplayFormat(DataFormatString = "{0:#,###}")]
        public decimal? GiaKM { get; set; } = 0;


        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [StringLength(100)]
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public Guid MaDanhMuc { get; set; }
        [ForeignKey("MaDanhMuc")]
        public  DanhMuc DanhMuc { get; set; }

        public Guid MaTH { get; set; }
        [ForeignKey("MaTH")]
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
