using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("HojaDeVidaTipoEvento")]
    public class HojaDeVidaTipoEvento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TiEv_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TiEv_Nombre { get; set; }

        [Required]
        public string TiEv_Descripcion { get; set; }

        //Relación con HojaDeVida
        public virtual ICollection<HojaDeVida> HojaDeVidas { get; set; }
    }
}