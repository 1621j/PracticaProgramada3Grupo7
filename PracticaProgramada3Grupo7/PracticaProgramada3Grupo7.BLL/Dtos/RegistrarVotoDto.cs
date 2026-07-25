using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada3Grupo7.BLL.Dtos
{
    public class RegistrarVotoDto
    {
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        public string Cedula { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un partido.")]
        public int PartidoPoliticoId { get; set; }
    }
}