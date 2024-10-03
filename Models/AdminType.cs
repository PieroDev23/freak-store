using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("admin_type")]
    public class AdminType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required int admin_id { get; set; }
        
        [ForeignKey("admin_id")]
        public Admins? Admin { get; set; }

        [Column(TypeName = "json")]
        public required string permissions { get; set; }

         public DateTime CreatedAt { get; set; } = DateTime.Now; 

        public DateTime? UpdatedAt { get; set; }

    }
}   