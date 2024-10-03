using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("admins")]
    public class Admins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string first_name { get; set; }

        [Required]
        public required string last_name { get; set; }

        [Required]
        public required string email { get; set; }

        [Required]
        public required string password { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public DateTime? deleted_at { get; set; }

        public AdminType? AdminType { get; set; }
    }
}