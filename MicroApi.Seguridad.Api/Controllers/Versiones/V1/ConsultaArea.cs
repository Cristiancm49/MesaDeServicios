using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Incidencias;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    public class ConsultaAreaController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public ConsultaAreaController(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAreas([FromQuery] int Id_CatAre)
        {
            var areas = await _context.AreaTecnicas
                .Where(a => a.Id_CatAre == Id_CatAre)
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