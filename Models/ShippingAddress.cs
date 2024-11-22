using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class ShippingAddress
    {
        [Key]
        public int Id { get; set; }

        // Clave foránea de tipo string para coincidir con el tipo Id de User
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string AddressLine { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
