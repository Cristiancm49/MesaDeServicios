using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaEstado")]
    public class IncidenciaEstado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InEs_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InEs_Estado { get; set; }

        // Relación con Incidencia
        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}