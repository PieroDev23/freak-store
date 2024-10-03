using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("order_details")]
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int user_id { get; set; }

        public UserAddresses? UserAddress { get; set; } 

        [Required]
        public required int total_amount { get; set; }

        [Required]
        public required int payment_id { get; set; }
        [ForeignKey("payment_id")]
        public PaymentDetails? PaymentDetail { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public ICollection<OrderItems>? OrderItems { get; set; }
    }
}