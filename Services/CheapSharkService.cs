using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using freak_store.ViewModels;
using System;

namespace freak_store.Services
{
    public class CheapSharkService
    {
        private readonly HttpClient _httpClient;

        public CheapSharkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Dictionary<string, object>>> GetCheapSharkOffers()
        {
            string apiUrl = "https://www.cheapshark.com/api/1.0/deals?storeID=1&upperPrice=15";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonResponse)!;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error de deserialización: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error en la llamada a la API de CheapShark: {response.StatusCode}");
            }

            return [];
        }
    }
}
