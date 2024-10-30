using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace freak_store.Controllers
{
    public class CheapSharkController : Controller
    {
        private readonly HttpClient _httpClient;

        public CheapSharkController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var featuredProducts = await GetCheapSharkOffers();
            return View(featuredProducts);
        }

        private async Task<List<Dictionary<string, object>>> GetCheapSharkOffers()
        {
            string apiUrl = "https://www.cheapshark.com/api/1.0/deals?storeID=1&upperPrice=15";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResponse);
            }

            return new List<Dictionary<string, object>>(); // Retorna lista vacía si falla la llamada a la API
        }
    }
}
