using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.zEjemplos
{
    public class VsProgramahistoricoest
    {
        public decimal? Idestp { get; set; }

        public string? Idprog { get; set; }

        public string? Programa { get; set; }

        public string? Semestre { get; set; }

        public decimal? Promedio { get; set; }

        public string Pensumdescripcion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public decimal PegeId { get; set; }
    }
}
