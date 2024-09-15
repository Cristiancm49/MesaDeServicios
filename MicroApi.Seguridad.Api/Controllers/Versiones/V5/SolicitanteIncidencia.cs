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

            var ultimoEstado4 = await _context.IncidenciasTrazabilidad
                .Where(it => it.Inci_Id == inci_id && it.InTrEs_Id == 4)
                .OrderByDescending(it => it.InTr_FechaActualizacion)
                .Select(it => new
                {
                    it.InTr_FechaActualizacion,
                    it.InTrEs_Id
                })
                .FirstOrDefaultAsync();

                        var estados = await _context.IncidenciasTrazabilidad
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

            // Verifica si ultimoEstado4 es nulo antes de usarlo
            var resultadoFinal = estados
                .Where(t => t.Estado != "En proceso" || t.InTr_FechaActualizacion == ultimoEstado4?.InTr_FechaActualizacion)
                .ToList();

            // Agrega el último estado "En proceso" si es necesario
            if (ultimoEstado4 != null)
            {
                var ultimoEstado4Detalle = new
                {
                    InTr_FechaActualizacion = ultimoEstado4.InTr_FechaActualizacion,
                    Estado = _context.IncidenciasTrazabilidadEstado
                        .Where(ite => ite.InTrEs_Id == ultimoEstado4.InTrEs_Id)
                        .Select(ite => ite.InTrEs_Nombre)
                        .FirstOrDefault(),
                    Descripcion = _context.IncidenciasTrazabilidadEstado
                        .Where(ite => ite.InTrEs_Id == ultimoEstado4.InTrEs_Id)
                        .Select(ite => ite.InTrEs_Descripcion)
                        .FirstOrDefault(),
                };

                // Solo agrega si el detalle no está ya en los resultados
                if (!resultadoFinal.Any(r => r.InTr_FechaActualizacion == ultimoEstado4Detalle.InTr_FechaActualizacion && r.Estado == "En proceso"))
                {
                    resultadoFinal.Add(ultimoEstado4Detalle);
                }
            }
            return Ok(resultadoFinal);
        }
    }
}