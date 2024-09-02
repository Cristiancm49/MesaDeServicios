using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencia
{
    [Table("IncidenciaAreaTecnicaCategoria")]
    public class IncidenciaAreaTecnicaCategoria
    {
        [Key]
        [Column("CaAr_Id")]
        public int CaAr_Id { get; set; }

        [Required]
        [Column("CaAr_Nombre")]
        public string CaAr_Nombre { get; set; }

        [Required]
        [Column("CaAr_Valor")]
        public int CaAr_Valor { get; set; }

        // Propiedades de navegación
        public virtual ICollection<IncidenciaAreaTecnica> IncidenciaAreaTecnicas { get; set; }
    }
}
