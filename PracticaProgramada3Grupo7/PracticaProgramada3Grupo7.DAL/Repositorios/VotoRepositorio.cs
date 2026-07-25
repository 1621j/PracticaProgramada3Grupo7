using Microsoft.EntityFrameworkCore;
using PracticaProgramada3Grupo7.DAL.Data;
using PracticaProgramada3Grupo7.DAL.Entidades;

namespace PracticaProgramada3Grupo7.DAL.Repositorios
{
    public class VotoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public VotoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteVotoDelVotanteAsync(int votanteId)
        {
            return await _context.Votos
                .AnyAsync(voto => voto.VotanteId == votanteId);
        }

        public async Task<Voto> AgregarAsync(Voto voto)
        {
            _context.Votos.Add(voto);
            await _context.SaveChangesAsync();

            return voto;
        }

        public async Task<Voto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Votos
                .AsNoTracking()
                .Include(voto => voto.Votante)
                .Include(voto => voto.PartidoPolitico)
                .FirstOrDefaultAsync(voto => voto.Id == id);
        }

        public async Task<List<Voto>> ObtenerTodosAsync()
        {
            return await _context.Votos
                .AsNoTracking()
                .Include(voto => voto.Votante)
                .Include(voto => voto.PartidoPolitico)
                .OrderByDescending(voto => voto.FechaHora)
                .ToListAsync();
        }

        public async Task<List<PartidoPolitico>>
            ObtenerResultadosAsync()
        {
            return await _context.PartidosPoliticos
                .AsNoTracking()
                .Include(partido => partido.Votos)
                .OrderByDescending(partido => partido.Votos.Count)
                .ThenBy(partido => partido.Nombre)
                .ToListAsync();
        }

        public async Task<int> ObtenerTotalVotosAsync()
        {
            return await _context.Votos.CountAsync();
        }
    }
}