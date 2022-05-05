namespace AnBinhMarket.Models
{
    public class DanhMucViewModel
    {
        public Guid Id { get; set; }
        public string TenDanhMuc { get; set; }
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        public int SoLuongSanPham { get; set; }
    }
}
