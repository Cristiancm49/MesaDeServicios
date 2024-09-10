using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Trazabilidad
{
    [Table("IncidenciaTrazabilidadTipoSolucion")]
    public class IncidenciaTrazabilidadTipoSolucion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InTrTiSo_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InTrTiSo_Nombre { get; set; }

        // Relación con IncidenciaTrazabilidad
        public virtual ICollection<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
    }
}
