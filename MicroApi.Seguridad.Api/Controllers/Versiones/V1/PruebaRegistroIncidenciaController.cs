using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PruebaRegistroIncidenciaController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public PruebaRegistroIncidenciaController(ModelContextSQL context)
        {
            _context = context;
        }

        // GET api/v3/persona?docChaLog=1004446325
        [HttpGet]
        public async Task<IActionResult> GetPersonas([FromQuery] int docChaLog)
        {
            var personas = await _context.Personals
                .Include(p => p.ChairaLogin)
                .ThenInclude(c => c.DependenciaLogin)
                .Include(p => p.RolModulo)
                .Where(p => p.ChairaLogin.Doc_ChaLog == docChaLog)
                .Select(p => new
                {
                    p.ChairaLogin.Nom_ChaLog,
                    p.ChairaLogin.Ape_ChaLog,
                    p.ChairaLogin.Doc_ChaLog,
                    p.ChairaLogin.Cargo_ChaLog,
                    p.ChairaLogin.DependenciaLogin.Nom_DepenLog,
                    p.ChairaLogin.DependenciaLogin.Tel_DepenLog,
                    p.ChairaLogin.DependenciaLogin.IndiTel_DepenLog,
                    p.ChairaLogin.DependenciaLogin.Val_DepenLog,
                    p.PromEval_Perso
                })
                .ToListAsync();

            if (personas == null || !personas.Any())
            {
                return NotFound();
            }

            return Ok(personas);
        }

    }
}