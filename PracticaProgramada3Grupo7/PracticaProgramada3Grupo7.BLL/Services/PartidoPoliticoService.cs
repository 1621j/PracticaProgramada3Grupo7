using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.DAL.Entidades;
using PracticaProgramada3Grupo7.DAL.Repositorios;

namespace PracticaProgramada3Grupo7.BLL.Services
{
    public class PartidoPoliticoService
    {
        private readonly PartidoPoliticoRepositorio
            _partidoRepositorio;

        public PartidoPoliticoService(
            PartidoPoliticoRepositorio partidoRepositorio)
        {
            _partidoRepositorio = partidoRepositorio;
        }

        public async Task<List<PartidoPoliticoDto>>
            ObtenerTodosAsync()
        {
            List<PartidoPolitico> partidos =
                await _partidoRepositorio.ObtenerTodosAsync();

            return partidos
                .Select(
                    MapeoClases.ConvertirAPartidoPoliticoDto)
                .ToList();
        }

        public async Task<List<PartidoPoliticoDto>>
            ObtenerActivosAsync()
        {
            List<PartidoPolitico> partidos =
                await _partidoRepositorio.ObtenerActivosAsync();

            return partidos
                .Select(
                    MapeoClases.ConvertirAPartidoPoliticoDto)
                .ToList();
        }

        public async Task<PartidoPoliticoDto?>
            ObtenerPorIdAsync(int id)
        {
            PartidoPolitico? partido =
                await _partidoRepositorio.ObtenerPorIdAsync(id);

            return partido == null
                ? null
                : MapeoClases
                    .ConvertirAPartidoPoliticoDto(partido);
        }

        public async Task<PartidoPoliticoDto> AgregarAsync(
            PartidoPoliticoDto partidoDto)
        {
            partidoDto.Siglas =
                partidoDto.Siglas.Trim().ToUpper();

            bool siglasExisten =
                await _partidoRepositorio.ExistenSiglasAsync(
                    partidoDto.Siglas);

            if (siglasExisten)
            {
                throw new InvalidOperationException(
                    "Ya existe un partido con esas siglas.");
            }

            PartidoPolitico partido =
                MapeoClases.ConvertirAPartidoPolitico(
                    partidoDto);

            PartidoPolitico partidoCreado =
                await _partidoRepositorio.AgregarAsync(partido);

            return MapeoClases
                .ConvertirAPartidoPoliticoDto(partidoCreado);
        }

        public async Task<bool> ActualizarAsync(
            int id,
            PartidoPoliticoDto partidoDto)
        {
            PartidoPolitico? existente =
                await _partidoRepositorio.ObtenerPorIdAsync(id);

            if (existente == null)
            {
                return false;
            }

            partidoDto.Siglas =
                partidoDto.Siglas.Trim().ToUpper();

            bool siglasExisten =
                await _partidoRepositorio.ExistenSiglasAsync(
                    partidoDto.Siglas,
                    id);

            if (siglasExisten)
            {
                throw new InvalidOperationException(
                    "Ya existe otro partido con esas siglas.");
            }

            partidoDto.Id = id;

            PartidoPolitico partido =
                MapeoClases.ConvertirAPartidoPolitico(
                    partidoDto);

            return await _partidoRepositorio
                .ActualizarAsync(partido);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            bool tieneVotos =
                await _partidoRepositorio
                    .TieneVotosRegistradosAsync(id);

            if (tieneVotos)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar un partido que tiene votos.");
            }

            return await _partidoRepositorio.EliminarAsync(id);
        }
    }
}