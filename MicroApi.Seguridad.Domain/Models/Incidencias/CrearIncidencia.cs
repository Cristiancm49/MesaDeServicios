using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class CrearIncidenciaDto
    {
        public int Id_Incidencias { get; set; }
        public int IdSolicitante_Incidencias { get; set; }
        public bool EsExc_Incidencias { get; set; }
        public int? IdAdmin_IncidenciasExc { get; set; }
        public DateTime FechaHora_Incidencias { get; set; }
        public int Id_AreaTec { get; set; }
        public string Descrip_Incidencias { get; set; }
        public byte[]? Eviden_Incidencias { get; set; }
        public double ValTotal_Incidencias { get; set; }
    }
}
