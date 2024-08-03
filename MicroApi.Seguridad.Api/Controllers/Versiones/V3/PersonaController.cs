using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V3
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public PersonaController(ModelContextSQL context)
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
                    p.Id_Perso,
                    p.ChairaLogin.Nom_ChaLog,
                    p.ChairaLogin.Ape_ChaLog,
                    p.ChairaLogin.Doc_ChaLog,
                    p.ChairaLogin.Cargo_ChaLog,
                    p.RolModulo.Nom_rolModulo,
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
