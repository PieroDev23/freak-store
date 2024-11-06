using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using freak_store.ViewModels;
using freak_store.Models;
using freak_store.Data;
using System;

namespace freak_store.Controllers
{
    public class AccesoController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccesoController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        // Vista de Login/Registro
        public IActionResult Index()
        {
            return View();
        }

        // Procesa el registro de un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Crear instancia de `User` con datos del ViewModel
                var newUser = new User
                {
                    Firstname = model.FirstName,
                    Lastname = model.LastName,
                    Phone = model.Phone,
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password, // Hash la contraseña en producción
                    CreatedAt = DateTime.Now
                };

                // Agregar el nuevo usuario a la base de datos
                _context.DataUsers.Add(newUser);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registro exitoso. Bienvenido!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = "No se pudo completar el registro. Verifica los datos e intenta nuevamente.";
            return View("Index");
        }
    }
}
