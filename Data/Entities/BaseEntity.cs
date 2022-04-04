using System.ComponentModel.DataAnnotations;

namespace AgriShop.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
