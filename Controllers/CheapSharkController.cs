using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace freak_store.Controllers
{
    public class CheapSharkController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://www.cheapshark.com/api/1.0/deals";
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(apiUrl);

            var deals = JsonConvert.DeserializeObject<List<CheapSharkDeal>>(response);
            return View(deals);
        }
    }

    public class CheapSharkDeal
    {
        public string Title { get; set; }
        public string StoreID { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public string Thumb { get; set; }
    }
}
