using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class IncidenciaDto
    {
        public int Cont_IdSolicitante { get; set; }
        public int? Usua_IdAdminExc { get; set; }
        public DateTime Inci_FechaRegistro { get; set; }
        public int ArTe_Id { get; set; }
        public string Inci_Descripcion { get; set; }
        public int Inci_ValorTotal { get; set; }
    }
}
