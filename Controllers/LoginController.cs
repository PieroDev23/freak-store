using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using freak_store.Models;
using freak_store.Data;

namespace freak_store.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly PasswordHasher<User> _passwordHasher;

        public LoginController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _passwordHasher = new PasswordHasher<User>();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InicioSesion(User user)
        {
            // Validar si el modelo es válido
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Buscar el usuario en la base de datos
            User? user_found = await _applicationDbContext.DataUser
                .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (user_found == null || _passwordHasher.VerifyHashedPassword(user_found, user_found.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                // Manejar el caso en que el usuario no se encuentra o la contraseña es incorrecta
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View(user);
            }
            return RedirectToAction("Index", "Home"); // Cambia esto según tu lógica
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}