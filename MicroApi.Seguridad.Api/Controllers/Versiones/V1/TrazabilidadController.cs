using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class TrazabilidadController : ControllerBase
    {
        private readonly ITrazabilidadService trazabilidadService;

        public TrazabilidadController(ITrazabilidadService trazabilidadService)
        {
            this.trazabilidadService = trazabilidadService;
        }

        [HttpGet("consultar-MisIncidenciasActivas/{documentoIdentidad}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarMisIncidenciasActivas(long documentoIdentidad)
        {
            var respuesta = await trazabilidadService.ConsultarMisIncidenciasActivasAsync(documentoIdentidad);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("generar-Diagnostico")]
        public async Task<ActionResult<RespuestaGeneral>> GenerarDiagnostico([FromBody] GenerarDiagnosticoDTO dto)
        {
            var respuesta = await trazabilidadService.GenerarDiagnosticoAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}