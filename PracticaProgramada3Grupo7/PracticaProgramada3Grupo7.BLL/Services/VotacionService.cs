using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.DAL.Entidades;
using PracticaProgramada3Grupo7.DAL.Repositorios;

namespace PracticaProgramada3Grupo7.BLL.Services
{
    public class VotacionService
    {
        private readonly VotanteRepositorio
            _votanteRepositorio;

        private readonly PartidoPoliticoRepositorio
            _partidoRepositorio;

        private readonly VotoRepositorio
            _votoRepositorio;

        public VotacionService(
            VotanteRepositorio votanteRepositorio,
            PartidoPoliticoRepositorio partidoRepositorio,
            VotoRepositorio votoRepositorio)
        {
            _votanteRepositorio = votanteRepositorio;
            _partidoRepositorio = partidoRepositorio;
            _votoRepositorio = votoRepositorio;
        }

        public async Task<RespuestaVotoDto> RegistrarVotoAsync(
            RegistrarVotoDto registrarVotoDto)
        {
            string cedula =
                registrarVotoDto.Cedula.Trim();

            Votante? votante =
                await _votanteRepositorio
                    .ObtenerPorCedulaAsync(cedula);

            if (votante == null)
            {
                return CrearRespuesta(
                    false,
                    "La cédula no está inscrita.");
            }

            if (!votante.EstaActivo)
            {
                return CrearRespuesta(
                    false,
                    "El votante se encuentra inactivo.");
            }

            bool yaVoto =
                await _votoRepositorio
                    .ExisteVotoDelVotanteAsync(votante.Id);

            if (yaVoto)
            {
                return CrearRespuesta(
                    false,
                    "Este votante ya registró su voto.");
            }

            PartidoPolitico? partido =
                await _partidoRepositorio.ObtenerPorIdAsync(
                    registrarVotoDto.PartidoPoliticoId);

            if (partido == null)
            {
                return CrearRespuesta(
                    false,
                    "El partido seleccionado no existe.");
            }

            if (!partido.EstaActivo)
            {
                return CrearRespuesta(
                    false,
                    "El partido seleccionado está inactivo.");
            }

            Voto voto = new Voto
            {
                VotanteId = votante.Id,
                PartidoPoliticoId = partido.Id,
                FechaHora = DateTime.Now
            };

            await _votoRepositorio.AgregarAsync(voto);

            return CrearRespuesta(
                true,
                "El voto se registró correctamente.");
        }

        public async Task<List<ResultadoVotacionDto>>
            ObtenerResultadosAsync()
        {
            List<PartidoPolitico> partidos =
                await _votoRepositorio.ObtenerResultadosAsync();

            int totalVotos =
                await _votoRepositorio.ObtenerTotalVotosAsync();

            return partidos
                .Select(partido =>
                    MapeoClases.ConvertirAResultadoDto(
                        partido,
                        totalVotos))
                .ToList();
        }

        private static RespuestaVotoDto CrearRespuesta(
            bool exitoso,
            string mensaje)
        {
            return new RespuestaVotoDto
            {
                Exitoso = exitoso,
                Mensaje = mensaje
            };
        }
    }
}