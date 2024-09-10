using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Data.Context;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public RolesController(ModelContextSQL context)
        {
            _context = context;
        }

        // GET: api/usuarios/contratos-activos
        [HttpGet("contratos-activos")]
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



        [HttpPost("insertar-usuario")]
        public async Task<IActionResult> InsertarUsuario([FromQuery] int numeroDocumento, [FromQuery] int usRoId)
        {
            try
            {
                // Obtener el Cont_Id del contrato asociado al número de documento, solo si el contrato está activo
                var contId = await _context.Contratos
                    .Where(c => c.PersonaGeneral.PeGe_DocumentoIdentidad == numeroDocumento && c.Cont_Estado)
                    .Select(c => c.Cont_Id)
                    .FirstOrDefaultAsync();

                // Verifica si se encontró un Cont_Id válido
                if (contId == 0)
                {
                    // Retorna un mensaje informativo si no se encontró un contrato activo
                    return NotFound(new { Message = "No se encontró un contrato activo para el número de documento proporcionado." });
                }

                // Verifica si el UsRo_Id es válido
                var usuarioRolExistente = await _context.UsuariosRoles.AnyAsync(ur => ur.UsRo_Id == usRoId);
                if (!usuarioRolExistente)
                {
                    // Retorna un mensaje informativo si el rol de usuario no existe
                    return BadRequest(new { Message = "El rol de usuario proporcionado no existe." });
                }

                // Insertar un nuevo registro en la tabla Usuario
                var nuevoUsuario = new Usuario
                {
                    UsRo_Id = usRoId,
                    Cont_Id = contId,
                    Usua_Estado = true,
                    Usua_FechaRegistro = DateTime.Now
                };

                _context.Usuarios.Add(nuevoUsuario);

                // Intentar guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Usuario insertado exitosamente." });
            }
            catch (Exception ex)
            {
                // Manejo de errores y retorno de un mensaje de error
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se produjo un error al insertar el usuario.", Error = ex.Message });
            }
        }

        [HttpGet("personas-contratos-activos")]
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

        [HttpPut("actualizar-rol")]
        public async Task<IActionResult> UpdateUserRole([FromQuery] int numeroDocumento, [FromQuery] int nuevoRolId)
        {
            // Obtén el ID del contrato asociado al número de documento
            var contrato = await _context.Contratos
                .Include(c => c.PersonaGeneral)
                .Where(c => c.PersonaGeneral.PeGe_DocumentoIdentidad == numeroDocumento && c.Cont_Estado == true)
                .Select(c => new { c.Cont_Id })
                .FirstOrDefaultAsync();

            if (contrato == null)
            {
                return NotFound("No se encontró un contrato activo para el número de documento proporcionado.");
            }

            // Actualiza el rol del usuario asociado al Cont_Id
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Cont_Id == contrato.Cont_Id);

            if (usuario == null)
            {
                return NotFound("No se encontró un usuario asociado al contrato.");
            }

            usuario.UsRo_Id = nuevoRolId;
            _context.Usuarios.Update(usuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Error al actualizar el rol del usuario: {ex.Message}");
            }

            return Ok("Rol del usuario actualizado exitosamente.");
        }
    }
}

