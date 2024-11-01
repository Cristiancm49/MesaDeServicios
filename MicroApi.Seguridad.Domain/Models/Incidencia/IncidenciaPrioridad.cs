using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaPrioridad")]
    public class IncidenciaPrioridad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InPr_Id { get; set; }

        [Required] // Campo obligatorio
        public int InPr_RangoMin { get; set; }

        [Required] // Campo obligatorio
        public int InPr_RangoMax { get; set; }

        [Required]
        [MaxLength(50)] // VARCHAR(50) en SQL Server
        public string InPr_Nombre { get; set; }

        // Relación con Incidencia
        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}