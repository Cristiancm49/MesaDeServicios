using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("Incidencia")]
    public class Incidencia
    {
        [Key]
        [Column("Inci_Id")]
        public int Inci_Id { get; set; }

        [Required]
        [ForeignKey("ContratoSolicitante")]
        [Column("Cont_IdSolicitante")]
        public int Cont_IdSolicitante { get; set; }

        [Required]
        [Column("Inci_EsExc")]
        [DefaultValue(false)]
        public bool Inci_EsExc { get; set; }

        [ForeignKey("ContratoAdminExc")]
        [Column("Cont_IdAdminExc")]
        public int? Cont_IdAdminExc { get; set; }

        [Required]
        [Column("Inci_FechaHora")]
        public DateTime Inci_FechaHora { get; set; }

        [Required]
        [ForeignKey("AreaTecnica")]
        [Column("ArTe_Id")]
        public int ArTe_Id { get; set; }

        [Required]
        [Column("Inci_Descripcion")]
        public string Inci_Descripcion { get; set; }

        [Column("Inci_Evidencia")]
        public byte[]? Inci_Evidencia { get; set; }

        [Required]
        [Column("Inci_ValorTotal")]
        public double Inci_ValorTotal { get; set; }

        [Required]
        [ForeignKey("EstadoIncidencia")]
        [Column("InEs_Id")]
        [DefaultValue(1)]
        public int InEs_Id { get; set; }

        [Required]
        [ForeignKey("PrioridadIncidencia")]
        [Column("InPr_Id")]
        public int InPr_Id { get; set; }

        [ForeignKey("Usuario")]
        [Column("Usua_Id")]
        public int? Usua_Id { get; set; }

        [MaxLength(50)]
        [Column("Inci_EscaladoA")]
        public string? Inci_EscaladoA { get; set; }

        [Column("Inci_MotivRechazo")]
        public string? Inci_MotivRechazo { get; set; }

        [Column("Inci_FechaCierre")]
        public DateTime? Inci_FechaCierre { get; set; }

        [Column("Inci_Resolucion")]
        public string? Inci_Resolucion { get; set; }

        // Propiedades de navegación
        public virtual Contrato ContratoSolicitante { get; set; }
        public virtual Contrato ContratoAdminExc { get; set; }
        public virtual IncidenciaAreaTecnica IncidenciaAreaTecnica { get; set; }
        public virtual IncidenciaEstado IncidenciaEstado { get; set; }
        public virtual IncidenciaPrioridad IncidenciaPrioridad { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
