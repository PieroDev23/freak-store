using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int quantity { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public DateTime? deleted_at { get; set; }

        public Products? Product { get; set; }
    }
}