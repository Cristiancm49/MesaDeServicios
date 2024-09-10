using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("Prioridad")]
    public class Prioridad
    {
        [Key]
        public int Id_Priori { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo_Priori { get; set; }

        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}
