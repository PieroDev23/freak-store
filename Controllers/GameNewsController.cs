using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace freak_store.Controllers
{
    public class GameNewsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string apiUrl = "https://www.gamerpower.com/api/giveaways";
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(apiUrl);

            // Parsear la respuesta JSON en un JArray
            var newsArray = JArray.Parse(response);

            return View(newsArray);
        }
    }
}
