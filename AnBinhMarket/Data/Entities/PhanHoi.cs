﻿namespace AnBinhMarket.Data.Entities
{
    public class PhanHoi :BaseEntity
    {
        public string  Ten { get; set; }
        public string  SoDT { get; set; }
        public string Email { get; set; }
        public string  DiaChi { get; set; }
        public string  NoiDung { get; set; }
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
