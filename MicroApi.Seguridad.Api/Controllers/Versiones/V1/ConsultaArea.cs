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

        [HttpGet("GetAreas")]
        public async Task<IActionResult> GetAreas([FromQuery] int CaAr_Id)
        {
            var areas = await _context.IncidenciaAreaTecnicas
                .Where(a => a.CaAr_Id == CaAr_Id)
                .Select(a => new
                {
                    a.ArTe_Id,
                    a.ArTe_Nombre,
                    a.ArTe_Valor
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