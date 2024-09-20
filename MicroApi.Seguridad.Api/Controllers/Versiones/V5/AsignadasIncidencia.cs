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
    public class AsignadasIncidencia : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public AsignadasIncidencia(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("MisIncidenciasAsignadas")]
        public async Task<IActionResult> MisIncidenciasAsignadas([FromQuery] int documentoIdentidad)
        {
            if (documentoIdentidad <= 0)
            {
                return BadRequest("Documento de identidad inválido.");
            }

            var casos = await _context.IncidenciasTrazabilidad
                .Where(it => it.InTrEs_Id == 3) // Estado asignado
                .Join(_context.Incidencias,
                    it => it.Inci_Id,
                    i => i.Inci_Id,
                    (it, i) => new { it, i })
                .Join(_context.Usuarios,
                    join => join.it.Usua_Id,
                    u => u.Usua_Id,
                    (join, u) => new { join.i, u })
                .Join(_context.PersonasGenerales,
                    join => join.u.Cont_Id,
                    pgS => pgS.PeGe_Id,
                    (join, pgS) => new { join.i, join.u, pgS })
                .Join(_context.Contratos,
                    join => join.u.Cont_Id,
                    c => c.Cont_Id,
                    (join, c) => new { join.i, join.u, join.pgS, c })
                .Join(_context.Unidades,
                    join => join.c.Unid_Id,
                    uS => uS.Unid_Id,
                    (join, uS) => new { join.i, join.u, join.pgS, join.c, uS })
                .Join(_context.IncidenciasAreaTecnica,
                    join => join.i.ArTe_Id,
                    at => at.ArTe_Id,
                    (join, at) => new { join.i, join.u, join.pgS, join.c, join.uS, at })
                .Join(_context.IncidenciasAreaTecnicaCategoria,
                    join => join.at.CaAr_Id,
                    cat => cat.CaAr_Id,
                    (join, cat) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, cat })
                .Join(_context.IncidenciasPrioridad,
                    join => join.i.InPr_Id,
                    ip => ip.InPr_Id,
                    (join, ip) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, join.cat, ip })
                .GroupJoin(_context.Usuarios,
                    join => join.i.Usua_IdAdminExc,
                    uA => uA.Usua_Id,
                    (join, uA) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, join.cat, join.ip, uA })
                .SelectMany(
                    j => j.uA.DefaultIfEmpty(),
                    (j, uA) => new
                    {
                        j.i.Inci_Id,
                        Solicitante_NombreCompleto = $"{j.pgS.PeGe_PrimerNombre} {j.pgS.PeGe_SegundoNombre ?? ""} {j.pgS.PeGe_PrimerApellido} {j.pgS.PeGe_SegundoApellido ?? ""}",
                        Solicitante_DocumentoIdentidad = j.pgS.PeGe_DocumentoIdentidad, // Agregado
                        j.c.Cont_Cargo,
                        j.uS.Unid_Nombre,
                        j.uS.Unid_Telefono,
                        CategoriaAreaTecnica_Nombre = j.cat.CaAr_Nombre,
                        AreaTecnica_Nombre = j.at.ArTe_Nombre,
                        j.i.Inci_Descripcion,
                        j.i.Inci_FechaRegistro,
                        Admin_NombreCompleto = uA != null ? $"{uA.Contrato.PersonaGeneral.PeGe_PrimerNombre} {uA.Contrato.PersonaGeneral.PeGe_SegundoNombre ?? ""} {uA.Contrato.PersonaGeneral.PeGe_PrimerApellido} {uA.Contrato.PersonaGeneral.PeGe_SegundoApellido ?? ""}" : null,
                        j.i.Inci_ValorTotal,
                        Prioridad_Tipo = j.ip.InPr_Tipo,
                        j.i.Inci_UltimoEstado
                    })
                .Where(c => c.Inci_UltimoEstado == 3 && c.Solicitante_DocumentoIdentidad == documentoIdentidad)
                .OrderBy(c => c.Inci_FechaRegistro)
                .ToListAsync();

            if (casos == null || !casos.Any())
            {
                return NotFound("No se encontraron incidencias asignadas.");
            }

            return Ok(casos);
        }

        [HttpGet("MiHistoricoIncidencias")]
        public async Task<IActionResult> MiHistoricoIncidencias([FromQuery] int documentoIdentidad)
        {
            if (documentoIdentidad <= 0)
            {
                return BadRequest("Documento de identidad inválido.");
            }

            var estadosPermitidos = new[] { 6, 7 };

            var casos = await _context.IncidenciasTrazabilidad
                .Where(it => estadosPermitidos.Contains(it.Incidencia.Inci_UltimoEstado ?? 0)) // Filtrar por estados 6 o 7
                .Join(_context.Incidencias,
                    it => it.Inci_Id,
                    i => i.Inci_Id,
                    (it, i) => new { it, i })
                .Join(_context.Usuarios,
                    join => join.it.Usua_Id,
                    u => u.Usua_Id,
                    (join, u) => new { join.i, u })
                .Join(_context.PersonasGenerales,
                    join => join.u.Cont_Id,
                    pgS => pgS.PeGe_Id,
                    (join, pgS) => new { join.i, join.u, pgS })
                .Join(_context.Contratos,
                    join => join.u.Cont_Id,
                    c => c.Cont_Id,
                    (join, c) => new { join.i, join.u, join.pgS, c })
                .Join(_context.Unidades,
                    join => join.c.Unid_Id,
                    uS => uS.Unid_Id,
                    (join, uS) => new { join.i, join.u, join.pgS, join.c, uS })
                .Join(_context.IncidenciasAreaTecnica,
                    join => join.i.ArTe_Id,
                    at => at.ArTe_Id,
                    (join, at) => new { join.i, join.u, join.pgS, join.c, join.uS, at })
                .Join(_context.IncidenciasAreaTecnicaCategoria,
                    join => join.at.CaAr_Id,
                    cat => cat.CaAr_Id,
                    (join, cat) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, cat })
                .Join(_context.IncidenciasPrioridad,
                    join => join.i.InPr_Id,
                    ip => ip.InPr_Id,
                    (join, ip) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, join.cat, ip })
                .GroupJoin(_context.Usuarios,
                    join => join.i.Usua_IdAdminExc,
                    uA => uA.Usua_Id,
                    (join, uA) => new { join.i, join.u, join.pgS, join.c, join.uS, join.at, join.cat, join.ip, uA })
                .SelectMany(
                    j => j.uA.DefaultIfEmpty(),
                    (j, uA) => new
                    {
                        j.i.Inci_Id,
                        Solicitante_NombreCompleto = $"{j.pgS.PeGe_PrimerNombre} {j.pgS.PeGe_SegundoNombre ?? ""} {j.pgS.PeGe_PrimerApellido} {j.pgS.PeGe_SegundoApellido ?? ""}",
                        Solicitante_DocumentoIdentidad = j.pgS.PeGe_DocumentoIdentidad,
                        j.c.Cont_Cargo,
                        j.uS.Unid_Nombre,
                        j.uS.Unid_Telefono,
                        CategoriaAreaTecnica_Nombre = j.cat.CaAr_Nombre,
                        AreaTecnica_Nombre = j.at.ArTe_Nombre,
                        j.i.Inci_Descripcion,
                        j.i.Inci_FechaRegistro,
                        Admin_NombreCompleto = uA != null ? $"{uA.Contrato.PersonaGeneral.PeGe_PrimerNombre} {uA.Contrato.PersonaGeneral.PeGe_SegundoNombre ?? ""} {uA.Contrato.PersonaGeneral.PeGe_PrimerApellido} {uA.Contrato.PersonaGeneral.PeGe_SegundoApellido ?? ""}" : null,
                        j.i.Inci_ValorTotal,
                        Prioridad_Tipo = j.ip.InPr_Tipo,
                        j.i.Inci_UltimoEstado
                    })
                .Where(c => c.Solicitante_DocumentoIdentidad == documentoIdentidad)
                .OrderByDescending(c => c.Inci_Id)
                .Distinct()
                .ToListAsync();

            if (casos == null || !casos.Any())
            {
                return NotFound("No se encontraron incidencias Cerradas.");
            }

            return Ok(casos);
        }
    }
}