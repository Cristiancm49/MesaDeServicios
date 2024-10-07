using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.Data.SqlClient;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ModelContextORACLE _context;

        public RolesController(ModelContextORACLE context)
        {
            _context = context;
        }

        [HttpGet("Cotratos_Oracle")]
        public async Task<IActionResult> GetAreaTecnicaNombres()
        {
            try
            {
                var result = await _context.contratos
                                           .Select(a => new {a.CONT_ID,a.PEGE_IDCONTRATISTA })
                                           .ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al consultar la base de datos: {ex.Message}");
            }
        }

        /* GET: api/usuarios/contratos-activos
        [HttpGet("PersonasConContratosActivos")]
        public async Task<IActionResult> GetPersonasConContratosActivos()
        {
            var personasConContratosActivos = await _context.Contratos
                .Where(c => c.Cont_Estado == true) // Filtrar contratos activos (Cont_Estado == 1)
                .Select(c => new
                {
                    PrimerNombre = c.PersonaGeneral.PeGe_PrimerNombre,
                    SegundoNombre = c.PersonaGeneral.PeGe_SegundoNombre,
                    PrimerApellido = c.PersonaGeneral.PeGe_PrimerApellido,
                    SegundoApellido = c.PersonaGeneral.PeGe_SegundoApellido,
                    DocumentoIdentidad = c.PersonaGeneral.PeGe_DocumentoIdentidad,
                    Cargo = c.Cont_Cargo,
                    FechaInicio = c.Cont_FechaInicio,
                    FechaFin = c.Cont_FechaFin,
                    NombreUnidad = c.Unidad.Unid_Nombre,
                    ExtTelefonoUnidad = c.Unidad.Unid_ExtTelefono,
                    TelefonoUnidad = c.Unidad.Unid_Telefono,
                    EstadoContrato = c.Cont_Estado
                })
                .ToListAsync();

            if (personasConContratosActivos == null || !personasConContratosActivos.Any())
            {
                return NotFound();
            }

            return Ok(personasConContratosActivos);
        }

        [HttpPost("AgregarUsuario")]
        public async Task<IActionResult> AgregarUsuario([FromBody] InsertarUsuarioDTO dto)
        {
            try
            {
                // Llamar al procedimiento almacenado utilizando ExecuteSqlRawAsync
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.InsertarUsuario @NumeroDocumento, @UsRo_Id",
                    new SqlParameter("@NumeroDocumento", dto.NumeroDocumento),
                    new SqlParameter("@UsRo_Id", dto.UsRo_Id)
                );

                // Retornar mensaje de éxito si no hay excepciones
                return Ok(new { Message = "Usuario insertado exitosamente." });
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error al insertar el usuario.", Error = ex.Message });
            }
        }

        [HttpGet("UsuariosContratosActivos")]
        public async Task<IActionResult> GetUsuariosConContratosActivos()
        {
            var usuariosConContratosActivos = await _context.Contratos
                .Where(c => c.Cont_Estado == true) // Filtrar contratos activos (Cont_Estado == 1)
                .SelectMany(c => _context.Usuarios
                    .Where(u => u.Cont_Id == c.Cont_Id)
                    .Select(u => new
                    {
                        PrimerNombre = c.PersonaGeneral.PeGe_PrimerNombre,
                        SegundoNombre = c.PersonaGeneral.PeGe_SegundoNombre,
                        PrimerApellido = c.PersonaGeneral.PeGe_PrimerApellido,
                        SegundoApellido = c.PersonaGeneral.PeGe_SegundoApellido,
                        DocumentoIdentidad = c.PersonaGeneral.PeGe_DocumentoIdentidad,
                        EstadoContrato = c.Cont_Estado,
                        EstadoUsuario = u.Usua_Estado,
                        RolNombre = u.UsuarioRol.UsRo_Nombre,
                        FechaRegistro = u.Usua_FechaRegistro,
                        PromedioEvaluacion = u.Usua_PromedioEvaluacion
                    })
                )
                .ToListAsync();

            if (usuariosConContratosActivos == null || !usuariosConContratosActivos.Any())
            {
                return NotFound();
            }

            return Ok(usuariosConContratosActivos);
        }

        [HttpPut("ActualizarRolUsuario")]
        public async Task<IActionResult> ActualizarRolUsuario([FromBody] ActualizarRolUsuarioDto dto)
        {
            // Llamar al procedimiento almacenado
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.ActualizarRolUsuario @NumeroDocumento, @NuevoRolId",
                    new SqlParameter("@NumeroDocumento", dto.NumeroDocumento),
                    new SqlParameter("@NuevoRolId", dto.NuevoRolId));

                return Ok("Rol del usuario actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el rol del usuario: {ex.Message}");
            }
        }

        [HttpGet("SelectRolesUsuario")]
        public async Task<IActionResult> GetSelectRolesUsuario()
        {
            var roles = await _context.UsuariosRoles
                .Select(r => new
                {
                    r.UsRo_Id,
                    r.UsRo_Nombre
                })
                .ToListAsync();

            if (roles == null || !roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }*/
    }
}

