using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("HistorialTipoCambio")]
    public class HistorialTipoCambio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HiTiCa_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string HiTiCa_Nombre { get; set; }

        // Relación con HistorialCambios
        public virtual ICollection<HistorialCambios> HistorialesCambios { get; set; }
    }
}
