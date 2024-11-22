using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freak_store.Data;
using freak_store.Models;

namespace freak_store.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el carrito de compras
        public async Task<IActionResult> Index()
        {
            var cart = await GetCartAsync();
            return View(cart);
        }

        // Método para obtener el carrito de compras del usuario actual
        private async Task<ShoppingCart> GetCartAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var cart = await _context.ShoppingCart
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.ShoppingCart.Add(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        // Acción para la vista de pago
        public async Task<IActionResult> Checkout()
        {
            var cart = await GetCartAsync();
            return View(cart);
        }

        // Acción para procesar el pago y vaciar el carrito
        [HttpPost]
        public async Task<IActionResult> ProcessPayment()
        {
            var cart = await GetCartAsync();

            if (cart.Items.Any())
            {
                // Vaciar el carrito
                _context.ShoppingCartItems.RemoveRange(cart.Items);
                cart.Items.Clear();
                cart.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Pago realizado exitosamente.";

            // Redirigir a la vista de confirmación de pago
            return RedirectToAction("PaymentConfirmation");
        }

        // Acción para mostrar la confirmación del pago
        public IActionResult PaymentConfirmation()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        // Método para añadir un producto al carrito
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var cart = await GetCartAsync();
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cartItem = new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price,
                    CreatedAt = DateTime.UtcNow,
                    CartId = cart.Id
                };
                cart.Items.Add(cartItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método para eliminar un producto del carrito
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cart = await GetCartAsync();
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                _context.ShoppingCartItems.Remove(cartItem);
                cart.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}

