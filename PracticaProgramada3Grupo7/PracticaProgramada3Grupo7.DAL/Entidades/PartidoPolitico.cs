using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada3Grupo7.DAL.Entidades
{
    public class PartidoPolitico
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del partido es obligatorio.")]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las siglas son obligatorias.")]
        [MaxLength(20)]
        public string Siglas { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del candidato es obligatorio.")]
        [MaxLength(150)]
        public string Candidato { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        public bool EstaActivo { get; set; } = true;

        public ICollection<Voto> Votos { get; set; } = new List<Voto>();
    }
}