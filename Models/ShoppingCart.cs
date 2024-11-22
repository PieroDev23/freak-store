using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }

        private DateTime createdAt = DateTime.UtcNow;
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value.ToUniversalTime(); }
        }

        private DateTime? updatedAt;
        public DateTime? UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value?.ToUniversalTime(); }
        }
    }

}
