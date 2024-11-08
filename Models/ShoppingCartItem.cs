using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("ShoppingCartItem")]
    public class ShoppingCartItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ShoppingCartId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
