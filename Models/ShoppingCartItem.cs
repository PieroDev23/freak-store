using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShoppingCartId { get; set; }
        public int Quantity { get; set; }

        // Relación con Product
        public virtual Product Product { get; set; }
        
        // Relación con ShoppingCart
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
