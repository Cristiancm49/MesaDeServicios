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

        // GET api/v5/Incidencia/Select incidencias
        [HttpGet("Incidencias")]
        public async Task<IActionResult> GetIncidencia()
        {
            var BloqueEdificio = await _context.Incidencias
                .Select(s => new
                {
                    s.Id_Incidencias,
                    s.IdSolicitante_Incidencias,
                    s.EsExc_Incidencias,
                    s.IdAdmin_IncidenciasExc,
                    s.FechaHora_Incidencias,
                    s.Id_AreaTec,
                    s.Descrip_Incidencias,
                    s.Eviden_Incidencias,
                    s.ValTotal_Incidencias,
                    s.Id_Estado,
                    s.Id_Priori,
                    s.Id_Perso,
                    s.EscaladoA_Incidencias,
                    s.MotivRechazo_Incidencias,
                    s.FechaCierre_Incidencias,
                    s.Resolu_Incidencias,
                    s.PromEval_Incidencias
                })
                .ToListAsync();

            if (BloqueEdificio == null || !BloqueEdificio.Any())
            {
                return NotFound("No se encontraron Incidencias.");
            }

            return Ok(BloqueEdificio);
        }
    }
}
