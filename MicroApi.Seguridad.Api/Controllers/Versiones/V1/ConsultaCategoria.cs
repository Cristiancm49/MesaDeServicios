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


        // GET api/v3/categorias
        [HttpGet("GetCategorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.IncidenciasAreaTecnicaCategoria
                .Select(c => new
                {
                    c.CaAr_Id,
                    c.CaAr_Nombre
                })
                .ToListAsync();

            if (categorias == null || !categorias.Any())
            {
                return NotFound();
            }

            return Ok(categorias);
        }
    }
}
