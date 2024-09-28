using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace freak_store.Models
{
    // Enum fuera de la clase User
    public enum Roles
    {
        ADMIN,
        CLIENT
    }

    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Roles Role { get; set; } = Roles.CLIENT;

        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}
