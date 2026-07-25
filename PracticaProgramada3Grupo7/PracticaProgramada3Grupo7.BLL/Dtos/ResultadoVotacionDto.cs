namespace PracticaProgramada3Grupo7.BLL.Dtos
{
    public class ResultadoVotacionDto
    {
        public int PartidoPoliticoId { get; set; }

        public string NombrePartido { get; set; } = string.Empty;

        public string Siglas { get; set; } = string.Empty;

        public string Candidato { get; set; } = string.Empty;

        public int CantidadVotos { get; set; }

        public decimal Porcentaje { get; set; }
    }
}