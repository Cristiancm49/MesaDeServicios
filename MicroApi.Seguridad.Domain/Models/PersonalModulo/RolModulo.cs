using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.PersonalModulo
{
    [Table("RolModulo")]
    public class RolModulo
    {
        [Key]
        public int Id_rolModulo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom_rolModulo { get; set; }
    }
}
