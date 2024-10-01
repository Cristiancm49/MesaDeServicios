using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface ITrazabilidadRepository
    {
        Task<RespuestaGeneral> ConsultarMisIncidenciasActivasAsync(long documentoIdentidad);
        Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto);
        Task<RespuestaGeneral> ReAsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarEscalarInternoIncidenciaAsync(long documentoIdentidad);
        Task<RespuestaGeneral> EscalarInternoIncidenciaAsync(AsignarIncidenciaDTO dto);
    }
}