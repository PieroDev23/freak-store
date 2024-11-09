using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using freak_store.Data;
using freak_store.Models;

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

        // Acción para mostrar la vista de inicio de sesión y registro
        public IActionResult Index()
        {
            return View();
        }

        // Procesar inicio de sesión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            Console.WriteLine("Login action called");

            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                Console.WriteLine("Login successful");
                TempData["SuccessMessage"] = "Inicio de sesión exitoso.";
                return RedirectToAction("Index", "Home");
            }

            Console.WriteLine("Login failed");
            ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            return View("Index");
        }

        // Procesar registro de un nuevo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string first_name, string last_name, string username, string email, string password, int phone)
        {
            Console.WriteLine("Register action called");

            if (ModelState.IsValid)
            {
                Console.WriteLine("Model state is valid");

                // Crear un nuevo usuario de identidad
                var identityUser = new IdentityUser { UserName = email, Email = email };
                var result = await _userManager.CreateAsync(identityUser, password);

                if (result.Succeeded)
                {
                    Console.WriteLine("Identity user created successfully");

                    // Crear un usuario personalizado para tu aplicación
                    var newUser = new User
                    {
                        Firstname = first_name,
                        Lastname = last_name,
                        Username = username,
                        Email = email,
                        Password = password, // Asegúrate de encriptar en producción
                        Phone = phone,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";
                    return RedirectToAction("Index", "Acceso");
                }
                else
                {
                    Console.WriteLine("Error creating identity user");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                Console.WriteLine("Model state is invalid");
            }

            TempData["ErrorMessage"] = "Error en el registro. Por favor, verifica tus datos.";
            return View("Index");
        }

        // Cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["SuccessMessage"] = "Has cerrado sesión.";
            return RedirectToAction("Index", "Home");
        }
    }
}