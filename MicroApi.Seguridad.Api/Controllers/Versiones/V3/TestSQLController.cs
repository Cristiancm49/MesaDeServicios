using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicroApi.Seguridad.Api.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class TestSQLController : ControllerBase
    {
        private readonly ILogger<TestSQLController> _logger;
        private readonly ModelContextSQL _context;

        public TestSQLController(ILogger<TestSQLController> logger, ModelContextSQL context)
        {
            _logger = logger;
            _context = context;
        }
        /*
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }*/
    }
}
