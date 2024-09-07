using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Incidencia;

namespace MicroApi.Seguridad.Domain.Models.Encuesta
{
    [Table("EncuestaCalidad")]
    public class EncuestaCalidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnCa_Id { get; set; }

        [Required]
        public int Inci_Id { get; set; }

        [Required]
        public DateTime EnCa_FechaRespuesta { get; set; }

        [Required]
        public int EnCa_Preg1 { get; set; }

        [Required]
        public int EnCa_Preg2 { get; set; }

        [Required]
        public int EnCa_Preg3 { get; set; }

        [Required]
        public int EnCa_Preg4 { get; set; }

        [Required]
        public int EnCa_Preg5 { get; set; }

        [Required]
        public double EnCa_PromedioEvaluacion { get; set; }

        // Relación
        [ForeignKey("Inci_Id")]
        public virtual Incidencia.Incidencia Incidencia { get; set; }
    }
}