using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class DiagnosticosDTO
    {
        public int Inci_Id { get; set; }
        public int Usua_Id { get; set; }
        public bool InTr_Solucionado { get; set; }
        public int? InTrTiSo_Id { get; set; }
        public bool InTr_Escalable { get; set; }
        public string InTr_descripcion { get; set; }
    }
}