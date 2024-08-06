using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class Incidencia
    {
        [Key]
        public int Id_Incidencias { get; set; }
        public int IdSolicitante_Incidencias { get; set; }
        public bool EsExc_Incidencias { get; set; }
        public int IdAdmin_IncidenciasExc { get; set; }
        public DateTime FechaHora_Incidencias { get; set; }
        public int Id_AreaTec { get; set; }
        public string Descrip_Incidencias { get; set; }
        public string? Eviden_Incidencias { get; set; }
        public double ValTotal_Incidencias { get; set; }
        public int Id_Estado { get; set; }
        public int Id_Priori { get; set; }
        public int? Id_Perso { get; set; }
        public string? EscaladoA_Incidencias { get; set; }
        public string? MotivRechazo_Incidencias { get; set; }
        public DateTime? FechaCierre_Incidencias { get; set; }
        public string? Resolu_Incidencias { get; set; }
        public double? PromEval_Incidencias { get; set; }
    }
}

