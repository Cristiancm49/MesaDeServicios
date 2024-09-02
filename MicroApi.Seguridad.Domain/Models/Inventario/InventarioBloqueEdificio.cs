using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("InventarioBloqueEdificio")]
    public class InventarioBloqueEdificio
    {
        [Key]
        [Column("BlEd_Id")]
        public int BlEd_Id { get; set; }

        [Column("BlEd_Nombre")]
        [Required]
        [StringLength(50)]
        public string BlEd_Nombre { get; set; }

        // Navigation property
        public ICollection<InventarioPisoOficina> PisosOficinas { get; set; }
    }

}
