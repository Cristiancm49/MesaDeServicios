using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.zNO
{
    public class RevisarDiagnosticoDTO
    {
        public int InTr_Id { get; set; }
        public string? InTr_ObservacionAdmin { get; set; }
        public int DocumentoAdmin { get; set; }
    }
}
