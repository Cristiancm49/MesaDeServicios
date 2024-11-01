using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IHistoricoService
    {
        Task<RespuestaGeneral> ConsultarHistoricoIncidenciasAsync();
        Task<RespuestaGeneral> ConsultarMisIncidenciaCerradasAsync(int IdContrato);
        Task<RespuestaGeneral> ConsultarMisSolicitudesAsync(int IdContrato, bool estado);
        Task<RespuestaGeneral> ConsultarEvaluacionIncidenciaAsync(int Inci_Id);
    }
}
