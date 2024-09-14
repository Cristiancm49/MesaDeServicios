using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Data.Context;
using Microsoft.SqlServer.Server;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V5
{
    [Route("api/v5/[controller]")]
    [ApiController]
    public class TrazabilidadIncidencia : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public TrazabilidadIncidencia(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("VistaTrazabilidadAdmin")]
        public async Task<IActionResult> GetVistaTrazabilidadAdmin([FromQuery] int inci_id)
        {
            if (inci_id <= 0)
            {
                return BadRequest("ID de incidencia inválido.");
            }

            var trazabilidad = await _context.IncidenciasTrazabilidad
                .Where(it => it.Inci_Id == inci_id)
                .Select(it => new
                {
                    it.InTr_Id,
                    it.Inci_Id,
                    it.InTr_FechaActualizacion,
                    it.InTr_Solucionado,
                    it.InTr_Escalable,
                    it.InTr_MotivoRechazo,
                    it.InTr_descripcion,
                    RolNombre = _context.UsuariosRoles
                        .Where(ur => ur.UsRo_Id == _context.Usuarios
                            .Where(u => u.Usua_Id == it.Usua_Id)
                            .Select(u => u.UsRo_Id)
                            .FirstOrDefault())
                        .Select(ur => ur.UsRo_Nombre)
                        .FirstOrDefault(),
                    Atiende = _context.PersonasGenerales
                        .Where(pg => pg.PeGe_Id == _context.Contratos
                            .Where(c => c.Cont_Id == _context.Usuarios
                                .Where(u => u.Usua_Id == it.Usua_Id)
                                .Select(u => u.Cont_Id)
                                .FirstOrDefault())
                            .Select(c => c.PeGe_Id)
                            .FirstOrDefault())
                        .Select(pg => $"{pg.PeGe_PrimerNombre} {pg.PeGe_SegundoNombre ?? ""} {pg.PeGe_PrimerApellido} {pg.PeGe_SegundoApellido ?? ""}")
                        .FirstOrDefault(),
                    EstadoNombre = _context.IncidenciasTrazabilidadEstado
                        .Where(ite => ite.InTrEs_Id == it.InTrEs_Id)
                        .Select(ite => ite.InTrEs_Nombre)
                        .FirstOrDefault(),
                    it.InTr_Revisado
                })
                .ToListAsync();

            if (trazabilidad == null || !trazabilidad.Any())
            {
                return NotFound();
            }

            return Ok(trazabilidad);
        }

        [HttpGet("VistaTrazabilidadFuncionario")]
        public async Task<IActionResult> VistaTrazabilidadFuncionario([FromQuery] int inci_id)
        {
            if (inci_id <= 0)
            {
                return BadRequest("ID de incidencia inválido.");
            }

            var trazabilidad = await _context.IncidenciasTrazabilidad
                .Where(it => it.Inci_Id == inci_id)
                .Select(it => new
                {
                    it.InTr_FechaActualizacion,
                    Estado = _context.IncidenciasTrazabilidadEstado
                        .Where(ite => ite.InTrEs_Id == it.InTrEs_Id)
                        .Select(ite => ite.InTrEs_Nombre)
                        .FirstOrDefault(),
                    Descripcion = _context.IncidenciasTrazabilidadEstado
                        .Where(ite => ite.InTrEs_Id == it.InTrEs_Id)
                        .Select(ite => ite.InTrEs_Descripcion)
                        .FirstOrDefault(),
                })
                .ToListAsync();

            if (trazabilidad == null || !trazabilidad.Any())
            {
                return NotFound();
            }

            return Ok(trazabilidad);
        }

    }
}
