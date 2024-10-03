using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace freak_store.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("first_name")]
        public required string Firstname { get; set; }

        [Column("password")]
        public required string Password { get; set; }

        [Column("phone")]
        public required int Phone { get; set; }

        [Column("last_name")]
        public required string Lastname { get; set; }

        [Column("username")]
        public required string Username { get; set; }

        [Column("email")]
        public required string Email { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

    }
}
