using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace freak_store.Controllers
{
    [Authorize(Roles = "Admin")] // Restringe el acceso solo a usuarios con el rol "Admin"
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View(); // Carga la vista principal del panel de administración
        }
    }
}
