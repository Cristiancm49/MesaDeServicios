using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.Models.zEjemplos;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly IHistoricoService historicoService;

        public HistoricoController(IHistoricoService historicoService)
        {
            this.historicoService = historicoService;
        }

        [HttpGet("consultar-HistoricoIncidencia")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarHistoricoIncidencias()
        {
            var respuesta = await historicoService.ConsultarHistoricoIncidenciasAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-MisIncidenciasCerradas/{documentoIdentidad}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarMisIncidenciaCerradas(long documentoIdentidad)
        {
            var respuesta = await historicoService.ConsultarMisIncidenciaCerradasAsync(documentoIdentidad);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}
