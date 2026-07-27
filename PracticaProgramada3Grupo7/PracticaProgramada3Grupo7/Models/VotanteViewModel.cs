using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada3Grupo7.Models
{
    public class VotanteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [Display(Name = "Cédula")]
        [RegularExpression(
            @"^\d{9}$",
            ErrorMessage =
                "La cédula debe contener exactamente 9 números, sin guiones ni espacios.")]
        public string Cedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(
            100,
            ErrorMessage =
                "El nombre no puede superar los 100 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s'-]+$",
            ErrorMessage =
                "El nombre solo puede contener letras.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(
            ErrorMessage =
                "El primer apellido es obligatorio.")]
        [Display(Name = "Primer apellido")]
        [StringLength(
            100,
            ErrorMessage =
                "El primer apellido no puede superar los 100 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s'-]+$",
            ErrorMessage =
                "El primer apellido solo puede contener letras.")]
        public string PrimerApellido { get; set; }
            = string.Empty;

        [Display(Name = "Segundo apellido")]
        [StringLength(
            100,
            ErrorMessage =
                "El segundo apellido no puede superar los 100 caracteres.")]
        [RegularExpression(
            @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü\s'-]*$",
            ErrorMessage =
                "El segundo apellido solo puede contener letras.")]
        public string? SegundoApellido { get; set; }

        [Display(Name = "Votante activo")]
        public bool EstaActivo { get; set; } = true;

        [Display(Name = "Ya votó")]
        public bool YaVoto { get; set; }
    }
}