using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccesoController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // Acción para mostrar la vista de inicio de sesión y registro
        public IActionResult Index()
        {
            return View();
        }

        // Acción para manejar el registro de usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string first_name, string last_name, string username, string email, string password, int phone)
        {
            if (ModelState.IsValid)
            {
                var newUser = new User
                {
                    Firstname = first_name,
                    Lastname = last_name,
                    Username = username,
                    Email = email,
                    Password = password, // En producción, asegúrate de encriptar la contraseña
                    Phone = phone,
                    CreatedAt = DateTime.UtcNow
                };

                _context.DataUsers.Add(newUser);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";
                TempData.Keep("SuccessMessage"); // Mantener el mensaje hasta que se lea en la vista de acceso
                return RedirectToAction("Index", "Acceso");
            }

            return View("Index");
        }

        // Acción para manejar el inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _context.DataUsers.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                var identityUser = new IdentityUser { UserName = user.Username, NormalizedUserName = user.Username };
                await _signInManager.SignInAsync(identityUser, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "Email o contraseña incorrectos. Inténtalo de nuevo.";
            TempData.Keep("ErrorMessage"); // Mantener el mensaje hasta que se lea en la vista de acceso
            return RedirectToAction("Index", "Acceso");
        }

        // Acción para manejar el cierre de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
