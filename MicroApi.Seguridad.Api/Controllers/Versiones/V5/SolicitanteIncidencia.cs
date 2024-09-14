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
    public class SolicitanteIncidencia : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public SolicitanteIncidencia(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("VistaIncidenciasFuncionario")]
        public async Task<IActionResult> VistaIncidenciasFuncionario([FromQuery] int documento)
        {
            var incidencias = await _context.Incidencias
                .Where(i => i.Solicitante.PersonaGeneral.PeGe_DocumentoIdentidad == documento)
                .Select(i => new
                {
                    i.Inci_Id,
                    i.Inci_FechaRegistro,
                    i.Inci_Descripcion,
                    Inci_Evidencia = i.Inci_Evidencia == null || i.Inci_Evidencia.Length == 0 ? "Sin evidencia" : "Con evidencia",
                    NombreCompleto = i.Solicitante.PersonaGeneral.PeGe_PrimerNombre + " " +
                                     (string.IsNullOrEmpty(i.Solicitante.PersonaGeneral.PeGe_SegundoNombre) ? "" : i.Solicitante.PersonaGeneral.PeGe_SegundoNombre + " ") +
                                     i.Solicitante.PersonaGeneral.PeGe_PrimerApellido + " " +
                                     (string.IsNullOrEmpty(i.Solicitante.PersonaGeneral.PeGe_SegundoApellido) ? "" : i.Solicitante.PersonaGeneral.PeGe_SegundoApellido),
                    NombreCategoriaAreaTecnica = i.AreaTecnica.Categoria.CaAr_Nombre,
                    NombreAreaTecnica = i.AreaTecnica.ArTe_Nombre,
                    FechaCierre = i.Inci_FechaCierre.HasValue ? i.Inci_FechaCierre.Value.ToString("yyyy-MM-dd HH:mm:ss") : "Sin establecer"
                })
                .ToListAsync();

            if (incidencias == null || !incidencias.Any())
            {
                return NotFound($"No se encontraron incidencias para el documento {documento}");
            }

            return Ok(incidencias);
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