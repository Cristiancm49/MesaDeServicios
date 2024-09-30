using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Incidencia
{
    public class CambiarPrioridadDTO
    {
        public int Inci_Id { get; set; }
        public int New_Prioridad { get; set; }
        public string MotivoCambio { get; set; }
    }
}