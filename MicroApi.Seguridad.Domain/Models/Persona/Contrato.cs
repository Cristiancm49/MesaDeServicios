using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("Contrato")]
    public class Contrato
    {
        [Key]
        [Column("Cont_Id")]
        public int Cont_Id { get; set; }

        [Required]
        [Column("Cont_Cargo")]
        public string Cont_Cargo { get; set; }

        [Required]
        [Column("Cont_FechaInicio")]
        public DateTime Cont_FechaInicio { get; set; }

        [Required]
        [Column("Cont_FechaFin")]
        public DateTime Cont_FechaFin { get; set; }

        [Required]
        [Column("Cont_Estado")]
        public bool Cont_Estado { get; set; }

        [ForeignKey("Unidad")]
        [Column("Unid_Id")]
        public int Unid_Id { get; set; }

        [ForeignKey("PersonaGeneral")]
        [Column("PeGe_Id")]
        public int? PeGe_Id { get; set; }

        // Propiedades de navegación
        public virtual Unidad Unidad { get; set; }
        public virtual PersonaGeneral? PersonaGeneral { get; set; }
    }
}
