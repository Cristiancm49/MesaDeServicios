using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("Incidencias")]
    public class Incidencia
    {
        [Key]
        public int Id_Incidencias { get; set; }

        [ForeignKey("ChairaLoginSolicitante")]
        public int IdSolicitante_Incidencias { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool EsExc_Incidencias { get; set; }

        [ForeignKey("ChairaLoginAdminExc")]
        public int? IdAdmin_IncidenciasExc { get; set; }

        [Required]
        public DateTime FechaHora_Incidencias { get; set; }

        [ForeignKey("AreaTecnica")]
        public int Id_AreaTec { get; set; }

        [Required]
        public string Descrip_Incidencias { get; set; }

        public byte[]? Eviden_Incidencias { get; set; }

        [Required]
        public double ValTotal_Incidencias { get; set; }

        [ForeignKey("EstadoIncidencia")]
        [DefaultValue(1)]
        public int Id_Estado { get; set; }

        [ForeignKey("Prioridad")]
        public int Id_Priori { get; set; }

        [ForeignKey("ChairaLogin")]
        public int? Id_Perso { get; set; }

        [MaxLength(50)]
        public string? EscaladoA_Incidencias { get; set; }

        public string? MotivRechazo_Incidencias { get; set; }

        public DateTime? FechaCierre_Incidencias { get; set; }

        public string? Resolu_Incidencias { get; set; }

        public double? PromEval_Incidencias { get; set; }

        // Propiedades de navegación
        //public virtual ChairaLogin ChairaLoginSolicitante { get; set; }
       // public virtual ChairaLogin ChairaLoginAdminExc { get; set; }
        public virtual AreaTecnica AreaTecnica { get; set; }
        public virtual EstadoIncidencia EstadoIncidencia { get; set; }
        public virtual Prioridad Prioridad { get; set; }
        public virtual ICollection<Diagnosticos> Diagnosticos { get; set; }
    }
}
