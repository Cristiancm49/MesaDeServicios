using MicroApi.Seguridad.Domain.Models.Persona;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("HistorialCambios")]
    public class HistorialCambios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HiCa_Id { get; set; }

        [Required]
        public int InGe_Id { get; set; }

        [Required]
        public DateTime HiCa_FechaCambio { get; set; }

        [Required]
        public int HiTiCa_Id { get; set; }

        public int? Usua_Id { get; set; }

        public string HiCa_Descripcion { get; set; }

        // Relaciones
        [ForeignKey("InGe_Id")]
        public virtual InventarioGeneral InventarioGeneral { get; set; }

        [ForeignKey("HiTiCa_Id")]
        public virtual HistorialTipoCambio TipoCambio { get; set; }

        [ForeignKey("Usua_Id")]
        public virtual Usuario Usuario { get; set; }
    }
}
