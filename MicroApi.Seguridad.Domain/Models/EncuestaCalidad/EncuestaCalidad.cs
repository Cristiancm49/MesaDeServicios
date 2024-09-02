using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Incidencia;

namespace MicroApi.Seguridad.Domain.Models.EncuestaCalidad
{
        [Table("EncuestaCalidad")]
        public class EncuestaCalidad
        {
            [Key]
            [Column("EnCa_Id")]
            public int EnCa_Id { get; set; }

            [Required]
            [Column("Inci_Id")]
            public int Inci_Id { get; set; }

            [Required]
            [Column("EnCa_Preg1")]
            public int EnCa_Preg1 { get; set; }

            [Required]
            [Column("EnCa_Preg2")]
            public int EnCa_Preg2 { get; set; }

            [Required]
            [Column("EnCa_Preg3")]
            public int EnCa_Preg3 { get; set; }

            [Required]
            [Column("EnCa_Preg4")]
            public int EnCa_Preg4 { get; set; }

            [Required]
            [Column("EnCa_Preg5")]
            public int EnCa_Preg5 { get; set; }

        [Required]
        [Column("EnCa_PromedioEvaluacion")]
        public float EnCa_PromedioEvaluacion { get; set; }

        // Definición de la relación con Incidencia
        [ForeignKey("Inci_Id")]
        public virtual Incidencia.Incidencia Incidencia { get; set; }
    }
}