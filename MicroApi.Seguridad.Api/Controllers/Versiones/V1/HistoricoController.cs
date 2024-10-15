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

        [HttpGet("consultar-MisIncidenciasCerradas/{IdContrato}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarMisIncidenciaCerradas(int IdContrato)
        {
            var respuesta = await historicoService.ConsultarMisIncidenciaCerradasAsync(IdContrato);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-MisSolicitudes/{IdContrato}&{estado}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarMisSolicitudes(int IdContrato, bool estado)
        {
            var respuesta = await historicoService.ConsultarMisSolicitudesAsync(IdContrato, estado);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-EvaluacionIncidencia/{Inci_Id}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarEvaluacionIncidencia(int Inci_Id)
        {
            var respuesta = await historicoService.ConsultarEvaluacionIncidenciaAsync(Inci_Id);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}
