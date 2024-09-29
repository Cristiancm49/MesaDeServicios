using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UnidadController : ControllerBase
    {
        private readonly IUnidadService unidadService;

        public UnidadController(IUnidadService unidadService)
        {
            this.unidadService = unidadService;
        }

        [HttpGet]
        public async Task<ActionResult<RespuestaGeneral>> GetUnidades() // Cambiar el tipo de retorno
        {
            var respuesta = await unidadService.GetUnidadesAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer); // Devuelve 404 con el mensaje de respuesta
            }
            return Ok(respuesta); // Devuelve 200 con la respuesta general
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaGeneral>> GetUnidadById(int id) // Cambiar el tipo de retorno
        {
            var respuesta = await unidadService.GetUnidadByIdAsync(id);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer); // Devuelve 404 con el mensaje de respuesta
            }
            return Ok(respuesta); // Devuelve 200 con la respuesta general
        }
    }
}
