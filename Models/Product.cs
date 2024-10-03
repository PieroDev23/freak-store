using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Column(TypeName = "smallint")]
        public short Price { get; set; }

        [Required]
        public required string Sku { get; set; }

        [Required]
        public required int Category_id { get; set; }

        [ForeignKey("category_id")]
        public Category? Category { get; set; }

        [Required]
        public required int Inventory_id { get; set; }

        [ForeignKey("inventory_id")]
        public Inventory? Inventory { get; set; }

        [Required]
        public int Discount_id { get; set; }

        [Required]
        [ForeignKey("discount_id")]
        public required Discounts Discount { get; set; }

        [Column(TypeName = "varchar")]
        public string? Img { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }

        public OrderItem? OrderItem { get; set; }
    }
}