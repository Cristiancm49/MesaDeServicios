using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Encuesta;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;

namespace MicroApi.Seguridad.Domain.Models.Diagnostico
{
    [Table("IncidenciaDiagnostico")]
    public class IncidenciaDiagnostico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Diag_Id { get; set; }

        [Required]
        public int Usua_Id { get; set; }

        [MaxLength]
        public string? Diag_DescripcionDiagnostico { get; set; }

        [Required]
        public bool Diag_Solucionado { get; set; }

        public int? TiSo_Id { get; set; }

        [Required]
        public bool Diag_Escalable { get; set; }

        // Relaciones
        [ForeignKey("Usua_Id")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("TiSo_Id")]
        public virtual IncidenciaDiagnosticoTipoSolucion? TipoSolucion { get; set; }

        public virtual ICollection<IncidenciaTrazabilidad> IncidenciaTrazabilidads { get; set; }
    }
}