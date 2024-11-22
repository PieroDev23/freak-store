using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
        public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        private DateTime createdAt = DateTime.UtcNow;
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value.ToUniversalTime(); }
        }
    }

}
