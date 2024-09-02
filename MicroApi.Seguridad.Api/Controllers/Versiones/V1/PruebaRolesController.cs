using MicroApi.Seguridad.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class PruebaRolesController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public PruebaRolesController(ModelContextSQL context)
        {
            _context = context;
        }
        /*
        [HttpGet("ConsultarInfoChaira")]
        public async Task<IActionResult> GetChairaLogins([FromQuery] int docChaLog)
        {
            var chairaLogins = await _context.ChairaLogins
                .Include(c => c.DependenciaLogin)
                .Where(c => c.Doc_ChaLog == docChaLog)
                .Select(c => new
                {
                    c.Id_ChaLog,
                    c.Nom_ChaLog,
                    c.Ape_ChaLog,
                    c.Doc_ChaLog,
                    c.Cargo_ChaLog,
                    c.DependenciaLogin.Nom_DepenLog,
                    c.DependenciaLogin.Tel_DepenLog,
                    c.DependenciaLogin.IndiTel_DepenLog,
                    c.DependenciaLogin.Val_DepenLog,
                })
                .ToListAsync();

            if (chairaLogins == null || !chairaLogins.Any())
            {
                return NotFound();
            }

            return Ok(chairaLogins);
        }

        [HttpPost("AgregarAlModulo")]
        public async Task<IActionResult> InsertarPersonal([FromBody] CrearPersonal modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Encuentra el ChairaLogin con el número de documento
            var chairaLogin = await _context.ChairaLogins
                .FirstOrDefaultAsync(c => c.Doc_ChaLog == modelo.Doc_ChaLog);

            if (chairaLogin == null)
            {
                return NotFound("El ChairaLogin con el número de documento especificado no existe.");
            }

            // Verifica si el RolModulo existe
            var rolModulo = await _context.RolModulos.FindAsync(modelo.Id_RolModulo);
            if (rolModulo == null)
            {
                return BadRequest("El RolModulo especificado no existe.");
            }

            // Crea una nueva entrada en la tabla Personal
            var nuevaPersona = new Personal
            {
                Id_ChaLog = chairaLogin.Id_ChaLog,
                Id_RolModulo = modelo.Id_RolModulo
            };

            _context.Personals.Add(nuevaPersona);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Captura el error y muestra más detalles
                var errorMessage = $"Error al crear la nueva entrada: {ex.InnerException?.Message ?? ex.Message}";
                return StatusCode(500, errorMessage);
            }
            catch (Exception ex)
            {
                // Maneja cualquier otra excepción
                return StatusCode(500, $"Error inesperado: {ex.Message}");
            }

            return CreatedAtAction(nameof(GetPersonas), new { id = nuevaPersona.Id_Perso }, nuevaPersona);
        }

        [HttpGet("ConsultarPersonalModulo")]
        public async Task<IActionResult> GetPersonas([FromQuery] int docChaLog)
        {
            var personas = await _context.Personals
                .Include(p => p.ChairaLogin)
                .ThenInclude(c => c.DependenciaLogin)
                .Include(p => p.RolModulo)
                .Where(p => p.ChairaLogin.Doc_ChaLog == docChaLog)
                .Select(p => new
                {
                    p.Id_Perso,
                    p.ChairaLogin.Nom_ChaLog,
                    p.ChairaLogin.Ape_ChaLog,
                    p.ChairaLogin.Doc_ChaLog,
                    p.ChairaLogin.Cargo_ChaLog,
                    p.RolModulo.Nom_rolModulo,
                    p.ChairaLogin.DependenciaLogin.Nom_DepenLog,
                    p.ChairaLogin.DependenciaLogin.Tel_DepenLog,
                    p.ChairaLogin.DependenciaLogin.IndiTel_DepenLog,
                    p.ChairaLogin.DependenciaLogin.Val_DepenLog,
                    p.PromEval_Perso // Asegúrate de que esta propiedad existe en ChairaLogin
                })
                .ToListAsync();

            if (personas == null || !personas.Any())
            {
                return NotFound();
            }

            return Ok(personas);
        }

        [HttpPut("CambiarRolPersonalModulo")]
        public async Task<IActionResult> UpdatePersonaRolModulo([FromQuery] int docChaLog, [FromBody] int rolModuloId)
        {
            // Encuentra el ChairaLogin con el número de documento
            var chairaLogin = await _context.ChairaLogins
                .FirstOrDefaultAsync(c => c.Doc_ChaLog == docChaLog);

            if (chairaLogin == null)
            {
                return NotFound("El ChairaLogin con el número de documento especificado no existe.");
            }

            // Encuentra la persona asociada al ChairaLogin
            var persona = await _context.Personals
                .FirstOrDefaultAsync(p => p.Id_ChaLog == chairaLogin.Id_ChaLog);

            if (persona == null)
            {
                return NotFound("La persona asociada a este número de documento no existe.");
            }

            // Verifica si el RolModulo existe
            var rolModulo = await _context.RolModulos.FindAsync(rolModuloId);
            if (rolModulo == null)
            {
                return BadRequest("El RolModulo especificado no existe.");
            }

            // Actualiza el campo Id_RolModulo
            persona.Id_RolModulo = rolModuloId;

            // Guarda los cambios en la base de datos
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error al actualizar la información");
            }

            return NoContent(); // Retorna 204 No Content en caso de éxito
        }*/
    }
}
