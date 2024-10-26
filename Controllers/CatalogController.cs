using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freak_store.Data;
using freak_store.Models;

namespace freak_store.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción que devuelve la vista del catálogo
        public IActionResult Index(Guid? categoryId)
        {
            // Obtener todas las categorías
            var categories = _context.DataCategories.ToList();

            // Obtener los productos, filtrando si se seleccionó una categoría
            var products = _context.DataProducts
                                   .Include(p => p.Inventory)
                                   .Include(p => p.Category)
                                   .Where(p => !categoryId.HasValue || p.CategoryId == categoryId)
                                   .ToList();

            // Pasar productos y categorías a la vista
            ViewBag.Categories = categories;
            return View(products);
        }

        // Acción que muestra los detalles del producto
        public IActionResult Details(Guid productId)
        {
            // Obtener el producto incluyendo la categoría e inventario
            var product = _context.DataProducts
                                  .Include(p => p.Inventory)
                                  .Include(p => p.Category)
                                  .FirstOrDefault(p => p.Id == productId);

            // Si el producto no se encuentra, redirigir a la vista de error
            if (product == null)
            {
                return NotFound();
            }

            // Pasar el producto a la vista de detalles
            return View("Details", product);
        }
    }
}
