using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Correo;
using System.Net;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
	{
        private readonly IEnviarCorreoService enviarCorreoService;

        public CorreoController(IEnviarCorreoService enviarCorreoService)
        {
            this.enviarCorreoService = enviarCorreoService;
        }

        [HttpPost]
        [Route("enviar-correo")]
        public async Task<IActionResult> NotificarCorreo([FromBody] NotificarCorreoDTO dto)
        {
            await enviarCorreoService.NotificarCorreoAsync(dto);
            return Ok(new { message = "Correo enviado exitosamente" });
        }
    }
}