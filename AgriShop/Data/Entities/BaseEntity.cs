using System.ComponentModel.DataAnnotations;

namespace AnBinhMarket.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
