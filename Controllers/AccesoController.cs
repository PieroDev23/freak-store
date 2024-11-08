using Microsoft.AspNetCore.Mvc;
using freak_store.Data;
using freak_store.Models;
using System;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccesoController(ApplicationDbContext context)
        {
            _context = context;
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
                // Crear un nuevo usuario con los datos del formulario de registro
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

                // Agregar el usuario a la base de datos
                _context.DataUsers.Add(newUser);
                await _context.SaveChangesAsync();

                // Mensaje de éxito temporal para mostrar en la vista de inicio de sesión
                TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";

                // Redirigir al formulario de inicio de sesión tras el registro exitoso
                return RedirectToAction("Index", "Acceso");
            }

            // Si el modelo no es válido, recargar la vista de registro con errores
            return View("Index");
        }
    }
}
