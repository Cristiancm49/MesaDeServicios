using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V4
{
    [Route("api/v4/[controller]")]
    [ApiController]
    public class TipoDispositivoController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public TipoDispositivoController(ModelContextSQL context)
        {
            _context = context;
        }

        [HttpGet("Tipo de dispositivo")]
        public async Task<IActionResult> GetTipoDispositivo()
        {
            var BloqueEdificio = await _context.TipoDispositivos
                .Select(s => new
                {
                    s.Id_TipDispo,
                    s.Nom_TipDispo

                })
                .ToListAsync();

            if (BloqueEdificio == null || !BloqueEdificio.Any())
            {
                return NotFound("No se encontraron Tipos de dispositivos.");
            }

            return Ok(BloqueEdificio);
        }
    }
}
