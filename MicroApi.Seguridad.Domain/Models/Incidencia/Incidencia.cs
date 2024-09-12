using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Encuesta;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("Incidencia")]
    public class Incidencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Inci_Id { get; set; }

        [Required]
        public int Cont_IdSolicitante { get; set; }

        [Required]
        public bool Inci_EsExc { get; set; }  // Default value is false (0)

        public int? Usua_IdAdminExc { get; set; }

        [Required]
        public DateTime Inci_FechaRegistro { get; set; }

        [Required]
        public int ArTe_Id { get; set; }

        [Required]
        public string Inci_Descripcion { get; set; }

        public byte[] Inci_Evidencia { get; set; }

        [Required]
        public int Inci_ValorTotal { get; set; }

        [Required]
        public int InPr_Id { get; set; }

        public DateTime? Inci_FechaCierre { get; set; }

        public int? Inci_UltimoEstado { get; set; }

        // Relaciones
        [ForeignKey("Usua_IdAdminExc")]
        public virtual Usuario AdminExc { get; set; }

        [ForeignKey("Cont_IdSolicitante")]
        public virtual Contrato Solicitante { get; set; }

        [ForeignKey("ArTe_Id")]
        public virtual IncidenciaAreaTecnica AreaTecnica { get; set; }

        [ForeignKey("InPr_Id")]
        public virtual IncidenciaPrioridad Prioridad { get; set; }

        public virtual ICollection<EncuestaCalidad> EncuestaCalidad { get; set; }
        public virtual ICollection<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
    }
}