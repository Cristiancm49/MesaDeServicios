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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsRo_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UsRo_Nombre { get; set; }

        [Required]
        public int UsRo_Nivel { get; set; }

        // Relación con Usuario
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}