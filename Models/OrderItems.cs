using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("order_items")]
    public class OrderItems
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? Order_id { get; set; }
        [ForeignKey("order_id")]
        public OrderDetails? OrderDetail { get; set; }

        public int? Product_id { get; set; }
        [ForeignKey("product_id")]
        public Products? Product { get; set; }

        public required int Quantity { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime? Updated_at { get; set; }
    }
}