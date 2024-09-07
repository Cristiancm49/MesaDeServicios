using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaAreaTecnica")]
    public class IncidenciaAreaTecnica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArTe_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ArTe_Nombre { get; set; }

        [Required]
        public int ArTe_Valor { get; set; }

        [Required]
        public int CaAr_Id { get; set; }

        [ForeignKey("CaAr_Id")]
        public virtual IncidenciaAreaTecnicaCategoria Categoria { get; set; }

        // Relación con Incidencia
        public virtual ICollection<Incidencia> Incidencia { get; set; }
    }
}