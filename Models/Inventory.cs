using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("created_at")]
        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key
        public ICollection<Product>? Products { get; set; }
    }
}