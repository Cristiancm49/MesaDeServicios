using MicroApi.Seguridad.Domain.DTOs;
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
        Task<RespuestaGeneral> ConsultarAreaTecnicaYCategoriaAsync();
        Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync();
        Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto);
        Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto);
    }
}