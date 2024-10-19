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
        public async Task<ActionResult<RespuestaGeneral>> InsertarEvidencia([FromBody] InsertarEvidenciaDTO dto)
        {
            var respuesta = await evidenciaService.InsertarEvidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
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