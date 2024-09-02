using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaPrioridad")]
    public class IncidenciaPrioridad
    {
        [Key]
        [Column("InPr_Id")]
        public int InPr_Id { get; set; }

        [Required]
        [Column("InPr_Tipo")]
        public string InPr_Tipo { get; set; }
    }
}
