using System;
using System.ComponentModel.DataAnnotations;

namespace freak_store.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Relación con ShoppingCart
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
