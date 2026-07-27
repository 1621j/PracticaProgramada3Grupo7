using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada3Grupo7.Models
{
    public class PartidoPoliticoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del partido es obligatorio.")]
        [Display(Name = "Nombre del partido")]
        [StringLength(
            150,
            ErrorMessage =
                "El nombre no puede superar los 150 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las siglas son obligatorias.")]
        [StringLength(
            20,
            MinimumLength = 2,
            ErrorMessage =
                "Las siglas deben tener entre 2 y 20 caracteres.")]
        [RegularExpression(
            @"^[A-Za-z0-9ÁÉÍÓÚáéíóúÑñÜü\-]+$",
            ErrorMessage =
                "Las siglas solo pueden contener letras, números o guiones.")]
        public string Siglas { get; set; } = string.Empty;

        [Required(ErrorMessage = "El candidato es obligatorio.")]
        [Display(Name = "Nombre del candidato")]
        [StringLength(
            150,
            ErrorMessage =
                "El candidato no puede superar los 150 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s'.\-]+$",
            ErrorMessage =
                "El candidato solo puede contener letras.")]
        public string Candidato { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [StringLength(
            500,
            ErrorMessage =
                "La descripción no puede superar los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Display(Name = "Partido activo")]
        public bool EstaActivo { get; set; } = true;
    }
}