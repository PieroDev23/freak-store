using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("user_payments")]
    public class UserPayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int user_id { get; set; }

        [ForeignKey("user_id")]
        public User? User { get; set; }

        [Required]
        public required string payment_type { get; set; }

        [Required]
        public required string provider { get; set; }

        [Required]
        public required int account_number { get; set; }
        
        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

    }
}