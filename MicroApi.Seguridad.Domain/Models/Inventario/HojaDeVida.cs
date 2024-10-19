using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroApi.Seguridad.Domain.Models.Usuarios;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("HojaDeVida")]
    public class HojaDeVida
	{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HoVi_Id { get; set; }

        [MaxLength]
        public string HoVi_Descripcion { get; set; }

        [Required]
        public int Dispo_Id { get; set; }

        [Required]
        public DateTime HoVi_FechaHora { get; set; }

        [Required]
        public int Usua_Id { get; set; }

        public int? Inci_Id { get; set; }

        [Required]
        public int TiEv_Id { get; set; }

        // Relaciones
        [ForeignKey("Usua_Id")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Inci_Id")]
        public virtual Incidencia.Incidencia Incidencia { get; set; }

        [ForeignKey("TiEv_Id")]
        public virtual HojaDeVidaTipoEvento HojaDeVidaTipoEvento { get; set; }
    }
}