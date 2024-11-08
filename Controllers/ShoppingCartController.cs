using freak_store.Data;
using freak_store.Models;
using freak_store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        // Acción para mostrar el carrito de compras del usuario actual
        public IActionResult Index()
        {
            // Obtener el userId del usuario actual
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            // Obtener el carrito del usuario actual desde la base de datos
            var cart = _context.ShoppingCarts
                .Where(c => c.UserId == userId)
                .Select(c => new ShoppingCartViewModel
                {
                    Items = c.Items.Select(i => new ShoppingCartItemViewModel
                    {
                        ImageUrl = i.Product.Img,
                        Name = i.Product.Name,
                        Description = i.Product.Description,
                        Price = i.Product.Price,
                        Quantity = i.Quantity
                    }).ToList(),
                    Subtotal = c.Items.Sum(i => i.Product.Price * i.Quantity),
                    ShippingCost = 5.00M, // Puedes ajustar este valor
                    Discount = 0.00M // Ajustar el descuento si aplica
                }).FirstOrDefault();

            // Si el carrito no existe, devolver una vista con un carrito vacío
            return View(cart ?? new ShoppingCartViewModel());
        }

        // Acción para agregar un producto al carrito
        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int quantity = 1)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var cart = _context.ShoppingCarts.FirstOrDefault(c => c.UserId == userId);

            // Crear un carrito si no existe para el usuario actual
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Agregar o actualizar el producto en el carrito
            var cartItem = _context.ShoppingCartItems
                .FirstOrDefault(ci => ci.ShoppingCartId == cart.Id && ci.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity; // Actualizar cantidad si el producto ya existe en el carrito
            }
            else
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.ShoppingCartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Acción para eliminar un producto del carrito
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemId)
        {
            var cartItem = _context.ShoppingCartItems.Find(cartItemId);
            if (cartItem != null)
            {
                _context.ShoppingCartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Acción para actualizar la cantidad de un producto en el carrito
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid cartItemId, int quantity)
        {
            var cartItem = _context.ShoppingCartItems.Find(cartItemId);
            if (cartItem != null && quantity > 0)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
