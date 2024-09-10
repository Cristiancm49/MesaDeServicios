using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("EstadoIncidencia")]
    public class EstadoIncidencia
    {
        [Key]
        public int Id_Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo_Estado { get; set; }

        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}
