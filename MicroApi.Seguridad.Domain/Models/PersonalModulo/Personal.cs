using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroApi.Seguridad.Domain.Models.Chaira;
using MicroApi.Seguridad.Domain.Models.Incidencias;
using MicroApi.Seguridad.Domain.Models.Inventario;

namespace MicroApi.Seguridad.Domain.Models.PersonalModulo
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        public int Id_Perso { get; set; }

        [Required]
        public int Id_ChaLog { get; set; }

        [ForeignKey("Id_ChaLog")]
        public virtual ChairaLogin ChairaLogin { get; set; }

        [Required]
        public int Id_RolModulo { get; set; }

        [ForeignKey("Id_RolModulo")]
        public virtual RolModulo RolModulo { get; set; }

        public double? PromEval_Perso { get; set; }

        // Navigation properties
        public virtual ICollection<Diagnosticos> Diagnosticos { get; set; }
        public virtual ICollection<HojaDeVida> HojasDeVida { get; set; }
    }
}
