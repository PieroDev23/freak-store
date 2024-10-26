using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using freak_store.ViewModels;
using freak_store.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace freak_store.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = _context.DataProducts
                               .Include(p => p.Inventory)
                               .Include(p => p.Category)
                               .ToList();

        var featuredProducts = await GetFeaturedProducts();

        // Crear el modelo de vista y pasar ambos conjuntos de datos
        var viewModel = new ProductsViewModel
        {
            Products = products,
            FeaturedProducts = featuredProducts
        };

        return View(viewModel);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<List<Dictionary<string, object>>> GetFeaturedProducts()
    {
        var client = new HttpClient();
        var response = await client.GetStreamAsync("https://www.cheapshark.com/api/1.0/deals?storeID=1&upperPrice=5");
        var featuredProducts = await JsonSerializer.DeserializeAsync<List<Dictionary<string, object>>>(response);
        return featuredProducts!;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
