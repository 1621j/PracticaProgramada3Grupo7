using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.Models;
using PracticaProgramada3Grupo7.Services;

namespace PracticaProgramada3Grupo7.Controllers
{
    public class VotantesController : Controller
    {
        private readonly ApiService _apiService;

        public VotantesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<VotanteViewModel> votantes =
                    await _apiService
                        .ObtenerListaAsync<VotanteViewModel>(
                            "api/Votantes");

                return View(votantes);
            }
            catch (Exception ex)
            {
                ViewBag.Error =
                    "No fue posible cargar los votantes. " +
                    ex.Message;

                return View(new List<VotanteViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new VotanteViewModel
            {
                EstaActivo = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(
            VotanteViewModel modelo)
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
                        "api/Votantes",
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
                    "El votante fue registrado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "No fue posible registrar el votante. " +
                    ex.Message);

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                VotanteViewModel? votante =
                    await _apiService
                        .ObtenerAsync<VotanteViewModel>(
                            $"api/Votantes/{id}");

                if (votante == null)
                {
                    return NotFound();
                }

                return View(votante);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible cargar el votante. " +
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(
            int id,
            VotanteViewModel modelo)
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
                        $"api/Votantes/{id}",
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
                    "El votante fue actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "No fue posible actualizar el votante. " +
                    ex.Message);

                return View(modelo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                VotanteViewModel? votante =
                    await _apiService
                        .ObtenerAsync<VotanteViewModel>(
                            $"api/Votantes/{id}");

                if (votante == null)
                {
                    return NotFound();
                }

                return View(votante);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible cargar el votante. " +
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
                        $"api/Votantes/{id}");

                if (!respuesta.IsSuccessStatusCode)
                {
                    TempData["Error"] =
                        await _apiService
                            .ObtenerMensajeErrorAsync(respuesta);

                    return RedirectToAction(nameof(Index));
                }

                TempData["Mensaje"] =
                    "El votante fue eliminado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "No fue posible eliminar el votante. " +
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        private static void LimpiarDatos(
            VotanteViewModel modelo)
        {
            modelo.Cedula =
                modelo.Cedula?.Trim() ?? string.Empty;

            modelo.Nombre =
                modelo.Nombre?.Trim() ?? string.Empty;

            modelo.PrimerApellido =
                modelo.PrimerApellido?.Trim()
                ?? string.Empty;

            modelo.SegundoApellido =
                string.IsNullOrWhiteSpace(
                    modelo.SegundoApellido)
                    ? null
                    : modelo.SegundoApellido.Trim();
        }
    }
}