using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime Created_at { get; set; } = DateTime.Now;

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }

        public ICollection<Products>? Products { get; set; }
    }
}