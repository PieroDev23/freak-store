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
            var categories = await _context.Categories.ToListAsync();
            var products = await _context.Products
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .Where(p => !categoryId.HasValue || p.CategoryId == categoryId)
                .ToListAsync();

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

            var product = await _context.Products
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
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Acción para añadir un producto al carrito
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                return Json(new { success = false, message = "ID de producto no válido." });
            }

            // Validación del UserId
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Json(new { success = false, message = "Usuario no autenticado." });
            }

            var userId = Guid.Parse(userIdClaim);

            // Obtiene o crea el carrito
            var cart = await GetOrCreateCartAsync(userId);

            // Comprueba si el producto existe antes de añadirlo
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return Json(new { success = false, message = "Producto no encontrado." });
            }

            // Añade el producto al carrito
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = 1
                });
            }

            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Producto añadido al carrito" });
        }

        // Método auxiliar para obtener o crear un carrito de compras
        private async Task<ShoppingCart> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }
    }
}
