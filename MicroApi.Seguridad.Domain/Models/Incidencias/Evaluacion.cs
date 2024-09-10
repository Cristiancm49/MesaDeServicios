using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("Evaluacion")]
    public class Evaluacion
    {
        [Key]
        public int Id_Eval { get; set; }

        [Required]
        public int Id_Incidencia { get; set; }

        [Required]
        public int Preg1_Eval { get; set; }

        [Required]
        public int Preg2_Eval { get; set; }

        [Required]
        public int Preg3_Eval { get; set; }

        [Required]
        public int Preg4_Eval { get; set; }

        [Required]
        public int Preg5_Eval { get; set; }

        [ForeignKey("Id_Incidencia")]
        public virtual Incidencia Incidencia { get; set; }
    }
}
