using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace freak_store.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string First_name { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required int Phone { get; set; }

        [Required]
        public required string Last_name { get; set; }
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Email { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        [Required]
        public DateTime Updated_at { get; set; }

       [Required]
        public DateTime Deleted_at { get; set; }

        public ICollection<UserPayments>? Payments { get; set; }
        public ICollection<UserAddresses>? Addresses { get; set; }
    }
}
