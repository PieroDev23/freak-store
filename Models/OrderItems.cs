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

        public int? order_id { get; set; }
        [ForeignKey("order_id")]
        public OrderDetails? OrderDetail { get; set; }

        public int? product_id { get; set; }
        [ForeignKey("product_id")]
        public Products? Product { get; set; }

        public int? quantity { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;
        public DateTime? update_at { get; set; }
    }
}