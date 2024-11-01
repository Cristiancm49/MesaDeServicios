using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Diagnostico
{
    [Table("IncidenciaDiagnosticoTipoSolucion")]
    public class IncidenciaDiagnosticoTipoSolucion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TiSo_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TiSo_Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string TiSo_Descripcion { get; set; }

        // Relación con IncidenciaTrazabilidad
        public virtual ICollection<IncidenciaDiagnostico> IncidenciasDiagnostico { get; set; }
    }
}