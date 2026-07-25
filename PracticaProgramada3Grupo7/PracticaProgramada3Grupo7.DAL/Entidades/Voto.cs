namespace PracticaProgramada3Grupo7.DAL.Entidades
{
    public class Voto
    {
        public int Id { get; set; }

        public int VotanteId { get; set; }

        public int PartidoPoliticoId { get; set; }

        public DateTime FechaHora { get; set; } = DateTime.Now;

        public Votante Votante { get; set; } = null!;

        public PartidoPolitico PartidoPolitico { get; set; } = null!;
    }
}