using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Img { get; set; }

        public int? DiscountId { get; set; }
        public Discount Discount { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
