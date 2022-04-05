using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgriShop.Areas.Admin.Models
{
    public class TaiKhoan
    {
        [StringLength(100)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [MaxLength(20, ErrorMessage = "Tên đăng nhập phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Tên đăng nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Tên tài khoản")]
        public string TenTaiKhoan { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(20, ErrorMessage = "Mật khẩu phải có tối đa 20 ký tự")]
        [MinLength(3, ErrorMessage = "Mật khẩu nhập phải có tối thiểu 3 ký tự")]
        [DisplayName("Mật khẩu"), DataType(DataType.Password)]

        public string MatKhau { get; set; }
        [StringLength(12)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }


        [StringLength(100)]
        [DisplayName("Địa chỉ email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email cần nhập đúng định dạng. VD: example@gmail.com")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [MaxLength(30, ErrorMessage = "Mật khẩu phải có tối đa 30 ký tự")]
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [StringLength(200)]
        [DisplayName("Địa chỉ")]
        [MaxLength(100, ErrorMessage = "Địa chỉ phải có tối đa 100 ký tự")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [DisplayName("Quyền")]
        public int Quyen { get; set; }

        [DisplayName("Trạng thái")]
        [UIHint("Boolean")]
        public bool TrangThai { get; set; }

        public DateTime? NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
    }
}
