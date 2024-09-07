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

        [Required]
        [MaxLength(50)]
        public string InPr_Tipo { get; set; }

        // Relación con Incidencia
        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}