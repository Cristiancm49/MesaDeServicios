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
    public class IncidenciaController : ControllerBase
    {

        private readonly ModelContextSQL _context;

        public IncidenciaController(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("SolicitarIncidencias/{documentoIdentidad}")]
        public async Task<IActionResult> GetContratosPorDocumento(int documentoIdentidad)
        {
            var contratos = await (from c in _context.Contratos
                                   join u in _context.Unidades on c.Unid_Id equals u.Unid_Id
                                   join p in _context.PersonasGenerales on c.PeGe_Id equals p.PeGe_Id into personaJoin
                                   from p in personaJoin.DefaultIfEmpty() // LEFT JOIN
                                   where c.Cont_Estado == true && p.PeGe_DocumentoIdentidad == documentoIdentidad
                                   select new
                                   {
                                       c.Cont_Id,
                                       p.PeGe_DocumentoIdentidad,
                                       NombreCompleto = p.PeGe_PrimerNombre + " " +
                                                        (p.PeGe_SegundoNombre ?? "") + " " +
                                                        p.PeGe_PrimerApellido + " " +
                                                        (p.PeGe_SegundoApellido ?? ""),
                                       c.Cont_Cargo,
                                       u.Unid_ExtTelefono,
                                       u.Unid_Nombre,
                                       u.Unid_Telefono,
                                       u.Unid_Valor,
                                       c.Cont_Estado
                                   }).ToListAsync();

            if (contratos == null || !contratos.Any())
            {
                return NotFound(new { message = "No se encontraron contratos activos para el documento proporcionado." });
            }

            return Ok(contratos);
        }

        [HttpGet("InscribirAdmin/{documentoIdentidad}")]
        public async Task<IActionResult> GetUsuarioPorDocumento(int documentoIdentidad)
        {
            var usuarios = await (from u in _context.Usuarios
                                  join c in _context.Contratos on u.Cont_Id equals c.Cont_Id
                                  join r in _context.UsuariosRoles on u.UsRo_Id equals r.UsRo_Id
                                  join p in _context.PersonasGenerales on c.PeGe_Id equals p.PeGe_Id
                                  where r.UsRo_Nombre == "Administrador" && p.PeGe_DocumentoIdentidad == documentoIdentidad
                                  select new
                                  {
                                      p.PeGe_DocumentoIdentidad,
                                      NombreCompleto = p.PeGe_PrimerNombre + " " +
                                                       (p.PeGe_SegundoNombre ?? "") + " " +
                                                       p.PeGe_PrimerApellido + " " +
                                                       (p.PeGe_SegundoApellido ?? ""),
                                      r.UsRo_Nombre,
                                      c.Cont_Estado,
                                      u.Usua_Estado,
                                      u.Usua_Id,
                                      c.Cont_Id
                                  }).ToListAsync();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { message = "No se encontraron usuarios con el rol de Administrador para el documento proporcionado." });
            }

            return Ok(usuarios);
        }

        [HttpGet("CatAreasTec")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.IncidenciasAreaTecnicaCategoria
                .Select(c => new
                {
                    c.CaAr_Id,
                    c.CaAr_Nombre,
                    c.CaAr_Valor
                })
                .ToListAsync();

            if (categorias == null || !categorias.Any())
            {
                return NotFound();
            }

            return Ok(categorias);
        }

        [HttpGet("AreasTec")]
        public async Task<IActionResult> GetAreasTec([FromQuery] int Id_CatAre)
        {
            var areastec = await _context.IncidenciasAreaTecnica
                .Where(c => c.CaAr_Id == Id_CatAre)
                .Select(c => new
                {
                    c.ArTe_Id,
                    c.ArTe_Nombre,
                    c.ArTe_Valor,
                    c.CaAr_Id
                })
                .ToListAsync();

            if (areastec == null || !areastec.Any())
            {
                return NotFound();
            }

            return Ok(areastec);
        }

        [HttpPost("InsertIncidencias(Normal&Excepcional)")]
        public async Task<IActionResult> CreateIncidencia([FromBody] IncidenciaDto incidenciaDto)
        {
            if (incidenciaDto == null)
            {
                return BadRequest("Los datos de incidencia no pueden ser nulos.");
            }

            var incidencia = new Incidencia
            {
                Cont_IdSolicitante = incidenciaDto.Cont_IdSolicitante,
                Usua_IdAdminExc = incidenciaDto.Usua_IdAdminExc,
                Inci_FechaRegistro = incidenciaDto.Inci_FechaRegistro,
                ArTe_Id = incidenciaDto.ArTe_Id,
                Inci_Descripcion = incidenciaDto.Inci_Descripcion,
                Inci_ValorTotal = incidenciaDto.Inci_ValorTotal,
                InPr_Id = 4,
                Inci_UltimoEstado = 1
            };

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateIncidencia), new { id = incidencia.Inci_Id }, incidencia);
        }

        [HttpGet("SelectIncidenciasRegistradas")]
        public async Task<IActionResult> GetIncidenciasRegistradas()
        {
            var incidencias = await _context.IncidenciasTrazabilidad
                .Where(it => it.Incidencia.Inci_UltimoEstado == 1)
                .Select(it => new
                {
                    it.Incidencia.Inci_Id,
                    NombreCompleto = it.Incidencia.Solicitante.PersonaGeneral.PeGe_PrimerNombre + " " +
                                     (it.Incidencia.Solicitante.PersonaGeneral.PeGe_SegundoNombre != null ? it.Incidencia.Solicitante.PersonaGeneral.PeGe_SegundoNombre + " " : "") +
                                     it.Incidencia.Solicitante.PersonaGeneral.PeGe_PrimerApellido + " " +
                                     (it.Incidencia.Solicitante.PersonaGeneral.PeGe_SegundoApellido ?? ""),
                    Cargo = it.Incidencia.Solicitante.Cont_Cargo,
                    NombreUnidad = it.Incidencia.Solicitante.Unidad.Unid_Nombre,
                    TelefonoUnidad = it.Incidencia.Solicitante.Unidad.Unid_Telefono,
                    NombreCategoriaAreaTecnica = it.Incidencia.AreaTecnica.Categoria.CaAr_Nombre,
                    NombreAreaTecnica = it.Incidencia.AreaTecnica.ArTe_Nombre,
                    DescripcionIncidencia = it.Incidencia.Inci_Descripcion,
                    FechaSolicitudIncidencia = it.Incidencia.Inci_FechaRegistro,
                    Prioridad = it.Incidencia.Prioridad.InPr_Tipo
                })
                .ToListAsync();

            return Ok(incidencias);
        }
    }
}