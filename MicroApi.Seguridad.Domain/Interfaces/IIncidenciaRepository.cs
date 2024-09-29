using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IIncidenciaRepository
    {
        Task<RespuestaGeneral> ConsultarContratoAsync(long documentoPersona);
        Task<RespuestaGeneral> ConsultarAreaTecnicaYCategoriaAsync();
        Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync();
        Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto);
        Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
    }
}
