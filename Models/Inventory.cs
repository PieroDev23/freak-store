using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
