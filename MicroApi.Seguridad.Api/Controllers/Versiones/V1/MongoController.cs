using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using System.IO;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        private readonly IEvidenciaService evidenciaService;

        public MongoController(IEvidenciaService evidenciaService)
        {
            this.evidenciaService = evidenciaService;
        }

        [HttpPost("insertar-Evidencias")]
        public async Task<ActionResult<RespuestaGeneral>> InsertarEvidencia([FromForm] int inciId, [FromForm] IFormFile soporte)
        {
            // Verificar si el archivo es nulo
            if (soporte == null || soporte.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            // Llamar al servicio para insertar la evidencia
            var resultado = await evidenciaService.InsertarEvidenciaAsync(inciId, soporte);

            return StatusCode(resultado.StatusCode, resultado);
        }

        [HttpGet("consultar-Evidencias/{inciId}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarEvidencias(int inciId)
        {
            var respuesta = await evidenciaService.ConsultarEvidenciasAsync(inciId);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}