using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("user_addresses")]
    public class UserAddresses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int user_id { get; set; }

        public User? User { get; set; }

        [Required]
        public required string address_line1 { get; set; }

        public string? address_line2 { get; set; }

        [Required]
        public required string postal_code { get; set; }

        [Required]
        public required string country { get; set; }

        [Required]
        public required string city { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public DateTime? deleted_at { get; set; }
    }
}