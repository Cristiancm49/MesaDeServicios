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
        public async Task<IActionResult> UsuariosConIncidenciasAsignadas([FromQuery] int? id_Rol = null)
        {
            // Construir la consulta base
            var query = _context.Usuarios
                .Join(_context.Contratos,
                    u => u.Cont_Id,
                    c => c.Cont_Id,
                    (u, c) => new { u, c })
                .Join(_context.PersonasGenerales,
                    uc => uc.c.PeGe_Id,
                    p => p.PeGe_Id,
                    (uc, p) => new { uc.u, uc.c, p })
                .GroupJoin(_context.IncidenciasTrazabilidad,
                    up => up.u.Usua_Id,
                    t => t.Usua_Id,
                    (up, t) => new { up.u, up.c, up.p, Trazabilidad = t })
                .SelectMany(
                    up => up.Trazabilidad.DefaultIfEmpty(),
                    (up, t) => new { up.u, up.p, up.c, Trazabilidad = t })
                .Join(_context.Incidencias,
                    ut => ut.Trazabilidad.Inci_Id,
                    i => i.Inci_Id,
                    (ut, i) => new { ut.u, ut.p, ut.c, i })
                .Join(_context.UsuariosRoles,
                    ui => ui.u.UsRo_Id,
                    r => r.UsRo_Id,
                    (ui, r) => new { ui.u, ui.p, ui.c, ui.i, r })
                .Where(x => x.c.Cont_Estado == true) // Filtrar contratos activos
                .GroupBy(x => new
                {
                    x.u.Usua_Id,
                    x.p.PeGe_PrimerNombre,
                    x.p.PeGe_SegundoNombre,
                    x.p.PeGe_PrimerApellido,
                    x.p.PeGe_SegundoApellido,
                    x.u.Usua_PromedioEvaluacion,
                    x.r.UsRo_Id, // Incluyendo el Id del rol en la agrupación
                    x.r.UsRo_Nombre
                })
                .Select(g => new
                {
                    g.Key.Usua_Id,
                    NombreCompleto = $"{g.Key.PeGe_PrimerNombre} {g.Key.PeGe_SegundoNombre ?? ""} {g.Key.PeGe_PrimerApellido} {g.Key.PeGe_SegundoApellido ?? ""}",
                    RolNombre = g.Key.UsRo_Nombre,
                    IncidenciasActivas = g.Count(x => x.i.Inci_UltimoEstado == 3), // Contar incidencias activas
                    PromedioEvaluacion = g.Key.Usua_PromedioEvaluacion
                });

            // Filtrar por id_Rol si se proporciona
            if (id_Rol.HasValue)
            {
                query = query.Where(x => x.RolNombre == _context.UsuariosRoles
                    .Where(r => r.UsRo_Id == id_Rol.Value)
                    .Select(r => r.UsRo_Nombre)
                    .FirstOrDefault());
            }

            var result = await query.ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
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

        [HttpGet("SelectPrioridad")]
        public async Task<IActionResult> ObtenerPrioridades()
        {
            var prioridades = await _context.IncidenciasPrioridad
                .Select(ip => new
                {
                    ip.InPr_Id,
                    ip.InPr_Tipo
                })
                .ToListAsync();

            return Ok(prioridades);
        }
    }
}