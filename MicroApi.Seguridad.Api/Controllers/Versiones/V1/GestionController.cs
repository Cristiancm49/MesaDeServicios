using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class GestionController : ControllerBase
    {
        private readonly IIncidenciaService incidenciaService;

        public GestionController(IIncidenciaService incidenciaService)
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

        [HttpPost("cambiarPrioridad-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> CambiarPrioridad([FromBody] CambiarPrioridadDTO dto)
        {
            var respuesta = await incidenciaService.CambiarPrioridadAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-rolesUsuarios")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarRolesUsuarios()
        {
            var respuesta = await incidenciaService.ConsultarRolesUsuariosAsync();

            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }

            return Ok(respuesta);
        }

        [HttpGet("consultar-usuario")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarUsuarios(int? usRoId = null)
        {
            var respuesta = await incidenciaService.ConsultarUsuariosAsync(usRoId);

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

        [HttpPost("resolver-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> ResolverIncidencia([FromBody] ResolverIncidenciaDTO dto)
        {
            var respuesta = await incidenciaService.ResolverIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}