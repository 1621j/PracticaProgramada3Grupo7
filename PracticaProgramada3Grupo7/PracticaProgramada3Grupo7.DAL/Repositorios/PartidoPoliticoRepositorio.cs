using Microsoft.EntityFrameworkCore;
using PracticaProgramada3Grupo7.DAL.Data;
using PracticaProgramada3Grupo7.DAL.Entidades;

namespace PracticaProgramada3Grupo7.DAL.Repositorios
{
    public class PartidoPoliticoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public PartidoPoliticoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PartidoPolitico>> ObtenerTodosAsync()
        {
            return await _context.PartidosPoliticos
                .AsNoTracking()
                .OrderBy(partido => partido.Nombre)
                .ToListAsync();
        }

        public async Task<List<PartidoPolitico>> ObtenerActivosAsync()
        {
            return await _context.PartidosPoliticos
                .AsNoTracking()
                .Where(partido => partido.EstaActivo)
                .OrderBy(partido => partido.Nombre)
                .ToListAsync();
        }

        public async Task<PartidoPolitico?> ObtenerPorIdAsync(int id)
        {
            return await _context.PartidosPoliticos
                .AsNoTracking()
                .FirstOrDefaultAsync(partido => partido.Id == id);
        }

        public async Task<bool> ExistenSiglasAsync(
            string siglas,
            int? idExcluido = null)
        {
            return await _context.PartidosPoliticos
                .AnyAsync(partido =>
                    partido.Siglas == siglas &&
                    (!idExcluido.HasValue ||
                     partido.Id != idExcluido.Value));
        }

        public async Task<PartidoPolitico> AgregarAsync(
            PartidoPolitico partidoPolitico)
        {
            _context.PartidosPoliticos.Add(partidoPolitico);
            await _context.SaveChangesAsync();

            return partidoPolitico;
        }

        public async Task<bool> ActualizarAsync(
            PartidoPolitico partidoPolitico)
        {
            PartidoPolitico? partidoExistente =
                await _context.PartidosPoliticos
                    .FindAsync(partidoPolitico.Id);

            if (partidoExistente == null)
            {
                return false;
            }

            partidoExistente.Nombre = partidoPolitico.Nombre;
            partidoExistente.Siglas = partidoPolitico.Siglas;
            partidoExistente.Candidato = partidoPolitico.Candidato;
            partidoExistente.Descripcion = partidoPolitico.Descripcion;
            partidoExistente.EstaActivo = partidoPolitico.EstaActivo;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            PartidoPolitico? partidoPolitico =
                await _context.PartidosPoliticos.FindAsync(id);

            if (partidoPolitico == null)
            {
                return false;
            }

            _context.PartidosPoliticos.Remove(partidoPolitico);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TieneVotosRegistradosAsync(int partidoId)
        {
            return await _context.Votos
                .AnyAsync(voto =>
                    voto.PartidoPoliticoId == partidoId);
        }
    }
}