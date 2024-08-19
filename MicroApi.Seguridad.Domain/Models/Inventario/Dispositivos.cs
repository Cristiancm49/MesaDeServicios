using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("Dispositivos")]
    public class Dispositivos
    {
        [Key]
        public int Id_Dispo { get; set; }

        [StringLength(50)]
        public string Serial_Dispo { get; set; }

        public int? Placa_Dispo { get; set; }

        public int? TIU_Dispo { get; set; }

        public int? Id_TipoDispo { get; set; }

        public int? PersonCargo_Dispo { get; set; }

        public int? Id_PisOfi { get; set; }

        [StringLength(50)]
        public string Marca_Dispo { get; set; }

        [StringLength(50)]
        public string Modelo_Dispo { get; set; }

        [StringLength(50)]
        public string Procesador_Dispo { get; set; }

        [StringLength(50)]
        public string CapDisc_Dispo { get; set; }

        [StringLength(50)]
        public string TipoDisc_Dispo { get; set; }

        [StringLength(50)]
        public string CapRam_Dispo { get; set; }

        [StringLength(50)]
        public string FechaAdquis_Dispo { get; set; }

        [ForeignKey("Id_PisOfi")]
        public virtual PisoOficina PisoOficina { get; set; }

        [ForeignKey("Id_TipoDispo")]
        public virtual TipoDispositivo TipoDispositivo { get; set; }

        // Navigation property for HojaDeVida
        public virtual ICollection<HojaDeVida> HojasDeVida { get; set; }
    }
}
