using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaAreaTecnicaCategoria")]
    public class IncidenciaAreaTecnicaCategoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaAr_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CaAr_Nombre { get; set; }

        [Required]
        public int CaAr_Valor { get; set; }

        // Relación con IncidenciaAreaTecnica
        public virtual ICollection<IncidenciaAreaTecnica> IncidenciasAreaTecnica { get; set; }
    }
}