using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Percentage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
