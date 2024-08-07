using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Propiedades de navegación
        [ForeignKey("IdSolicitante_Incidencias")]
        public virtual ChairaLogin ChairaLoginSolicitante { get; set; }

        [ForeignKey("IdAdmin_IncidenciasExc")]
        public virtual ChairaLogin ChairaLoginAdminExc { get; set; }

        [ForeignKey("Id_AreaTec")]
        public virtual AreaTecnica AreaTecnica { get; set; }

        [ForeignKey("Id_Estado")]
        public virtual EstadoIncidencia EstadoIncidencia { get; set; }

        [ForeignKey("Id_Priori")]
        public virtual Prioridad Prioridad { get; set; }

        [ForeignKey("Id_Perso")]
        public virtual Personal Personal { get; set; }
    }
}
