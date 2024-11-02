using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using freak_store.ViewModels;
using freak_store.Services;

namespace freak_store.Controllers;

public class CheapSharkPageController : Controller
{
    private readonly CheapSharkService _css;

    public CheapSharkPageController(CheapSharkService css)
    {
        _css = css;
    }

    public async Task<IActionResult> Index()
    {
        var featuredProducs = await _css.GetCheapSharkOffers();
        return View(featuredProducs);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

