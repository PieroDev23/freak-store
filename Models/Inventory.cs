using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("inventory")]
    public class Inventory
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign key
         public ICollection<Product>? Products { get; set; }
    }
}