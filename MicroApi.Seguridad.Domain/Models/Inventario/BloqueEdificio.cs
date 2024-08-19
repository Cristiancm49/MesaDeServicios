using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("BloqueEdificio")]
    public class BloqueEdificio
    {
        [Key]
        public int Id_BloqEdi { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(MAX)")]
        public string Nom_BloqEdi { get; set; }

        public ICollection<PisoOficina> PisosOficina { get; set; }
    }
}
