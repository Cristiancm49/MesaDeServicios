using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Trazabilidad
{
    [Table("IncidenciaTrazabilidadEstado")]
    public class IncidenciaTrazabilidadEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TrEs_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrEs_Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrEs_Descripcion { get; set; }

        // Relación con IncidenciaTrazabilidad
        public virtual ICollection<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
    }
}
