using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("TipoDispositivo")]
    public class TipoDispositivo
    {
        [Key]
        public int Id_TipDispo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom_TipDispo { get; set; }
    }
}