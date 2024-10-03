using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("products")]
    public class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string name { get; set; }

        [Required]
        public required string description { get; set; }

        [Column(TypeName = "smallint")]
        public short Price { get; set; }

        [Required]
        public required string sku { get; set; }

        [Required]
        public required int category_id { get; set; }

        [ForeignKey("category_id")]
        public Categories? Category { get; set; }

        [Required]
        public required int inventory_id { get; set; }

        [ForeignKey("inventory_id")]
        public Inventory? Inventory { get; set; }

        [Required]
        public required int discount_id { get; set; }

        [ForeignKey("discount_id")]
        public Discounts? Discount { get; set; }

        [Column(TypeName = "text")]
        public string? img { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public DateTime? deleted_at { get; set; }

        public OrderItems? OrderItem { get; set; }
    }
}