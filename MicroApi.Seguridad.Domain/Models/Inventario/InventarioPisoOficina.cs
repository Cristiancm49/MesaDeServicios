using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("InventarioPisoOficina")]
    public class InventarioPisoOficina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PiOf_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PiOf_Nombre { get; set; }

        [Required]
        public int BlEd_Id { get; set; }

        // Relación con InventarioBloqueEdificio
        [ForeignKey("BlEd_Id")]
        public virtual InventarioBloqueEdificio BloqueEdificio { get; set; }

        // Relación con InventarioGeneral
        public virtual ICollection<InventarioGeneral> InventariosGenerales { get; set; }
    }
}
