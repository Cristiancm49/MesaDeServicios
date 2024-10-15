using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Trazabilidad
{
    public class GenerarDiagnosticoDTO
    {
        public int Inci_Id { get; set; }
        public int IdContratoUsuario { get; set; }
        public string Diag_DescripcionDiagnostico { get; set; }
        public bool Diag_Solucionado { get; set; }
        public int? TiSo_Id { get; set; }
        public bool Diag_Escalable { get; set; }
    }
}