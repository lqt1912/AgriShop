namespace AnBinhMarket.Models
{
    public class HoaDonViewModel
    {
        public Guid Id { get; set; }
        public int MaHoaDon { get; set; }
        public string TrangThai { get; set; }

        public decimal PhiShip { get; set; }

        public string? ChuY { get; set; }

        public string? DiaChi { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

        public Decimal ThanhTien { get; set; }
        public string  HoTen { get; set; }
    }
}
