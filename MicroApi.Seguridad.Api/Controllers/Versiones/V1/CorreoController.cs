using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Correo;
using Microsoft.AspNetCore.Mvc;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
    {
        private readonly IEnviarCorreoService enviarCorreoService;

        public CorreoController(IEnviarCorreoService enviarCorreoService)
        {
            this.enviarCorreoService = enviarCorreoService;
        }

        [HttpPost("notificar-Incidencia")]
        public async Task<IActionResult> NotificarCorreo([FromBody] NotificarCorreoDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Correo) || string.IsNullOrEmpty(dto.Asunto) || string.IsNullOrEmpty(dto.Mensaje))
            {
                return BadRequest("correo, asunto, y mensaje son requeridos.");
            }

            await enviarCorreoService.NotificarCorreoAsync(dto);
            return Ok("Notificación enviada exitosamente.");
        }
    }
}