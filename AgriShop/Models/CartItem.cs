using AnBinhMarket.Data.Entities;

namespace AnBinhMarket.Models
{
    [Serializable]
    public class CartItem
    {
        public SanPham SanPham { get; set; }
        public int  Soluong { get; set; }
    }
}
