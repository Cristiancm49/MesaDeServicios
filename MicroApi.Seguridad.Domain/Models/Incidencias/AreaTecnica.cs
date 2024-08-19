using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("AreaTecnica")]
    public class AreaTecnica
    {
        [Key]
        public int Id_AreaTec { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom_AreaTec { get; set; }

        [Required]
        public int Val_AreaTec { get; set; }

        [Required]
        public int Id_CatAre { get; set; }

        [ForeignKey("Id_CatAre")]
        public virtual CategoriaAreaTec CategoriaAreaTec { get; set; }

        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}