using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("user_payments")]
    public class UserPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int User_id { get; set; }

        [ForeignKey("user_id")]
        public User? User { get; set; }

        [Required]
        public required string Payment_type { get; set; }

        [Required]
        public required string Provider { get; set; }

        [Required]
        public required int Account_number { get; set; }
        
        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

    }
}