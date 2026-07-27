using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PracticaProgramada3Grupo7.Models
{
    public class RegistrarVotoViewModel
    {
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        [Display(Name = "Cédula")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "La cédula debe contener exactamente 9 números, sin guiones ni espacios.")]
        [Remote(action: "VerifyCedula", controller: "Votacion")]
        public string Cedula { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un partido.")]
        [Display(Name = "Partido político")]
        public int PartidoPoliticoId { get; set; }
    }
}
