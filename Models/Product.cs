using System;

namespace freak_store.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }  // Esta propiedad debe existir
        public decimal Price { get; set; }
        public string Img { get; set; }
        public string Sku { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? DiscountId { get; set; }
        public Discount Discount { get; set; }
        public Guid? InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
