using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using MLModeloContact;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(Contact model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Hubo errores al enviar el formulario";
                return RedirectToAction("Index");
            }

            if (!model.Message.EndsWith("."))
            {
                model.Message += ".";
            }

            // Crear la entrada de predicción basada en el mensaje
            var sampleData = new MLModelContact.ModelInput()
            {
                Comentario = model.Message
            };

            // Realizar la predicción
            var output = MLModelContact.Predict(sampleData);
            var sortedScoresWithLabel = MLModelContact.PredictAllLabels(sampleData);

            // Asignación de resultado al Subject basado en el score y etiquetas    
            var scoreKeyFirst = sortedScoresWithLabel.First().Key;
            var scoreValueFirst = sortedScoresWithLabel.First().Value;

            model.Subject = scoreKeyFirst == "1" && scoreValueFirst > 0.5 ? "Positivo" : "Negativo";

            Console.WriteLine($"{scoreKeyFirst,-40}{scoreValueFirst,-20}");
            model.CreatedAt = model.CreatedAt.ToUniversalTime();
            // Guardar el modelo en la base de datos
            _context.Add(model);
            _context.SaveChanges();

            // Mensaje de éxito para la vista
            TempData["SuccessMessage"] = "¡Gracias por enviarnos un mensaje, nosotros nos pondremos en contacto contigo!";

            // Limpiar el modelo del formulario
            return RedirectToAction("Index");
        }
    }
}