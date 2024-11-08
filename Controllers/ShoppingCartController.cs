using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para obtener o crear el carrito de compras
        private async Task<ShoppingCart> GetOrCreateCartAsync(Guid userId)
        {
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
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

        // Acción para agregar un producto al carrito
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity)
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value); // Obtiene el UserId del usuario autenticado
            var cart = await GetOrCreateCartAsync(userId);

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Acción para mostrar el contenido del carrito
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirst("UserId").Value); // Obtiene el UserId del usuario autenticado
            var cart = await GetOrCreateCartAsync(userId);
            return View(cart);
        }
    }
}

