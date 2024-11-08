using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace freak_store.Models
{
    [Table("shopping_cart_item")]
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("shopping_cart_id")]
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
