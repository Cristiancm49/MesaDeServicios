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
        [Column("PiOf_Id")]
        public int PiOf_Id { get; set; }

        [Column("PiOf_Nombre")]
        [Required]
        [StringLength(50)]
        public string PiOf_Nombre { get; set; }

        [Column("BlEd_Id")]
        public int BlEd_Id { get; set; }

        // Navigation property
        [ForeignKey("BlEd_Id")]
        public InventarioBloqueEdificio BloqueEdificio { get; set; }
    }

}
