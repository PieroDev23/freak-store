using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace freak_store.Models
{
    [Table("admin_types")]
    public class AdminType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required int Admin_id { get; set; }

        [ForeignKey("admin_id")]
        public Admin? Admin { get; set; }

        [Column(TypeName = "json")]
        public required JSObject Permissions { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

    }
}