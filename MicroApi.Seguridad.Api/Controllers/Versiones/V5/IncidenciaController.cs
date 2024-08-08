using MicroApi.Seguridad.Domain.Models.Incidencias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("Incidencias")]
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

    }
}
