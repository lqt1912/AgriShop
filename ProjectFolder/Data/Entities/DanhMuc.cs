﻿using System.ComponentModel;

namespace AgriShop.Data.Entities
{
    public class DanhMuc :BaseEntity
    {
        public int MaDanhMuc { get; set; }
        public string  TenDanhMuc { get; set; }
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;

        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
