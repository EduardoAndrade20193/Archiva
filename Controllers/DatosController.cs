using Archiva.Models;
using Microsoft.AspNetCore.Mvc;

namespace Archiva.Controllers
{
    public class DatosController : Controller
    {
        // Simulación de base de datos en memoria
        private static List<Dato> _datos = new List<Dato>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AgregarDato([FromBody] Dato dato)
        {
            dato.Id = _datos.Count + 1;
            _datos.Add(dato);
            return Json(new { success = true });
        }

        [HttpGet]
        public IActionResult ObtenerDatos()
        {
            return Json(_datos);
        }
    }
}
