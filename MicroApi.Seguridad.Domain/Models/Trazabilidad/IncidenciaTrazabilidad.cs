using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Inventario;

namespace MicroApi.Seguridad.Domain.Models.Trazabilidad
{
    [Table("IncidenciaTrazabilidad")]
    public class IncidenciaTrazabilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InTr_Id { get; set; }

        [Required]
        public int Inci_Id { get; set; }

        [Required]
        public int InTrEs_Id { get; set; }

        [Required]
        public DateTime InTr_FechaActualizacion { get; set; }

        [Required]
        public int Usua_Id { get; set; }

        [Required]
        public bool InTr_Solucionado { get; set; }

        public int? InTrTiSo_Id { get; set; }

        [Required]
        public bool InTr_Escalable { get; set; }

        public string InTr_MotivoRechazo { get; set; }

        [Required]
        public bool InTr_Revisado { get; set; }

        [Required]
        public string InTr_descripcion { get; set; }

        // Relaciones
        [ForeignKey("Inci_Id")]
        public virtual Incidencia.Incidencia Incidencia { get; set; }

        [ForeignKey("InTrEs_Id")]
        public virtual IncidenciaTrazabilidadEstado TrazabilidadEstado { get; set; }

        [ForeignKey("InTrTiSo_Id")]
        public virtual IncidenciaTrazabilidadTipoSolucion TrazabilidadTipoSolucion { get; set; }

        [ForeignKey("Usua_Id")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<IncidenciaTrazabilidadTipoSolucion> IncidenciaTrazabilidadTipoSolucion { get; set; }
        public virtual ICollection<IncidenciaTrazabilidadEstado> IncidenciaTrazabilidadEstado { get; set; }

    }
}
