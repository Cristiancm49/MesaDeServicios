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
    [Table("Responsable")]
    public class Responsable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Resp_Id { get; set; }

        [Required]
        public int InGe_Id { get; set; }

        [Required]
        public int Cont_Id { get; set; }

        [Required]
        public DateTime Resp_FechaAsignacion { get; set; }

        // Relaciones
        [ForeignKey("InGe_Id")]
        public virtual InventarioGeneral InventarioGeneral { get; set; }

        [ForeignKey("Cont_Id")]
        public virtual Contrato Contrato { get; set; }
    }
}
