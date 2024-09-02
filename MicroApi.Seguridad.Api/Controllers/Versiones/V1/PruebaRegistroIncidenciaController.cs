using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Api.Controllers.Versiones.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PruebaRegistroIncidenciaController : ControllerBase
    {
        private readonly ModelContextSQL _context;

        public PruebaRegistroIncidenciaController(ModelContextSQL context)
        {
            _context = context;
        }

        // GET api/v3/persona?docChaLog=1004446325
        [HttpGet]
        public async Task<IActionResult> GetPersonas([FromQuery] int docChaLog)
        {
            var personas = await _context.Usuarios
                .Include(u => u.Contrato)
                    .ThenInclude(c => c.Unidad)
                .Include(u => u.Contrato)
                    .ThenInclude(c => c.PersonaGeneral)
                .Where(u => u.Contrato.PersonaGeneral.PeGe_DocumentoIdentidad == docChaLog)
                .Select(u => new
                {
                    u.Usua_Id,
                    u.Usua_PromedioEvaluacion,
                    u.Usua_Estado,
                    u.Usua_FechaRegistro,
                    Contrato = new
                    {
                        u.Contrato.Cont_Cargo,
                        u.Contrato.Cont_FechaInicio,
                        u.Contrato.Cont_FechaFin,
                        u.Contrato.Cont_Estado,
                        Unidad = new
                        {
                            u.Contrato.Unidad.Unid_Nombre,
                            u.Contrato.Unidad.Unid_Telefono,
                            u.Contrato.Unidad.Unid_ExtTelefono,
                            u.Contrato.Unidad.Unid_Valor
                        },
                        PersonaGeneral = new
                        {
                            u.Contrato.PersonaGeneral.PeGe_DocumentoIdentidad,
                            u.Contrato.PersonaGeneral.PeGe_PrimerNombre,
                            u.Contrato.PersonaGeneral.PeGe_SegundoNombre,
                            u.Contrato.PersonaGeneral.PeGe_PrimerApellido,
                            u.Contrato.PersonaGeneral.PeGe_SegundoApellido
                        }
                    }
                })
                .ToListAsync();

            if (personas == null || !personas.Any())
            {
                return NotFound();
            }

            return Ok(personas);
        }


    }
}