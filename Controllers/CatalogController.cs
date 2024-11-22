using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para la vista principal del catálogo
        public async Task<IActionResult> Index(int? categoryId)
        {
            // Obtener las categorías para el filtro
            ViewBag.Categories = await _context.Categories.ToListAsync();

            // Filtrar productos por categoría si se proporciona un categoryId
            var products = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .Include(p => p.Inventory)
                .AsQueryable();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            return View(await products.ToListAsync());
        }

        // Acción para la vista de detalles del producto
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Acción para la búsqueda de productos
        public async Task<IActionResult> FindProduct(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index");
            }

            // Buscar el producto solo por nombre
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Name.Contains(query));

            if (product != null)
            {
                // Redirigir a la vista de detalles del producto si se encuentra
                return RedirectToAction("Details", new { id = product.Id });
            }

            // Si no se encuentra, redirigir al catálogo principal con un mensaje de error
            TempData["ErrorMessage"] = "No se encontró ningún producto con esa descripción.";
            return RedirectToAction("Index");
        }
    }
}
