using Microsoft.AspNetCore.Mvc;
using freak_store.Models; 
using freak_store.Data;

namespace freak_store.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ILogger<AccesoController> _logger;
        private readonly ApplicationDbContext _context; // Inyección de dependencias para el contexto

        public AccesoController(ILogger<AccesoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Acción para mostrar la vista de Login
        public IActionResult Index()
        {
            return View();
        }

        // Acción para manejar la autenticación del usuario
        [HttpPost]
        public IActionResult InicioSesion(User model)
        {
            if (ModelState.IsValid)
            {
                // Validar usuario en la base de datos con el modelo correcto
                var user = _context.DataUsers.FirstOrDefault(x => x.Username == model.Username);

                if (user != null)
                {
                    // Si el usuario existe, autenticación exitosa
                    return RedirectToAction("Index", "Home"); // Redirige a la página principal
                }
                else
                {
                    // Si el usuario no es encontrado, mostrar mensaje de error
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                }
            }

            // Si algo falla, devolver la vista de login con el mensaje de error
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}