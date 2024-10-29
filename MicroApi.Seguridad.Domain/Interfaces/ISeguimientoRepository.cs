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
    public interface ISeguimientoRepository
    {
        Task<RespuestaGeneral> ConsultarSeguimientoIncidenciaAsync();
        Task<RespuestaGeneral> ConsultarTrazabilidadIncidenciaAsync(int incidenciaId);
        Task<RespuestaGeneral> ConsultarTrazabilidadGeneralAsync(int incidenciaId);
    }
}
