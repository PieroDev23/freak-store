using freak_store.Data;
using freak_store.Models;
using Microsoft.AspNetCore.Mvc;
using MLSentymentalAnalysis;

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
                TempData["ErrorMessage"] = "Hubo errores al enviar el formulario";
                return RedirectToAction("Index");
            }
            MLModelTextClassification.ModelInput sampleData = new MLModelTextClassification.ModelInput()
            {
                Comentario = model.Message
            };
            
            MLModelTextClassification.ModelOutput output = MLModelTextClassification.Predict(sampleData);
  
            //Console.WriteLine($"{output.Label}{output.PredictedLabel}");

            //output.Score.ToList().ForEach(score => Console.WriteLine(score));

            var sortedScoresWithLabel = MLModelTextClassification.PredictAllLabels(sampleData);
            var scoreKeyFirst = sortedScoresWithLabel.ToList()[0].Key;
            var scoreValueFirst = sortedScoresWithLabel.ToList()[0].Value;
            var scoreKeySecond = sortedScoresWithLabel.ToList()[1].Key;
            var scoreValueSecond = sortedScoresWithLabel.ToList()[1].Value;

            if(scoreKeyFirst == "1")
            {
                if(scoreValueFirst > 0.5)
                {
                    model.Message = "Positivo";
                }
                else
                {
                    model.Message = "Negativo";
                }
            }else{
                if(scoreValueFirst > 0.5)
                {
                    model.Message = "Negativo";
                }
                else
                {
                    model.Message = "Positivo";
                }
            }
            
            Console.WriteLine($"{scoreKeyFirst,-40}{scoreValueFirst,-20}");
            Console.WriteLine($"{scoreKeySecond,-40}{scoreValueSecond,-20}");

            TempData["SuccessMessage"] = "¡Gracias por enviarnos un mensaje, nosotros nos pondremos en contacto contigo!";

            _context.Add(model);
            _context.SaveChanges();

            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
