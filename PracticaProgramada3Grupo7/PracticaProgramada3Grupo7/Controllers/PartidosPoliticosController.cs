using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.Models;
using PracticaProgramada3Grupo7.Services;

namespace PracticaProgramada3Grupo7.Controllers
{
    public class PartidosPoliticosController : Controller
    {
        private readonly ApiService _apiService;

        public PartidosPoliticosController(
            ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<PartidoPoliticoViewModel> partidos =
                    await _apiService
                        .ObtenerListaAsync<PartidoPoliticoViewModel>(
                            "api/PartidosPoliticos");

                return View(partidos);
            }
            catch (Exception ex)
            {
                ViewBag.Error =
                    "No fue posible cargar los partidos políticos. " +
                    ex.Message;

                return View(
                    new List<PartidoPoliticoViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new PartidoPoliticoViewModel
            {
                EstaActivo = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(
            PartidoPoliticoViewModel modelo)
        {
            LimpiarDatos(modelo);

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                HttpResponseMessage respuesta =
                    await _apiService.CrearAsync(
                        "api/PartidosPoliticos",
                        modelo);

                if (!respuesta.IsSuccessStatusCode)
                {
                    string mensaje =
                        await _apiService
                            .ObtenerMensajeErrorAsync(respuesta);

                    ModelState.AddModelError(
                        string.Empty,
                        mensaje);

                    return View(modelo);
                }

                TempData["Mensaje"] =
                    "El partido político fue registrado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "No fue posible registrar el partido. " +
                    ex.Message);

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                PartidoPoliticoViewModel? partido =
                    await _apiService
                        .ObtenerAsync<PartidoPoliticoViewModel>(
                            $"api/PartidosPoliticos/{id}");

                if (partido == null)
                {
                    return NotFound();
                }

                return View(partido);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible cargar el partido. " +
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int id,
            PartidoPoliticoViewModel modelo)
        {
            if (id != modelo.Id)
            {
                return BadRequest();
            }

            LimpiarDatos(modelo);

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            try
            {
                HttpResponseMessage respuesta =
                    await _apiService.ActualizarAsync(
                        $"api/PartidosPoliticos/{id}",
                        modelo);

                if (!respuesta.IsSuccessStatusCode)
                {
                    string mensaje =
                        await _apiService
                            .ObtenerMensajeErrorAsync(respuesta);

                    ModelState.AddModelError(
                        string.Empty,
                        mensaje);

                    return View(modelo);
                }

                TempData["Mensaje"] =
                    "El partido político fue actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "No fue posible actualizar el partido. " +
                    ex.Message);

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                PartidoPoliticoViewModel? partido =
                    await _apiService
                        .ObtenerAsync<PartidoPoliticoViewModel>(
                            $"api/PartidosPoliticos/{id}");

                if (partido == null)
                {
                    return NotFound();
                }

                return View(partido);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible cargar el partido. " +
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(
            int id)
        {
            try
            {
                HttpResponseMessage respuesta =
                    await _apiService.EliminarAsync(
                        $"api/PartidosPoliticos/{id}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        await _apiService
                            .ObtenerMensajeErrorAsync(respuesta);

                    return RedirectToAction(nameof(Index));
                }

                TempData["Mensaje"] =
                    "El partido político fue eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible eliminar el partido. " +
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        private static void LimpiarDatos(
            PartidoPoliticoViewModel modelo)
        {
            modelo.Nombre =
                modelo.Nombre?.Trim() ?? string.Empty;

            modelo.Siglas =
                modelo.Siglas?
                    .Trim()
                    .ToUpperInvariant()
                ?? string.Empty;

            modelo.Candidato =
                modelo.Candidato?.Trim() ?? string.Empty;

            modelo.Descripcion =
                string.IsNullOrWhiteSpace(modelo.Descripcion)
                    ? null
                    : modelo.Descripcion.Trim();
        }
    }
}