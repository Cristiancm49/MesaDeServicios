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
    public class GestionIncidenciaController : ControllerBase
    {
        private readonly IIncidenciaService incidenciaService;

        public GestionIncidenciaController(IIncidenciaService incidenciaService)
        {
            this.incidenciaService = incidenciaService;
        }

        [HttpGet("consultar-IncidenciasResgistradas")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarIncidenciasRegistradas()
        {
            var respuesta = await incidenciaService.ConsultarIncidenciasRegistradasAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("rechazar-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> RechazarIncidencia([FromBody] RechazarIncidenciaDTO dto)
        {
            var respuesta = await incidenciaService.RechazarIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("asignar-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> AsignarIncidencia([FromBody] AsignarIncidenciaDTO dto)
        {
            var respuesta = await incidenciaService.AsignarIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}