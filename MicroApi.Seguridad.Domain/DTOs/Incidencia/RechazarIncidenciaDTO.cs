using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Incidencia
{
    public class RechazarIncidenciaDTO
    {
        public int Inci_Id { get; set; }
        public string InTr_MotivoRechazo { get; set; }
    }
}
