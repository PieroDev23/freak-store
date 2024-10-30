using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using freak_store.ViewModels;

namespace freak_store.Services
{
    public class CheapSharkService
    {
        private readonly HttpClient _httpClient;

        public CheapSharkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OfferViewModel>> GetCheapSharkOffers()
        {
            string apiUrl = "https://www.cheapshark.com/api/1.0/deals?storeID=1&upperPrice=15";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<OfferViewModel>>(jsonResponse);
            }

            return new List<OfferViewModel>(); // Retorna lista vacía si falla la llamada a la API
        }
    }
}
