using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("admins")]
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string First_name { get; set; }

        [Required]
        public required string Last_name { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;
        
        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }

        public AdminType? Admin_type { get; set; }
    }
}