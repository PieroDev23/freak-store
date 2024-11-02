using Microsoft.AspNetCore.Mvc;
using freak_store.Services;

namespace freak_store.Api
{
  [ApiController] // Marca este controlador como API
  [Route("api/[controller]")] // Define una ruta base para el controlador
  public class CheapSharkController : ControllerBase
  {
    private readonly CheapSharkService _cheapSharkService;

    public CheapSharkController(CheapSharkService cheapSharkService)
    {
      _cheapSharkService = cheapSharkService;
    }

    /// <summary>
    /// Obtiene una lista de ofertas desde CheapShark.
    /// </summary>
    /// <remarks>
    /// Este método realiza una llamada a la API externa de CheapShark para obtener ofertas de juegos.
    /// </remarks>
    /// <returns>Una lista de ofertas disponibles en CheapShark.</returns>
    /// <response code="200">Retorna una lista de ofertas con éxito.</response>
    /// <response code="500">Error interno del servidor.</response>
    [HttpGet("offers")] // Define una ruta específica para este método
    public async Task<IActionResult> GetOffers()
    {
      var offers = await _cheapSharkService.GetCheapSharkOffers();
      return Ok(offers);
    }
  }
}
