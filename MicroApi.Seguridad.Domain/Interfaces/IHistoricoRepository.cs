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
    public interface IHistoricoRepository
    {
        Task<RespuestaGeneral> ConsultarHistoricoIncidenciasAsync();
        Task<RespuestaGeneral> ConsultarMisIncidenciaCerradasAsync(int IdContrato);
        Task<RespuestaGeneral> ConsultarMisSolicitudesAsync(int IdContrato, bool estado);
        Task<RespuestaGeneral> ConsultarEvaluacionIncidenciaAsync(int Inci_Id);
    }
}
