using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaEstado")]
    public class IncidenciaEstado
    {
        [Key]
        [Column("InEs_Id")]
        public int InEs_Id { get; set; }

        [Required]
        [Column("InEs_Estado")]
        public string InEs_Estado { get; set; }
    }
}
