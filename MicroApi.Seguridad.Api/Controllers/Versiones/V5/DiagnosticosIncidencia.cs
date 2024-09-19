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
    public class DiagnosticosIncidencia : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public DiagnosticosIncidencia(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("TipoSolucionDiagnosticos")]
        public async Task<IActionResult> TipoSolucionDiagnosticos()
        {
            try
            {
                var soluciones = await _context.IncidenciasTrazabilidadTipoSolucion
                    .Select(s => new
                    {
                        s.InTrTiSo_Id,
                        s.InTrTiSo_Nombre,
                        s.InTrTiSo_Descripcion
                    })
                    .ToListAsync();

                return Ok(soluciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("GenerarDiagnosticos")]
        public async Task<IActionResult> GenerarDiagnosticos([FromBody] DiagnosticosDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Datos no válidos.");
            }

            try
            {
                // Llamar al procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync("EXEC dbo.GenerarDiagnosticos @Inci_Id, @Usua_Id, @InTr_Solucionado, @InTrTiSo_Id, @InTr_Escalable, @InTr_descripcion",
                    new SqlParameter("@Inci_Id", dto.Inci_Id),
                    new SqlParameter("@Usua_Id", dto.Usua_Id),
                    new SqlParameter("@InTr_Solucionado", dto.InTr_Solucionado),
                    new SqlParameter("@InTrTiSo_Id", (object)dto.InTrTiSo_Id ?? DBNull.Value),
                    new SqlParameter("@InTr_Escalable", dto.InTr_Escalable),
                    new SqlParameter("@InTr_descripcion", dto.InTr_descripcion)
                );

                return Ok("Diagnóstico generado con éxito.");
            }
            catch (DbUpdateException ex)
            {
                // Manejar excepciones relacionadas con la base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}