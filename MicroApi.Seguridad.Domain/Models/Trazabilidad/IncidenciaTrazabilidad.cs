using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Diagnostico;

namespace MicroApi.Seguridad.Domain.Models.Trazabilidad
{
    [Table("IncidenciaTrazabilidad")]
    public class IncidenciaTrazabilidad
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // IDENTITY en SQL Server
        public int InTr_Id { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        public int Inci_Id { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int TrEs_Id { get; set; } = 1; // Valor por defecto 1

        public int? Diag_Id { get; set; }

        [MaxLength(50)]
        public string? InTr_ObservacionAdmin { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime InTr_FechaGenerada { get; set; } = DateTime.UtcNow; // Default: Fecha actual en UTC

        [Required]
        [Column(TypeName = "bit")]
        public bool InTr_Revisado { get; set; } = false; // Valor por defecto 0 (false)

        // Relaciones
        [ForeignKey("Inci_Id")]
        public virtual Incidencia.Incidencia Incidencia { get; set; }

        [ForeignKey("Diag_Id")]
        public virtual IncidenciaDiagnostico? Diagnostico { get; set; }

        [ForeignKey("TrEs_Id")]
        public virtual IncidenciaTrazabilidadEstado TrazabilidadEstado { get; set; }
    }
}
