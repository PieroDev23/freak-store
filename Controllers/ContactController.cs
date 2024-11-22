using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace freak_store.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Acción para mostrar el formulario de contacto
        public IActionResult Index()
        {
            return View();
        }

        // Acción para procesar el formulario de contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                // Convertir CreatedAt al formato UTC antes de guardar en la base de datos
                contact.CreatedAt = contact.CreatedAt.ToUniversalTime();

                _context.Contacts.Add(contact);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Tu mensaje ha sido enviado exitosamente.";
                return RedirectToAction("Index");
            }

            return View("Index", contact);
        }
    }
}
