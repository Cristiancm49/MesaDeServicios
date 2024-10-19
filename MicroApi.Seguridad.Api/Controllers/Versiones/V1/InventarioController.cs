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
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioService inventarioService;

        public InventarioController(IInventarioService inventarioService)
        {
            this.inventarioService = inventarioService;
        }

        [HttpGet("consultar-TipoEvento")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarTipoEventosAsync()
        {
            var respuesta = await inventarioService.ConsultarTipoEventosAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}