using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("PisoOficina")]
    public class PisoOficina
    {
        [Key]
        public int Id_PisOfi { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Nom_PisOfi { get; set; }

        [Required]
        public int Id_BloqEdi { get; set; }

        [ForeignKey("Id_BloqEdi")]
        public virtual BloqueEdificio BloqueEdificio { get; set; }
    }
}
