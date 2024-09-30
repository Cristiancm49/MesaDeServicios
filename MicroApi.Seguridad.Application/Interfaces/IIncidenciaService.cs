using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IIncidenciaService
    {
        Task<RespuestaGeneral> ConsultarContratoAsync(long documentoPersona);
        Task<RespuestaGeneral> ConsultarCategoriaAreaTecnicaAsync();
        Task<RespuestaGeneral> ConsultarAreaTecnicaAsync(int CategoriaId);
        Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync();
        Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto);
        Task<RespuestaGeneral> CambiarPrioridadAsync(CambiarPrioridadDTO dto);
        Task<RespuestaGeneral> ConsultarRolesUsuariosAsync();
        Task<RespuestaGeneral> ConsultarUsuariosAsync(int? usRoId = null);
        Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
        Task<RespuestaGeneral> ResolverIncidenciaAsync(ResolverIncidenciaDTO dto);
    }
}