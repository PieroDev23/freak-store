using freak_store.Models;

namespace freak_store.ViewModels;

public class ProductsViewModel
{ 
  public required List<Product> Products { get; set;}

  public required List<Dictionary<string, object>> FeaturedProducts { get; set; }
}