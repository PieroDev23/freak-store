using Microsoft.AspNetCore.Mvc;
using System.Linq;
using freak_store.Data;
using freak_store.Models;

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
            // Obtén la lista de productos desde la base de datos
            var products = _context.Products.ToList();

            // Verifica si la lista es null o vacía
            if (products == null)
            {
                products = new List<Product>();
            }

            // Pasa la lista al modelo de la vista
            ViewData["Products"] = products;
            return View();
        }
    }
}
