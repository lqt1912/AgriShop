﻿using Microsoft.AspNetCore.Identity;

namespace AgriShop.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string HoTen { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Quyen { get; set; }
        public string  DiaChi { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual ICollection<TinTuc> TinTucs { get; set; }


    }

    public class ApplicationRole : IdentityRole<Guid>
    {

    }
}
