using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Seguridad.Data.Context;

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
        /*
        // GET api/v3/persona/salas
        [HttpGet("Bloque o edificio")]
        public async Task<IActionResult> GetAll()
        {
            var bloques = await _context.InventariosBloquesEdificio
                .Select(b => new
                {
                    b.BlEd_Id,
                    b.BlEd_Nombre
                })
                .ToListAsync();

            if (bloques == null || !bloques.Any())
            {
                return NotFound("No se encontraron bloques de edificio.");
            }

            return Ok(bloques);
        }*/
    }
}
