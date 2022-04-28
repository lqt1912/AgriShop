﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AnBinhMarket.Data.Entities
{
    public class HoaDon :BaseEntity
    {
        public string TrangThai { get; set; }

        public decimal PhiShip { get; set; }

        public string? ChuY { get; set; }

        public string? DiaChi { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayCapNhat { get; set; } = DateTime.Now;

        public Guid MaGioHang { get; set; }
        [ForeignKey("MaGioHang")]
        public virtual GioHang GioHang { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
