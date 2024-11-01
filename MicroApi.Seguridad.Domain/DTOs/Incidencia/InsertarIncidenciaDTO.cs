using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Incidencia
{
    public class InsertarIncidenciaDTO
    {
        public int IdContratoSolicitante { get; set; }
        public int ValorUnidadSolicitante { get; set; }
        public int? IdContratoAdmin { get; set; }
        public int AreaTecnica { get; set; }
        public string Descripcion { get; set; }
    }
}