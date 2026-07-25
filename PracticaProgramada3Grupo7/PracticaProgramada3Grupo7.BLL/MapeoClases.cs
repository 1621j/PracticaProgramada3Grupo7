using PracticaProgramada3Grupo7.BLL.Dtos;
using PracticaProgramada3Grupo7.DAL.Entidades;

namespace PracticaProgramada3Grupo7.BLL
{
    public static class MapeoClases
    {
        public static VotanteDto ConvertirAVotanteDto(
            Votante votante)
        {
            return new VotanteDto
            {
                Id = votante.Id,
                Cedula = votante.Cedula,
                Nombre = votante.Nombre,
                PrimerApellido = votante.PrimerApellido,
                SegundoApellido = votante.SegundoApellido,
                EstaActivo = votante.EstaActivo,
                YaVoto = votante.Voto != null
            };
        }

        public static Votante ConvertirAVotante(
            VotanteDto votanteDto)
        {
            return new Votante
            {
                Id = votanteDto.Id,
                Cedula = votanteDto.Cedula.Trim(),
                Nombre = votanteDto.Nombre.Trim(),
                PrimerApellido = votanteDto.PrimerApellido.Trim(),
                SegundoApellido =
                    votanteDto.SegundoApellido?.Trim(),
                EstaActivo = votanteDto.EstaActivo
            };
        }

        public static PartidoPoliticoDto
            ConvertirAPartidoPoliticoDto(
                PartidoPolitico partidoPolitico)
        {
            return new PartidoPoliticoDto
            {
                Id = partidoPolitico.Id,
                Nombre = partidoPolitico.Nombre,
                Siglas = partidoPolitico.Siglas,
                Candidato = partidoPolitico.Candidato,
                Descripcion = partidoPolitico.Descripcion,
                EstaActivo = partidoPolitico.EstaActivo
            };
        }

        public static PartidoPolitico
            ConvertirAPartidoPolitico(
                PartidoPoliticoDto partidoDto)
        {
            return new PartidoPolitico
            {
                Id = partidoDto.Id,
                Nombre = partidoDto.Nombre.Trim(),
                Siglas = partidoDto.Siglas.Trim().ToUpper(),
                Candidato = partidoDto.Candidato.Trim(),
                Descripcion = partidoDto.Descripcion?.Trim(),
                EstaActivo = partidoDto.EstaActivo
            };
        }

        public static ResultadoVotacionDto
            ConvertirAResultadoDto(
                PartidoPolitico partido,
                int totalVotos)
        {
            int cantidadVotos = partido.Votos.Count;

            decimal porcentaje = totalVotos == 0
                ? 0
                : Math.Round(
                    (decimal)cantidadVotos / totalVotos * 100,
                    2);

            return new ResultadoVotacionDto
            {
                PartidoPoliticoId = partido.Id,
                NombrePartido = partido.Nombre,
                Siglas = partido.Siglas,
                Candidato = partido.Candidato,
                CantidadVotos = cantidadVotos,
                Porcentaje = porcentaje
            };
        }
    }
}