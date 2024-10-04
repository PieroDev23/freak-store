using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("contacts")]
    public class Contact
    {
        public Guid Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("subject")]
        public string? Subject { get; set; }

        [Column("message")]
        public string?  Message{ get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}

