using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaAreaTecnica")]
    public class IncidenciaAreaTecnica
    {
        [Key]
        [Column("ArTe_Id")]
        public int ArTe_Id { get; set; }

        [Required]
        [Column("ArTe_Nombre")]
        public string ArTe_Nombre { get; set; }

        [Required]
        [Column("ArTe_Valor")]
        public int ArTe_Valor { get; set; }

        [ForeignKey("IncidenciaAreaTecnicaCategoria")]
        [Column("CaAr_Id")]
        public int CaAr_Id { get; set; }

        // Propiedad de navegación
        public virtual IncidenciaAreaTecnicaCategoria IncidenciaAreaTecnicaCategoria { get; set; }
    }
}