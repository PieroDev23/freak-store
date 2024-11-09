using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relación con User
        public virtual User User { get; set; }
        
        // Relación con ShoppingCartItem
        public virtual ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
