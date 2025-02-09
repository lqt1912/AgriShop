﻿using System.ComponentModel.DataAnnotations;

namespace AnBinhMarket.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
