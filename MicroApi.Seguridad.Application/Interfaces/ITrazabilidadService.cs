using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface ITrazabilidadService
    {
        Task<RespuestaGeneral> ConsultarMisIncidenciasActivasAsync(long documentoIdentidad);
        Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto);
        Task<RespuestaGeneral> ReAsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarEscalarInternoIncidenciaAsync(long documentoIdentidad);
        Task<RespuestaGeneral> EscalarInternoIncidenciaAsync(AsignarIncidenciaDTO dto);
        Task<RespuestaGeneral> EscalarExternoIncidenciaAsync(EscalarExternoIncidenciaDTO dto);
    }
}