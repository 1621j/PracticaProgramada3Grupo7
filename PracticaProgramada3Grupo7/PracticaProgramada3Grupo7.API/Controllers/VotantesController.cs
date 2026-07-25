using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.BLL.Services;

namespace PracticaProgramada3Grupo7.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotantesController : ControllerBase
    {
        private readonly VotanteService _votanteService;

        public VotantesController(VotanteService votanteService)
        {
            _votanteService = votanteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VotanteDto>>>
            ObtenerTodos()
        {
            return Ok(await _votanteService.ObtenerTodosAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VotanteDto>>
            ObtenerPorId(int id)
        {
            VotanteDto? votante =
                await _votanteService.ObtenerPorIdAsync(id);

            if (votante == null)
            {
                return NotFound(new
                {
                    mensaje = "El votante no existe."
                });
            }

            return Ok(votante);
        }

        [HttpGet("cedula/{cedula}")]
        public async Task<ActionResult<VotanteDto>>
            ObtenerPorCedula(string cedula)
        {
            VotanteDto? votante =
                await _votanteService
                    .ObtenerPorCedulaAsync(cedula);

            if (votante == null)
            {
                return NotFound(new
                {
                    mensaje = "La cédula no está inscrita."
                });
            }

            return Ok(votante);
        }

        [HttpPost]
        public async Task<ActionResult<VotanteDto>> Agregar(
            VotanteDto votanteDto)
        {
            try
            {
                VotanteDto votanteCreado =
                    await _votanteService
                        .AgregarAsync(votanteDto);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = votanteCreado.Id },
                    votanteCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(
            int id,
            VotanteDto votanteDto)
        {
            try
            {
                bool actualizado =
                    await _votanteService
                        .ActualizarAsync(id, votanteDto);

                if (!actualizado)
                {
                    return NotFound(new
                    {
                        mensaje = "El votante no existe."
                    });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                bool eliminado =
                    await _votanteService.EliminarAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        mensaje = "El votante no existe."
                    });
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}