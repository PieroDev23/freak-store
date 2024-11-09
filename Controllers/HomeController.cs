using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using freak_store.ViewModels;
using freak_store.Data;
using Microsoft.EntityFrameworkCore;
using freak_store.Services;

namespace freak_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products  // Cambiado de DataProducts a Products
                                   .Include(p => p.Inventory)
                                   .Include(p => p.Category)
                                   .ToList();

            // Crear el modelo de vista y pasar ambos conjuntos de datos
            var viewModel = new ProductsViewModel
            {
                Products = products,
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
