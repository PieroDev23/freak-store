using System.Collections.Generic;

namespace freak_store.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();
        public decimal Subtotal { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Discount { get; set; }
        public decimal Total => Subtotal + ShippingCost - Discount;
    }

    public class ShoppingCartItemViewModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
