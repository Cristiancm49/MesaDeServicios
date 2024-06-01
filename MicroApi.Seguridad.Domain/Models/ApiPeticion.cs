using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models
{
    public class ApiPeticion
    {
        public decimal ApiIdpeticion { get; set; }

        public string ApiIp { get; set; } = null!;

        public DateTime ApiFecha { get; set; }

        public string ApiEndpoint { get; set; } = null!;

        public string? ApiBody { get; set; }

        public string? ApiToken { get; set; }
    }
}
