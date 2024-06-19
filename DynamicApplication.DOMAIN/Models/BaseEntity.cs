using System.ComponentModel.DataAnnotations;

namespace DynamicApplication.DOMAIN.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}
