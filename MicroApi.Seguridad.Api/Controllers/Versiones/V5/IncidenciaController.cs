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
                .Where(c => c.ArTe_Id == Id_CatAre)
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
                Inci_EsExc = incidenciaDto.Inci_EsExc,
                Usua_IdAdminExc = incidenciaDto.Usua_IdAdminExc,
                Inci_FechaRegistro = incidenciaDto.Inci_FechaRegistro,
                ArTe_Id = incidenciaDto.ArTe_Id,
                Inci_Descripcion = incidenciaDto.Inci_Descripcion,
                Inci_ValorTotal = incidenciaDto.Inci_ValorTotal,
                InPr_Id = 4
            };

            _context.Incidencias.Add(incidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateIncidencia), new { id = incidencia.Inci_Id }, incidencia);
        }

        // GET api/incidencia
        [HttpGet("InsertIncidencias(Normal-Excepcional")]
        public async Task<IActionResult> GetIncidencias()
        {
            var incidencias = await _context.IncidenciasTrazabilidad
                .Include(it => it.Incidencia)
                    .ThenInclude(i => i.Solicitante)
                        .ThenInclude(c => c.PersonaGeneral)
                .Include(it => it.Incidencia)
                    .ThenInclude(i => i.Solicitante)
                        .ThenInclude(c => c.Unidad)
                .Include(it => it.Incidencia)
                    .ThenInclude(i => i.AdminExc)
                        .ThenInclude(u => u.Contrato)
                            .ThenInclude(c => c.PersonaGeneral)
                .Include(it => it.Incidencia)
                    .ThenInclude(i => i.AreaTecnica)
                        .ThenInclude(at => at.Categoria)
                .Include(it => it.Incidencia)
                    .ThenInclude(i => i.Prioridad)
                .Where(it => it.InTrEs_Id == 1) // Filtra por el estado de la trazabilidad
                .Select(it => new
                {
                    it.Incidencia.Inci_Id,
                    Solicitante_NombreCompleto = $"{it.Incidencia.Solicitante.PersonaGeneral.PeGe_PrimerNombre} {it.Incidencia.Solicitante.PersonaGeneral.PeGe_SegundoNombre ?? ""} {it.Incidencia.Solicitante.PersonaGeneral.PeGe_PrimerApellido} {it.Incidencia.Solicitante.PersonaGeneral.PeGe_SegundoApellido ?? ""}",
                    Solicitante_Cargo = it.Incidencia.Solicitante.Cont_Cargo,
                    Solicitante_Unid_Nombre = it.Incidencia.Solicitante.Unidad.Unid_Nombre,
                    Solicitante_Telefono = it.Incidencia.Solicitante.Unidad.Unid_Telefono,
                    CategoriaAreaTecnica_Nombre = it.Incidencia.AreaTecnica.Categoria.CaAr_Nombre,
                    AreaTecnica_Nombre = it.Incidencia.AreaTecnica.ArTe_Nombre,
                    it.Incidencia.Inci_Descripcion,
                    it.Incidencia.Inci_FechaRegistro,
                    Admin_NombreCompleto = it.Incidencia.AdminExc == null ? "No es excepcional": 
                    $"{it.Incidencia.AdminExc.Contrato.PersonaGeneral.PeGe_PrimerNombre} {it.Incidencia.AdminExc.Contrato.PersonaGeneral.PeGe_SegundoNombre ?? ""} {it.Incidencia.AdminExc.Contrato.PersonaGeneral.PeGe_PrimerApellido} {it.Incidencia.AdminExc.Contrato.PersonaGeneral.PeGe_SegundoApellido ?? ""}",
                    Prioridad_Tipo = it.Incidencia.Prioridad.InPr_Tipo
                })
                .ToListAsync();

            return Ok(incidencias);
        }
        /*
        [HttpGet("SelectSolicitante")]
        public async Task<IActionResult> GetPersonas([FromQuery] int docChaLog)
        {
            var result = await (from c in _context.ChairaLogins
                                join p in _context.Personals on c.Id_ChaLog equals p.Id_ChaLog into cp
                                from p in cp.DefaultIfEmpty()
                                join d in _context.DependenciaLogins on c.Id_DepenLog equals d.Id_DepenLog into cd
                                from d in cd.DefaultIfEmpty()
                                join r in _context.RolModulos on p.Id_RolModulo equals r.Id_rolModulo into pr
                                from r in pr.DefaultIfEmpty()
                                where c.Doc_ChaLog == docChaLog
                                select new
                                {
                                    c.Doc_ChaLog,
                                    c.Nom_ChaLog,
                                    c.Ape_ChaLog,
                                    c.Cargo_ChaLog,
                                    d.IndiTel_DepenLog,
                                    d.Nom_DepenLog,
                                    d.Tel_DepenLog,
                                    d.Val_DepenLog,
                                    Nom_RolModulo = r != null ? r.Nom_rolModulo : null,

                                    //Id para enviar al insert de incidencias
                                    c.Id_ChaLog,

                                }).ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);

        }


        [HttpGet("SelectIncidenciasXEstado")]
        public async Task<IActionResult> GetIncidenciasPorEstado([FromQuery] string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                return BadRequest("El parámetro de estado no puede estar vacío.");
            }

            var result = await _context.Incidencias
                .Include(i => i.ChairaLoginSolicitante)
                .Include(i => i.AreaTecnica)
                .ThenInclude(at => at.CategoriaAreaTec)
                .Include(i => i.EstadoIncidencia)
                .Include(i => i.Prioridad)
                .Where(i => i.EstadoIncidencia.Tipo_Estado == estado)
                .Select(i => new
                {
                    i.Id_Incidencias,
                    Nom_Solicitante = i.ChairaLoginSolicitante.Nom_ChaLog,
                    Ape_Solicitante = i.ChairaLoginSolicitante.Ape_ChaLog,
                    Doc_Solicitante = i.ChairaLoginSolicitante.Doc_ChaLog,
                    Cargo_Solicitante = i.ChairaLoginSolicitante.Cargo_ChaLog,
                    i.EsExc_Incidencias,
                    i.FechaHora_Incidencias,
                    Nom_AreaTec = i.AreaTecnica.Nom_AreaTec,
                    Nom_CatAre = i.AreaTecnica.CategoriaAreaTec.Nom_CatAre,
                    i.Descrip_Incidencias,
                    i.ValTotal_Incidencias,
                    Tipo_Estado = i.EstadoIncidencia.Tipo_Estado,
                    Tipo_Priori = i.Prioridad.Tipo_Priori,
                    i.Id_Perso
                })
                .OrderBy(i => i.Id_Incidencias)
                .ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound($"No se encontraron Incidencias con el estado '{estado}'.");
            }

            return Ok(result);
        }*/
    }
}