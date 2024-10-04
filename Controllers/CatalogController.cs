using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freak_store.Data;
using freak_store.Models;
using System.Linq;

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
    }
}
