namespace AgriShop.Data.Entities
{
    public class LienHe :BaseEntity
    {
        public int  MaLienHe { get; set; }
        public string  NoiDung { get; set; }
        public bool TrangThai { get; set; } = true;
    }
}
