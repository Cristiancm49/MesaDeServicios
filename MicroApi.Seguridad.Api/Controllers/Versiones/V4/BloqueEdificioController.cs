using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V4
{
    [Route("api/v4/[controller]")]
    [ApiController]
    public class BloqueEdificioController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public BloqueEdificioController(ModelContextSQL context)
        {
            _context = context;
        }

        // GET api/v3/persona/salas
        [HttpGet("Bloque o edificio")]
        public async Task<IActionResult> GetBloqueEdificio()
        {
            var BloqueEdificio = await _context.BloqueEdificios
                .Select(s => new
                {
                    s.Id_BloqEdi,
                    s.Nom_BloqEdi

                })
                .ToListAsync();

            if (BloqueEdificio == null || !BloqueEdificio.Any())
            {
                return NotFound("No se encontraron bloques o edificios.");
            }

            return Ok(BloqueEdificio);
        }
    }
}
