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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BlEd_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string BlEd_Nombre { get; set; }

        // Relación con InventarioPisoOficina
        public virtual ICollection<InventarioPisoOficina> PisosOficina { get; set; }
    }
}