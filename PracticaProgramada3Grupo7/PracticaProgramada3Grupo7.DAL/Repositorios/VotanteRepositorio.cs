using Microsoft.EntityFrameworkCore;
using PracticaProgramada3Grupo7.DAL.Data;
using PracticaProgramada3Grupo7.DAL.Entidades;

namespace PracticaProgramada3Grupo7.DAL.Repositorios
{
    public class VotanteRepositorio
    {
        private readonly ApplicationDbContext _context;

        public VotanteRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Votante>> ObtenerTodosAsync()
        {
            return await _context.Votantes
                .AsNoTracking()
                .Include(votante => votante.Voto)
                .OrderBy(votante => votante.PrimerApellido)
                .ThenBy(votante => votante.Nombre)
                .ToListAsync();
        }

        public async Task<Votante?> ObtenerPorIdAsync(int id)
        {
            return await _context.Votantes
                .AsNoTracking()
                .FirstOrDefaultAsync(votante => votante.Id == id);
        }

        public async Task<Votante?> ObtenerPorCedulaAsync(string cedula)
        {
            return await _context.Votantes
                .AsNoTracking()
                .Include(votante => votante.Voto)
                .FirstOrDefaultAsync(votante => votante.Cedula == cedula);
        }

        public async Task<bool> ExisteCedulaAsync(
            string cedula,
            int? idExcluido = null)
        {
            return await _context.Votantes.AnyAsync(votante =>
                votante.Cedula == cedula &&
                (!idExcluido.HasValue || votante.Id != idExcluido.Value));
        }

        public async Task<Votante> AgregarAsync(Votante votante)
        {
            _context.Votantes.Add(votante);
            await _context.SaveChangesAsync();

            return votante;
        }

        public async Task<bool> ActualizarAsync(Votante votante)
        {
            Votante? votanteExistente =
                await _context.Votantes.FindAsync(votante.Id);

            if (votanteExistente == null)
            {
                return false;
            }

            votanteExistente.Cedula = votante.Cedula;
            votanteExistente.Nombre = votante.Nombre;
            votanteExistente.PrimerApellido = votante.PrimerApellido;
            votanteExistente.SegundoApellido = votante.SegundoApellido;
            votanteExistente.EstaActivo = votante.EstaActivo;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            Votante? votante =
                await _context.Votantes.FindAsync(id);

            if (votante == null)
            {
                return false;
            }

            _context.Votantes.Remove(votante);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TieneVotoRegistradoAsync(int votanteId)
        {
            return await _context.Votos
                .AnyAsync(voto => voto.VotanteId == votanteId);
        }
    }
}