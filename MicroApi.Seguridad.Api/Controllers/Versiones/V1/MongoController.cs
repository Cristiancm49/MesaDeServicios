using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.Data.SqlClient;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Evidencias;
using MongoDB.Bson;

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
        public async Task<ActionResult<RespuestaGeneral>> InsertarEvidencia([FromForm] InsertarEvidenciaDTO dto, [FromForm] IFormFile soporte)
        {
            // Verificar si el archivo es nulo
            if (soporte == null || soporte.Length == 0)
            {
                return BadRequest("No se ha subido ningún archivo.");
            }

            // Convertir el archivo IFormFile a un arreglo de bytes
            using var memoryStream = new MemoryStream();
            await soporte.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            // Aquí puedes convertir los bytes a BsonBinaryData
            var bsonData = new BsonBinaryData(fileBytes);

            // No necesitas asignar BsonBinaryData al DTO, solo pasa los bytes al repositorio
            var resultado = await evidenciaService.InsertarEvidenciaAsync(dto, soporte); // El método ahora acepta IFormFile

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