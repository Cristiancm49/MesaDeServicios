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

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OracleController : ControllerBase
    {
        private readonly IOracleService oracleService;

        public OracleController(IOracleService oracleService)
        {
            this.oracleService = oracleService;
        }
        
        [HttpGet("Cotratos_Oracle")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarContratosActivos(string documentoIdentidad)
        {
            var respuesta = await oracleService.ConsultarContratosActivosAsync(documentoIdentidad);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("IdCotratos_Oracle/{ContId}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarIdContrato(int ContId)
        {
            var respuesta = await oracleService.ConsultarIdContratoAsync(ContId);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}