using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using MLSentymentalAnalysis;
using Microsoft.Extensions.Logging;
using System;

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
                TempData["ErrorMessage"] = "Hubo errores al enviar el formulario.";
                return RedirectToAction("Index");
            }

            // Clasificación de sentimiento usando el modelo de ML
            MLModelTextClassification.ModelInput sampleData = new MLModelTextClassification.ModelInput()
            {
                Comentario = model.Message
            };

            MLModelTextClassification.ModelOutput output = MLModelTextClassification.Predict(sampleData);
            var sortedScoresWithLabel = MLModelTextClassification.PredictAllLabels(sampleData);
            var scoreKeyFirst = sortedScoresWithLabel.ToList()[0].Key;
            var scoreValueFirst = sortedScoresWithLabel.ToList()[0].Value;

            // Modificar el mensaje según el análisis de sentimiento
            model.Message = scoreKeyFirst == "1" && scoreValueFirst > 0.5 ? "Positivo" : "Negativo";

            // Guardar el mensaje en la base de datos
            _context.Contacts.Add(model);  // Cambiado de DataContact a Contacts
            _context.SaveChanges();

            TempData["SuccessMessage"] = "¡Gracias por enviarnos un mensaje, nosotros nos pondremos en contacto contigo!";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
