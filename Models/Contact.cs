using System;
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
        public string? Message { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
