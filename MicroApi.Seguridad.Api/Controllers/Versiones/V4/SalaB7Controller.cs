using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V4
{
    [Route("api/v4/[controller]")]
    [ApiController]
    public class SalaB7Controller : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public SalaB7Controller(ModelContextSQL context)
        {
            _context = context;
        }

        // GET api/v3/persona/salas
        [HttpGet("Salas")]
        public async Task<IActionResult> GetSalas()
        {
            var salas = await _context.SalaB7s
                .Select(s => new
                {
                    s.Id_SalaB7,
                    s.Nom_Sala
                })
                .ToListAsync();

            if (salas == null || !salas.Any())
            {
                return NotFound("No se encontraron salas.");
            }

            return Ok(salas);
        }
    }
}
