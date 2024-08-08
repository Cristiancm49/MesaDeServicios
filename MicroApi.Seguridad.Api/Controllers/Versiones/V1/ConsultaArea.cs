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

        [HttpGet]
        public async Task<IActionResult> GetAreas([FromQuery] int docChaLog)
        {
            var areas = await _context.AreaTecnicas
                .Select(a => new
                {
                    a.Id_AreaTec,
                    a.Nom_AreaTec,
                    a.Val_AreaTec
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
