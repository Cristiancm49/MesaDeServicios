using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Data.Context;

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

        // GET api/v3/Areas
        [HttpGet("GetAreas")]
        public async Task<IActionResult> GetCategorias()
        {
            var areastec = await _context.IncidenciasAreaTecnica
                .Select(c => new
                {
                    c.ArTe_Id,
                    c.ArTe_Nombre,
                    c.ArTe_Valor,
                    c.CaAr_Id
                })
                .ToListAsync();

            if (areastec == null || !areastec.Any())
            {
                return NotFound();
            }

            return Ok(areastec);
        }
    }
}