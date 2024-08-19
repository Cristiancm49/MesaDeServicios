using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("CategoriaAreaTec")]
    public class CategoriaAreaTec
    {
        [Key]
        public int Id_CatAre { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom_CatAre { get; set; }

        public virtual ICollection<AreaTecnica> AreaTecnicas { get; set; }
    }
}