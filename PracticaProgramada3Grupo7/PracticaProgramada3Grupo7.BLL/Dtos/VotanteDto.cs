using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada3Grupo7.BLL.Dtos
{
    public class VotanteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [MaxLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El primer apellido es obligatorio.")]
        [MaxLength(100)]
        public string PrimerApellido { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? SegundoApellido { get; set; }

        public bool EstaActivo { get; set; } = true;

        public bool YaVoto { get; set; }
    }
}