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
    public class InsertarController : ControllerBase
    {
        private readonly IIncidenciaService incidenciaService;

        public InsertarController(IIncidenciaService incidenciaService)
        {
            this.incidenciaService = incidenciaService;
        }

        [HttpGet("consultar-Contrato/{IdContrato}")]//Complemento a Oracle
        public async Task<ActionResult<RespuestaGeneral>> ConsultarContrato(int IdContrato)
        {
            var respuesta = await incidenciaService.ConsultarContratoAsync(IdContrato);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-CategoriaAreaTecnica")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarCategoriaAreaTecnica()
        {
            var respuesta = await incidenciaService.ConsultarCategoriaAreaTecnicaAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-AreaTecnica/{CategoriaId}")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarAreaTecnica(int CategoriaId)
        {
            var respuesta = await incidenciaService.ConsultarAreaTecnicaAsync(CategoriaId);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("insertar-Incidencia")]
        public async Task<ActionResult<RespuestaGeneral>> InsertarIncidencia([FromBody] InsertarIncidenciaDTO dto)
        {
            var respuesta = await incidenciaService.InsertarIncidenciaAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}
