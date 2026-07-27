using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.Models;
using PracticaProgramada3Grupo7.Services;

namespace PracticaProgramada3Grupo7.Controllers
{
    public class VotacionController : Controller
    {
        private readonly ApiService _apiService;

        public VotacionController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                return Json("La cédula es obligatoria.");
            }

            try
            {
                var votante = await _apiService.ObtenerAsync<Models.VotanteViewModel>($"api/Votantes/cedula/{cedula}");

                if (votante == null)
                {
                    return Json("La cédula no está inscrita.");
                }

                if (votante.YaVoto)
                {
                    return Json("El votante ya ejerció su voto.");
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json($"No fue posible validar la cédula. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<PartidoPoliticoViewModel> partidos =
                    await _apiService.ObtenerListaAsync<PartidoPoliticoViewModel>("api/PartidosPoliticos");

                ViewBag.Partidos = partidos;

                return View(new RegistrarVotoViewModel());
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No fue posible cargar los partidos. " + ex.Message;
                ViewBag.Partidos = new List<PartidoPoliticoViewModel>();
                return View(new RegistrarVotoViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrarVotoViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    ViewBag.Partidos = await _apiService.ObtenerListaAsync<PartidoPoliticoViewModel>("api/PartidosPoliticos");
                }
                catch
                {
                    ViewBag.Partidos = new List<PartidoPoliticoViewModel>();
                }

                return View(modelo);
            }

            try
            {
                HttpResponseMessage respuesta =
                    await _apiService.CrearAsync("api/Votacion", new {
                        Cedula = modelo.Cedula?.Trim() ?? string.Empty,
                        PartidoPoliticoId = modelo.PartidoPoliticoId
                    });

                if (!respuesta.IsSuccessStatusCode)
                {
                    string mensaje = await _apiService.ObtenerMensajeErrorAsync(respuesta);
                    ModelState.AddModelError(string.Empty, mensaje);

                    ViewBag.Partidos = await _apiService.ObtenerListaAsync<PartidoPoliticoViewModel>("api/PartidosPoliticos");

                    return View(modelo);
                }

                TempData["Mensaje"] = "Voto registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "No fue posible registrar el voto. " + ex.Message);

                try
                {
                    ViewBag.Partidos = await _apiService.ObtenerListaAsync<PartidoPoliticoViewModel>("api/PartidosPoliticos");
                }
                catch
                {
                    ViewBag.Partidos = new List<PartidoPoliticoViewModel>();
                }

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Resultados()
        {
            try
            {
                var resultados = await _apiService.ObtenerListaAsync<ResultadoVotacionViewModel>("api/Votacion/resultados");
                return View(resultados);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "No fue posible obtener los resultados. " + ex.Message;
                return View(new List<ResultadoVotacionViewModel>());
            }
        }
    }
}
