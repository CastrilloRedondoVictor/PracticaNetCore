using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PracticaNetCore.Models;
using PracticaNetCore.Repositories;

namespace PracticaNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RepositoryZapatillas repo;

        public HomeController(ILogger<HomeController> logger, RepositoryZapatillas repo)
        {
            _logger = logger;
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }

        public async Task<IActionResult> Details(int? posicion, int zapatilla)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            (int totalRegistros, Imagen? imagen) = await this.repo.GetImagenesZapatillaAsync(posicion.Value, zapatilla);


            Zapatilla? zap = await this.repo.FindZapatillaAsync(zapatilla);


            ViewData["REGISTROS"] = totalRegistros;
            ViewData["ZAPATILLA"] = zap;
            ViewData["POSICION"] = posicion;

            return View(imagen);
        }

        public async Task<IActionResult> GetImagen(int posicion, int zapatilla)
        {
            (int totalRegistros, Imagen? imagen) = await this.repo.GetImagenesZapatillaAsync(posicion, zapatilla);

            if (imagen == null)
            {
                return NotFound();
            }

            ViewData["REGISTROS"] = totalRegistros;
            ViewData["POSICION"] = posicion;
            ViewData["ZAPATILLA_ID"] = zapatilla;

            return PartialView("_ImagenPartial", imagen);
        }







        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
