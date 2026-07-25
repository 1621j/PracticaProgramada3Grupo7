using Microsoft.AspNetCore.Mvc;
using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.BLL.Services;

namespace PracticaProgramada3Grupo7.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VotacionController : ControllerBase
    {
        private readonly VotacionService _votacionService;

        public VotacionController(
            VotacionService votacionService)
        {
            _votacionService = votacionService;
        }

        [HttpPost]
        public async Task<ActionResult<RespuestaVotoDto>>
            RegistrarVoto(RegistrarVotoDto registrarVotoDto)
        {
            RespuestaVotoDto respuesta =
                await _votacionService
                    .RegistrarVotoAsync(registrarVotoDto);

            if (!respuesta.Exitoso)
            {
                return BadRequest(respuesta);
            }

            return Ok(respuesta);
        }

        [HttpGet("resultados")]
        public async Task<
            ActionResult<List<ResultadoVotacionDto>>>
            ObtenerResultados()
        {
            return Ok(
                await _votacionService
                    .ObtenerResultadosAsync());
        }
    }
}