using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class ConsultaCategoria: ControllerBase
    {
        private readonly ModelContextSQL _context;
        public ConsultaCategoria(ModelContextSQL context)
        {
            _context = context;
        }


        // GET api/v3/persona?docChaLog=1004446325
        [HttpGet]
        public async Task<IActionResult> GetCategorias([FromQuery] int docChaLog)
        {
            var areas = await _context.Categorias
                
                .Select(a => new
                {
                    a.Id_CatAre,
                    a.Nom_CatAre
                })
                .ToListAsync();

            if (areas == null || !areas.Any())
            {
                return NotFound();
            }

            return Ok(areas);
        }

    }
}
