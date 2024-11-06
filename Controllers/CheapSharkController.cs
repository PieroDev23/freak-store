using Microsoft.AspNetCore.Mvc;
using freak_store.Services;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class CheapSharkController : Controller
    {
        private readonly CheapSharkService _cheapSharkService;

        public CheapSharkController(CheapSharkService cheapSharkService)
        {
            _cheapSharkService = cheapSharkService;
        }

        public async Task<IActionResult> Index()
        {
            var offers = await _cheapSharkService.GetCheapSharkOffers();
            return View(offers); // Pasa las ofertas directamente a la vista
        }
    }
}
