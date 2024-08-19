using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("CategoriaProblema")]
    public class CategoriaProblema
    {
        [Key]
        public int Id_CatProb { get; set; }

        [Required]
        [StringLength(50)]
        public string Tip_CatProb { get; set; }

        // Navigation property for HojaDeVida
        public virtual ICollection<HojaDeVida> HojasDeVida { get; set; }
    }
}
