using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class GenerarDiagnosticoDTO
    {
        public int Inci_Id { get; set; }
        public long DocumentoUsuario { get; set; }
        public string Diag_DescripcionDiagnostico { get; set; }
        public bool Diag_Solucionado { get; set; }
        public int? TiSo_Id { get; set; }
        public bool Diag_Escalable { get; set; }
    }
}