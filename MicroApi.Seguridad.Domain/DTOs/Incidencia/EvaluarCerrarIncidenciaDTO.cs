using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Incidencia
{
    public class EvaluarCerrarIncidenciaDTO
    {
        public int Inci_Id { get; set; }
        public int EnCa_Preg1 { get; set; }
        public int EnCa_Preg2 { get; set; }
        public int EnCa_Preg3 { get; set; }
        public int EnCa_Preg4 { get; set; }
        public int EnCa_Preg5 { get; set; }
    }
}