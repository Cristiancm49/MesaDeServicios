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
    [Table("InventarioGeneral")]
    public class InventarioGeneral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InGe_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InGe_Serial { get; set; }

        [Required]
        public int InGe_Placa { get; set; }

        public int? InGe_TIU { get; set; }

        [Required]
        public int InTi_Id { get; set; }

        [Required]
        public DateTime InGe_FechaRegistro { get; set; }

        [Required]
        public int PiOf_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string InGe_Marca { get; set; }

        [Required]
        [MaxLength(50)]
        public string InGe_Modelo { get; set; }

        public string InGe_Procesador { get; set; }

        public int? InGe_CapacidadDisco { get; set; }

        public bool? InGe_TipoDisco { get; set; }

        public int? InGe_CapacidadRam { get; set; }

        [Required]
        public DateTime InGe_FechaAdquisicion { get; set; }

        [Required]
        public int Usua_Id { get; set; }

        // Relaciones
        [ForeignKey("PiOf_Id")]
        public virtual InventarioPisoOficina PisoOficina { get; set; }

        [ForeignKey("InTi_Id")]
        public virtual InventarioTipo Tipo { get; set; }

        [ForeignKey("Usua_Id")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Responsable> Responsables { get; set; }
        public virtual ICollection<HistorialCambios> HistorialesCambios { get; set; }
    }
}
