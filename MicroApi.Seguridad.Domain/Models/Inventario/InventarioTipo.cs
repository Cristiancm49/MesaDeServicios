using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("InventarioTipo")]
    public class InventarioTipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InTi_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InTi_Nombre { get; set; }

        // Relación con InventarioGeneral
        public virtual ICollection<InventarioGeneral> InventariosGenerales { get; set; }
    }
}
