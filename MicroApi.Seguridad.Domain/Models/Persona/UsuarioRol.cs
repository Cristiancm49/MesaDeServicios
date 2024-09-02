using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("UsuarioRol")]
    public class UsuarioRol
    {
        [Key]
        [Column("UsRo_Id")]
        public int UsRo_Id { get; set; }

        [Required]
        [Column("UsRo_Nombre")]
        public string UsRo_Nombre { get; set; }
    }
}
