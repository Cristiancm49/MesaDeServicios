using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using MicroApi.Seguridad.Domain.Models.zEjemplos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IIncidenciaRepository
    {
        Task<RespuestaGeneral> ConsultarContratoAsync(int IdContrato);
        Task<RespuestaGeneral> ConsultarCategoriaAreaTecnicaAsync();
        Task<RespuestaGeneral> ConsultarAreaTecnicaAsync(int CategoriaId);
        Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync();
        Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarTipoPrioridadesAsync();
        Task<RespuestaGeneral> CambiarPrioridadAsync(CambiarPrioridadDTO dto);
        Task<RespuestaGeneral> ConsultarRolesUsuariosAsync();
        Task<RespuestaGeneral> ConsultarUsuariosAsync(int? usRoId = null);
        Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
        Task<RespuestaGeneral> ResolverIncidenciaAsync(ResolverIncidenciaDTO dto);
        Task<RespuestaGeneral> EvaluarCerrarIncidenciaAsync(EvaluarCerrarIncidenciaDTO dto);
    }
}
