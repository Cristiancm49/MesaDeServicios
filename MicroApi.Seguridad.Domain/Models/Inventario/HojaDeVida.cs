using MicroApi.Seguridad.Domain.Models.PersonalModulo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("HojaDeVida")]
    public class HojaDeVida
    {
        [Key]
        public int Id_HojVida { get; set; }

        [Required]
        public int Id_Dispo { get; set; }

        [Required]
        public DateTime FechaHora_HojVida { get; set; }

        [Required]
        [StringLength(50)]
        public string DescripIncidencia_HojVida { get; set; }

        [Required]
        public int Id_Perso { get; set; }

        [Required]
        public int EntregadoPor_HojVida { get; set; }

        [Required]
        [StringLength(50)]
        public string Recomend_HojVida { get; set; }

        [Required]
        public int RecibidoPor_HojVida { get; set; }

        [Required]
        public int Id_CatProb { get; set; }

        [ForeignKey("Id_Dispo")]
        public virtual Dispositivos Dispositivo { get; set; }

        [ForeignKey("Id_Perso")]
        public virtual Personal Personal { get; set; }

        [ForeignKey("Id_CatProb")]
        public virtual CategoriaProblema CategoriaProblema { get; set; }
    }
}
