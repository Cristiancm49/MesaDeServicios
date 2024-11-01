using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    [Table("TIPONOMBRAMIENTO", Schema = "TALENTOV3")]
    public class TipoNombramiento
    {
        [Key]
        [Column("TNOM_ID")]
        public int Tnom_Id { get; set; }

        [Required]
        [Column("TNOM_DESCRIPCION")]
        public string Tnom_Descripcion { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}