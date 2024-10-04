using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace freak_store.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactController(ILogger<ContactController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitContactForm(Contact model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Hubo errores al enviar el formulario";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "¡Gracias por enviarnos un mensaje, nosotros nos pondremos en contacto contigo!";

            _context.Add(model);
            _context.SaveChanges();

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
