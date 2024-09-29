using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Incidencia
{
    public class InsertarIncidenciaDTO
    {
        public long DocumentoSolicitante { get; set; }
        public long? DocumentoAdmin { get; set; }
        public int AreaTecnica { get; set; }
        public string Descripcion { get; set; }
    }
}