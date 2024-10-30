using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        // Acción principal del catálogo
        public async Task<IActionResult> Index(Guid? categoryId)
        {
            var categories = await _context.DataCategories.ToListAsync();
            var products = _context.DataProducts
                                   .Include(p => p.Inventory)
                                   .Include(p => p.Category)
                                   .Where(p => !categoryId.HasValue || p.CategoryId == categoryId)
                                   .ToList();

            ViewBag.Categories = categories;
            return View(products);
        }

        // Acción de búsqueda
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["SearchMessage"] = "Por favor, ingrese un término de búsqueda.";
                return RedirectToAction("Index");
            }

            var product = await _context.DataProducts
                .FirstOrDefaultAsync(p => EF.Functions.ILike(p.Name, $"%{query}%"));

            if (product == null)
            {
                TempData["SearchMessage"] = $"No se encontraron resultados para '{query}'.";
                return RedirectToAction("Index");
            }

            // Redirige a la vista de detalles del producto si se encuentra una coincidencia
            return RedirectToAction("Details", new { id = product.Id });
        }

        // Acción para mostrar los detalles de un producto
        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _context.DataProducts
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
