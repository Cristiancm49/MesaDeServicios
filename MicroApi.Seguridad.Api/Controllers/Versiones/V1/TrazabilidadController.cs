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

        [HttpGet("consultar-TiposDeSolucion")]
        public async Task<ActionResult<RespuestaGeneral>> ConstarTipoSolucion()
        {
            var respuesta = await trazabilidadService.ConsultarTipoSolucionAsync();
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

        [HttpPost("reAsignar-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> ReAsignarIncidencia([FromBody] AsignarIncidenciaDTO dto)
        {
            var respuesta = await trazabilidadService.ReAsignarIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-escalarInterno/{documentoIdentidad}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarEscalarInternoIncidencia(long documentoIdentidad)
        {
            var respuesta = await trazabilidadService.ConsultarEscalarInternoIncidenciaAsync(documentoIdentidad);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("escalarInterno-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> EscalarInternoIncidencia([FromBody] AsignarIncidenciaDTO dto)
        {
            var respuesta = await trazabilidadService.EscalarInternoIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("escalarExterno-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> EscalarExternoIncidencia([FromBody] EscalarExternoIncidenciaDTO dto)
        {
            var respuesta = await trazabilidadService.EscalarExternoIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}