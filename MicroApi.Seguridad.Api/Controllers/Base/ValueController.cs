using MicroApi.Seguridad.Api.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroApi.Seguridad.Api.Controllers.Base
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        private readonly ILogger<ValueController> logger;
        private readonly WriteHttpRequests writeHttpRequests;

        public ValueController(ILogger<ValueController> logger)
        {
            this.logger = logger;
            writeHttpRequests = new WriteHttpRequests();
        }

        // GET: api/<ValueController>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<string> Get()
        {
            if (writeHttpRequests.WriteDocument(new WriteModel(), HttpContext))
            {
                logger.LogInformation("Estamos obteniendo los autores");
                return new string[] { "value1", "value2" };
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        // GET api/<ValueController>/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public string Get(int id)
        {
            if (writeHttpRequests.WriteDocument(new WriteModel(), HttpContext))
            {
                return "value";
            }
            else
            {
                return "Error";
            }
        }
    }
}
