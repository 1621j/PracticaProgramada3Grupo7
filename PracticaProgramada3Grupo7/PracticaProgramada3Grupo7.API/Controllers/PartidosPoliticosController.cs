using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.BLL.Services;

namespace PracticaProgramada3Grupo7.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartidosPoliticosController : ControllerBase
    {
        private readonly PartidoPoliticoService _partidoService;

        public PartidosPoliticosController(
            PartidoPoliticoService partidoService)
        {
            _partidoService = partidoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PartidoPoliticoDto>>>
            ObtenerTodos()
        {
            return Ok(await _partidoService.ObtenerTodosAsync());
        }

        [HttpGet("activos")]
        public async Task<
            ActionResult<List<PartidoPoliticoDto>>>
            ObtenerActivos()
        {
            return Ok(
                await _partidoService.ObtenerActivosAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PartidoPoliticoDto>>
            ObtenerPorId(int id)
        {
            PartidoPoliticoDto? partido =
                await _partidoService.ObtenerPorIdAsync(id);

            if (partido == null)
            {
                return NotFound(new
                {
                    mensaje = "El partido no existe."
                });
            }

            return Ok(partido);
        }

        [HttpPost]
        public async Task<ActionResult<PartidoPoliticoDto>>
            Agregar(PartidoPoliticoDto partidoDto)
        {
            try
            {
                PartidoPoliticoDto partidoCreado =
                    await _partidoService
                        .AgregarAsync(partidoDto);

                return CreatedAtAction(
                    nameof(ObtenerPorId),
                    new { id = partidoCreado.Id },
                    partidoCreado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(
            int id,
            PartidoPoliticoDto partidoDto)
        {
            try
            {
                bool actualizado =
                    await _partidoService
                        .ActualizarAsync(id, partidoDto);

                if (!actualizado)
                {
                    return NotFound(new
                    {
                        mensaje = "El partido no existe."
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
                    await _partidoService.EliminarAsync(id);

                if (!eliminado)
                {
                    return NotFound(new
                    {
                        mensaje = "El partido no existe."
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