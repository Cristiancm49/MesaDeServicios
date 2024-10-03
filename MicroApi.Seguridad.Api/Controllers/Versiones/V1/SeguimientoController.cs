using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class SeguimientoController : ControllerBase
    {
        private readonly ISeguimientoService seguimientoService;

        public SeguimientoController(ISeguimientoService seguimientoService)
        {
            this.seguimientoService = seguimientoService;
        }

        [HttpGet("consultar-SeguimientoIncidencias")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarSeguimientoIncidencia()
        {
            var respuesta = await seguimientoService.ConsultarSeguimientoIncidenciaAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-TrazabilidadIncidencias/{incidenciaId}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarTrazabilidadIncidencia(int incidenciaId)
        {
            var respuesta = await seguimientoService.ConsultarTrazabilidadIncidenciaAsync(incidenciaId);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}
