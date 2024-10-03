using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("payment_details")]
    public class PaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? order_id { get; set; }

        public OrderDetails? OrderDetail { get; set; }

        public int? total_amount { get; set; }
        public string? provider { get; set; }
        public string? status { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }
    }
}