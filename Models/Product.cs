using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }


        [Column("price")]
        public decimal Price { get; set; }

        [Column("sku")]
        public string? Sku { get; set; }

        [Column("img")]
        public string? Img { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        [Column("discount_id")]
        public Guid? DiscountId { get; set; }
        public Discount? Discount { get; set; }

        [Column("inventory_id")]
        public Guid? InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
    }
}