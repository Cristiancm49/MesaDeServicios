using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class RechazarIncidenciaDto
    {
        public int Inci_Id { get; set; }
        public DateTime InTr_FechaActualizacion { get; set; }
        public string InTr_MotivoRechazo { get; set; }
    }
}
