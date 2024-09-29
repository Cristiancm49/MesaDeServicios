using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TrazabilidadIncidenciaController : ControllerBase
    {
        private readonly ITrazabilidadService trazabilidadService;

        public TrazabilidadIncidenciaController(ITrazabilidadService trazabilidadService)
        {
            this.trazabilidadService = trazabilidadService;
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