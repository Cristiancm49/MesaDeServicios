using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("Usua_Id")]
        public int Usua_Id { get; set; }

        [Column("Usua_PromedioEvaluacion")]
        public double? Usua_PromedioEvaluacion { get; set; }

        [ForeignKey("UsuarioRol")]
        [Column("UsRo_Id")]
        public int UsRo_Id { get; set; }

        [Required]
        [Column("Usua_Estado")]
        public bool Usua_Estado { get; set; }

        [ForeignKey("Contrato")]
        [Column("Cont_Id")]
        public int? Cont_Id { get; set; }

        [Required]
        [Column("Usua_FechaRegistro")]
        public DateTime Usua_FechaRegistro { get; set; }

        // Propiedades de navegación
        public virtual Contrato? Contrato { get; set; }
        public virtual UsuarioRol UsuarioRol { get; set; }
    }
}
