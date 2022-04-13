using AnBinhMarket.Data.Entities;

namespace AnBinhMarket.Models
{
    [Serializable]
    public class CartItem
    {
        public SanPham SanPham { get; set; }
        public int  SoLuong { get; set; }
    }
}
