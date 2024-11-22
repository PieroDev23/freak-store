using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using freak_store.Models;
using freak_store.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Imprimir los errores del ModelState en consola para depuración
                foreach (var state in ModelState.Values)
                {
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
                    }
                }

                return View(model); // Regresar a la vista si el modelo no es válido
            }

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            try
            {
                // Intentar crear el usuario
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    // Imprimir los errores de Identity en consola para depuración
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Identity Error: Code = {error.Code}, Description = {error.Description}");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(model); // Regresar a la vista con los errores
                }

                // Registrar al usuario en el rol por defecto si es necesario
                // Nota: No estamos asignando un rol explícito por el momento
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                // Capturar excepciones inesperadas
                Console.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado. Inténtalo de nuevo.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "El usuario no existe. Por favor, verifica tus credenciales.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
