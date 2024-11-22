using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace freak_store.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminProduct/Index
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .Include(p => p.Inventory)
                .Where(p => p.DeletedAt == null)
                .ToListAsync();

            return View(products);
        }

        // GET: AdminProduct/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.Inventory)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Discounts = await _context.Discounts.ToListAsync();
            return View(product);
        }

        // POST: AdminProduct/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Discounts = await _context.Discounts.ToListAsync();
                return View(product);
            }

            try
            {
                var existingProduct = await _context.Products
                    .Include(p => p.Inventory)
                    .FirstOrDefaultAsync(p => p.Id == product.Id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Update product properties
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Img = product.Img;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.DiscountId = product.DiscountId;
                existingProduct.Inventory.Quantity = product.Inventory.Quantity;
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "El producto ha sido actualizado correctamente.";
                return RedirectToAction("Index", "AdminProduct");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar el producto: {ex.Message}");
                return View(product);
            }
        }
    }
}
