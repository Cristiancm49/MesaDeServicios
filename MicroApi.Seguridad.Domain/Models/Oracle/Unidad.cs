using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    [Table("UNIDAD", Schema = "ACADEMICO")]
    public class Unidad
    {
        [Key]
        [Column("UNID_ID")]
        public int Unid_Id { get; set; }

        [Required]
        [Column("UNID_NOMBRE")]
        public string Unid_Nombre { get; set; }

        [Column("UNID_TELEFONO")]
        public string Unid_Telefono { get; set; }

        [Column("UNID_EXTTELEFONO")]
        public string Unid_ExtTelefono { get; set; }

        [Column("UNID_NIVEL")]
        public string Unid_Nivel { get; set; }

        // Propiedad de navegación para la relación con Contratos
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}