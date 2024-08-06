using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class ConsultaArea : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public ConsultaArea(ModelContextSQL context)
        {
            _context = context;
        }

        // GET api/v3/persona?docChaLog=1004446325
        [HttpGet]
        public async Task<IActionResult> GetAreas([FromQuery] int docChaLog)
        {
            var Areas = await _context.AreasT
                .Select(A => new
                {
                    A.id_AreaTec,
                    A.Nom_AreaTec,
                    A.Val_AreaTec
                })
                .ToListAsync();

            if (Areas == null || !Areas.Any())
            {
                return NotFound();
            }

            return Ok(Areas);
        }

    }
}
