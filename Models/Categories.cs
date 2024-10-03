using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("categories")]
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string name { get; set; }

        public  string? description { get; set; }

        public DateTime create_at { get; set; } = DateTime.Now;

        public DateTime? update_at { get; set; }

        public DateTime? deleted_at { get; set; }

        public ICollection<Products>? Products { get; set; } 
    }
}