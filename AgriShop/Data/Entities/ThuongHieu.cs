namespace AnBinhMarket.Data.Entities
{
    public class ThuongHieu: BaseEntity
    {
        public int  MaTH { get; set; }
        public string  TenHuongHieu { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<SanPham> SanPhams { get; set; }

    }
}
