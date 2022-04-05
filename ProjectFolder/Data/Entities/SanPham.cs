﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AgriShop.Data.Entities
{
    public class SanPham: BaseEntity 
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string TrongLuong { get; set; }
        public string MoTa { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }

        public Guid MaDanhMuc { get; set; }
        [ForeignKey("MaDanhMuc")]
        public virtual DanhMuc DanhMuc { get; set; }

        public Guid MaTH { get; set; }
        [ForeignKey("MaTH")]
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }
}
