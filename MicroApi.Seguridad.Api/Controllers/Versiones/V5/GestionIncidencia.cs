using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Data.Context;
using Microsoft.SqlServer.Server;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V5
{
    [Route("api/v5/[controller]")]
    [ApiController]
    public class GestionIncidencia : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public GestionIncidencia(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("UsuariosConIncidenciasAsignadas")]
        public async Task<IActionResult> GetUsuariosConIncidenciasAsignadas([FromQuery] int? id_Rol = null)
        {
            var usuariosConIncidenciasQuery = _context.Usuarios
                .Join(
                    _context.Contratos,
                    u => u.Cont_Id,
                    c => c.Cont_Id,
                    (u, c) => new { Usuario = u, Contrato = c }
                )
                .Join(
                    _context.PersonasGenerales,
                    uc => uc.Contrato.PeGe_Id,
                    p => p.PeGe_Id,
                    (uc, p) => new { uc.Usuario, uc.Contrato, Persona = p }
                )
                .GroupJoin(
                    _context.IncidenciasTrazabilidad.Where(t => t.InTrEs_Id == 3),
                    uc => uc.Usuario.Usua_Id,
                    t => t.Usua_Id,
                    (uc, trazabilidades) => new { uc.Usuario, uc.Contrato, uc.Persona, IncidenciasActivas = trazabilidades }
                )
                .Join(
                    _context.UsuariosRoles,
                    u => u.Usuario.UsRo_Id,
                    r => r.UsRo_Id,
                    (u, r) => new
                    {
                        u.Usuario.Usua_Id,
                        NombreCompleto = $"{u.Persona.PeGe_PrimerNombre} {u.Persona.PeGe_SegundoNombre} {u.Persona.PeGe_PrimerApellido} {u.Persona.PeGe_SegundoApellido}",
                        RolNombre = r.UsRo_Nombre,
                        IncidenciasActivas = u.IncidenciasActivas.Count(),
                        u.Usuario.Usua_PromedioEvaluacion,
                        u.Usuario.UsRo_Id
                    }
                );

            // Aplicamos el filtro por id_Rol si se proporciona
            if (id_Rol.HasValue)
            {
                usuariosConIncidenciasQuery = usuariosConIncidenciasQuery.Where(u => u.UsRo_Id == id_Rol.Value);
            }

            var usuariosConIncidencias = await usuariosConIncidenciasQuery.ToListAsync();

            if (usuariosConIncidencias == null || !usuariosConIncidencias.Any())
            {
                return NotFound();
            }

            return Ok(usuariosConIncidencias);
        }

        [HttpPost("AsignarUsuario")]
        public async Task<IActionResult> AsignarUsuario([FromBody] AsignarIncidenciaDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos no válidos.");
            }

            try
            {
                // Llamar al procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.AsignarUsuarioAIncidencia @Inci_Id, @Usua_Id",
                    new SqlParameter("@Inci_Id", dto.Inci_Id),
                    new SqlParameter("@Usua_Id", dto.Usua_Id)
                );

                return Ok("Usuario asignado a la incidencia con éxito.");
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones relacionadas con la base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("RechazarIncidencia")] //Procedimeinto de rechazo de incidencia
        public async Task<IActionResult> RechazarIncidencia([FromBody] RechazarIncidenciaDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos no válidos.");
            }

            try
            {
                // Llamar al procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.RechazarIncidencia @Inci_Id, @InTr_FechaActualizacion, @InTr_MotivoRechazo",
                    new SqlParameter("@Inci_Id", dto.Inci_Id),
                    new SqlParameter("@InTr_FechaActualizacion", dto.InTr_FechaActualizacion),
                    new SqlParameter("@InTr_MotivoRechazo", dto.InTr_MotivoRechazo)
                );

                return Ok("Incidencia rechazada con éxito.");
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones relacionadas con la base de datos
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CambioPrioridad")] //Procedimiento de cambio de prioridad, justificando el motivo
        public async Task<IActionResult> CambioPrioridad([FromBody] CambioPrioridadDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos no válidos.");
            }

            try
            {
                // Llamar al procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateIncidenciaPrioridad @Inci_Id, @New_Prioridad, @MotivoCambio",
                    new SqlParameter("@Inci_Id", dto.Inci_Id),
                    new SqlParameter("@New_Prioridad", dto.New_Prioridad),
                    new SqlParameter("@MotivoCambio", dto.MotivoCambio)
                );

                return Ok("Prioridad de la incidencia actualizada con éxito.");
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones relacionadas con la base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}