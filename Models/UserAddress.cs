using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("user_addresses")]
    public class UserAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int User_id { get; set; }

        public User? User { get; set; }

        [Required]
        public required string Address_line1 { get; set; }

        public string? Address_line2 { get; set; }

        [Required]
        public required string Postal_code { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required string City { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}