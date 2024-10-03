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
        public required int Total_amount { get; set; }

        [Required]
        public required int Payment_id { get; set; }

        public PaymentDetail? PaymentDetail { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}