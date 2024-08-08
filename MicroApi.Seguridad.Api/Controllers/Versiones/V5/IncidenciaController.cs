using MicroApi.Seguridad.Domain.Models.Incidencias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("SelectIncidencias")]
        public async Task<IActionResult> GetIncidencias()
        {
            var result = await _context.Incidencias
                .Include(i => i.ChairaLoginSolicitante)
                .Include(i => i.ChairaLoginAdminExc)
                .Include(i => i.AreaTecnica)
                .ThenInclude(at => at.CategoriaAreaTec)
                .Include(i => i.EstadoIncidencia)
                .Include(i => i.Prioridad)
                .Include(i => i.Personal)
                .ThenInclude(p => p.ChairaLogin)
                .Include(i => i.Personal)
                .ThenInclude(p => p.RolModulo)
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
                    i.Id_Perso,
                    Id_ChaLog_Personal = i.Personal != null && i.Personal.ChairaLogin != null ? i.Personal.ChairaLogin.Id_ChaLog : (int?)null,
                    Nom_Personal = i.Personal != null && i.Personal.ChairaLogin != null ? i.Personal.ChairaLogin.Nom_ChaLog : null,
                    Ape_Personal = i.Personal != null && i.Personal.ChairaLogin != null ? i.Personal.ChairaLogin.Ape_ChaLog : null,
                    Rol_Personal = i.Personal != null && i.Personal.RolModulo != null ? i.Personal.RolModulo.Nom_RolModulo : null,
                    i.EscaladoA_Incidencias,
                    i.MotivRechazo_Incidencias,
                    i.FechaCierre_Incidencias,
                    i.Resolu_Incidencias,
                    i.PromEval_Incidencias
                })
                .OrderBy(i => i.Id_Incidencias)
                .ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound("No se encontraron Incidencias.");
            }

            return Ok(result);
        }

        [HttpGet("SelectSolicitante")]
        public async Task<IActionResult> GetPersonas([FromQuery] int docChaLog)
        {
            var result = await (from c in _context.ChairaLogins
                                join p in _context.Personals on c.Id_ChaLog equals p.Id_ChaLog into cp
                                from p in cp.DefaultIfEmpty()
                                join d in _context.DependenciaLogins on c.Id_DepenLog equals d.Id_DepenLog into cd
                                from d in cd.DefaultIfEmpty()
                                join r in _context.RolModulos on p.Id_RolModulo equals r.Id_RolModulo into pr
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
                                    Nom_RolModulo = r != null ? r.Nom_RolModulo : null
                                }).ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);

        }


        [HttpPost("Insert")]
        public async Task<IActionResult> CreateIncidencia([FromBody] CrearIncidenciaDto crearIncidenciaDto)
        {
            if (crearIncidenciaDto == null)
            {
                return BadRequest("Datos de incidencia no válidos.");
            }

            var nuevaIncidencia = new Incidencia
            {
                IdSolicitante_Incidencias = crearIncidenciaDto.IdSolicitante_Incidencias,
                EsExc_Incidencias = crearIncidenciaDto.EsExc_Incidencias,
                IdAdmin_IncidenciasExc = crearIncidenciaDto.IdAdmin_IncidenciasExc,
                FechaHora_Incidencias = crearIncidenciaDto.FechaHora_Incidencias,
                Id_AreaTec = crearIncidenciaDto.Id_AreaTec,
                Descrip_Incidencias = crearIncidenciaDto.Descrip_Incidencias,
                Eviden_Incidencias = crearIncidenciaDto.Eviden_Incidencias,
                ValTotal_Incidencias = crearIncidenciaDto.ValTotal_Incidencias,
                Id_Estado = crearIncidenciaDto.Id_Estado,
                Id_Priori = crearIncidenciaDto.Id_Priori,
                Id_Perso = crearIncidenciaDto.Id_Perso,
                EscaladoA_Incidencias = crearIncidenciaDto.EscaladoA_Incidencias,
                MotivRechazo_Incidencias = crearIncidenciaDto.MotivRechazo_Incidencias,
                FechaCierre_Incidencias = crearIncidenciaDto.FechaCierre_Incidencias,
                Resolu_Incidencias = crearIncidenciaDto.Resolu_Incidencias,
                PromEval_Incidencias = crearIncidenciaDto.PromEval_Incidencias
            };

            _context.Incidencias.Add(nuevaIncidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIncidencias), new { id = nuevaIncidencia.Id_Incidencias }, nuevaIncidencia);
        }

        /*[HttpGet("GetIdSolicitanteProdecure")]
        public async Task<IActionResult> GetIdSolicitanteProdecure([FromQuery] int numeroDocumento)
        {
            var sql = "EXEC GetIdSolicitanteByNumeroDocumento @NumeroDocumento";
            var param = new SqlParameter("@NumeroDocumento", numeroDocumento);

            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add(param);
                    var result = await command.ExecuteScalarAsync();

                    // Verifica el resultado
                    if (result == null || Convert.ToInt32(result) <= 0)
                    {
                        return NotFound("Solicitante no encontrado.");
                    }

                    return Ok(result);
                }
            }
        }*/
    }
}
