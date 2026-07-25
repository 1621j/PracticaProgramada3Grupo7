using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.DAL.Entidades;
using PracticaProgramada3Grupo7.DAL.Repositorios;

namespace PracticaProgramada3Grupo7.BLL.Services
{
    public class VotanteService
    {
        private readonly VotanteRepositorio _votanteRepositorio;

        public VotanteService(
            VotanteRepositorio votanteRepositorio)
        {
            _votanteRepositorio = votanteRepositorio;
        }

        public async Task<List<VotanteDto>> ObtenerTodosAsync()
        {
            List<Votante> votantes =
                await _votanteRepositorio.ObtenerTodosAsync();

            return votantes
                .Select(MapeoClases.ConvertirAVotanteDto)
                .ToList();
        }

        public async Task<VotanteDto?> ObtenerPorIdAsync(int id)
        {
            Votante? votante =
                await _votanteRepositorio.ObtenerPorIdAsync(id);

            if (votante == null)
            {
                return null;
            }

            bool yaVoto =
                await _votanteRepositorio
                    .TieneVotoRegistradoAsync(id);

            VotanteDto votanteDto =
                MapeoClases.ConvertirAVotanteDto(votante);

            votanteDto.YaVoto = yaVoto;

            return votanteDto;
        }

        public async Task<VotanteDto?> ObtenerPorCedulaAsync(
            string cedula)
        {
            string cedulaLimpia = cedula.Trim();

            Votante? votante =
                await _votanteRepositorio
                    .ObtenerPorCedulaAsync(cedulaLimpia);

            return votante == null
                ? null
                : MapeoClases.ConvertirAVotanteDto(votante);
        }

        public async Task<VotanteDto> AgregarAsync(
            VotanteDto votanteDto)
        {
            votanteDto.Cedula = votanteDto.Cedula.Trim();

            bool cedulaExiste =
                await _votanteRepositorio.ExisteCedulaAsync(
                    votanteDto.Cedula);

            if (cedulaExiste)
            {
                throw new InvalidOperationException(
                    "Ya existe un votante con esa cédula.");
            }

            Votante votante =
                MapeoClases.ConvertirAVotante(votanteDto);

            Votante votanteCreado =
                await _votanteRepositorio.AgregarAsync(votante);

            return MapeoClases.ConvertirAVotanteDto(
                votanteCreado);
        }

        public async Task<bool> ActualizarAsync(
            int id,
            VotanteDto votanteDto)
        {
            Votante? existente =
                await _votanteRepositorio.ObtenerPorIdAsync(id);

            if (existente == null)
            {
                return false;
            }

            votanteDto.Cedula = votanteDto.Cedula.Trim();

            bool cedulaExiste =
                await _votanteRepositorio.ExisteCedulaAsync(
                    votanteDto.Cedula,
                    id);

            if (cedulaExiste)
            {
                throw new InvalidOperationException(
                    "Ya existe otro votante con esa cédula.");
            }

            votanteDto.Id = id;

            Votante votante =
                MapeoClases.ConvertirAVotante(votanteDto);

            return await _votanteRepositorio
                .ActualizarAsync(votante);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            bool tieneVoto =
                await _votanteRepositorio
                    .TieneVotoRegistradoAsync(id);

            if (tieneVoto)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar un votante que ya votó.");
            }

            return await _votanteRepositorio.EliminarAsync(id);
        }
    }
}