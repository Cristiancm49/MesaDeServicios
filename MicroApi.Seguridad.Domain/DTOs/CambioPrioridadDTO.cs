using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class CambioPrioridadDTO
    {
        public int Inci_Id { get; set; }
        public int New_Prioridad { get; set; }
        public string MotivoCambio { get; set; }
    }
}
