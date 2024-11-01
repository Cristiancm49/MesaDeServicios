using Microsoft.AspNetCore.Mvc;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Domain.DTOs.Usuario;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/Incidencia/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet("consultar-Roles")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarUsuarioRol()
        {
            var respuesta = await usuarioService.ConsultarUsuarioRolAsync();
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpGet("consultar-usuarios")]
        public async Task<ActionResult<RespuestaGeneral>> ConsultarUsuarios(int? UsRoId)
        {
            var respuesta = await usuarioService.ConsultarUsuariosAsync(UsRoId);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPost("insertar-Usuarios")]
        public async Task<ActionResult<RespuestaGeneral>> InsertarUsuario([FromBody] InsertarUsuarioDTO dto)
        {
            var respuesta = await usuarioService.InsertarUsuarioAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }

        [HttpPut("actualizar-Usuario")]
        public async Task<ActionResult<RespuestaGeneral>> ActualizarUsuario([FromBody] ActualizarUsuarioDTO dto)
        {
            var respuesta = await usuarioService.ActualizarUsuarioAsync(dto);
            if (respuesta.Status == "NotFound")
            {
                return NotFound(respuesta.Answer);
            }
            return Ok(respuesta);
        }
    }
}

