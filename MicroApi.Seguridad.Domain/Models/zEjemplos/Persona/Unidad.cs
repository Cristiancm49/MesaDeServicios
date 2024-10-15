using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("Unidad")]
    public class Unidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Unid_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Unid_Nombre { get; set; }

        [Required]
        public int Unid_Telefono { get; set; }

        [Required]
        public int Unid_ExtTelefono { get; set; }

        [Required]
        public int Unid_Valor { get; set; }

        [Required]
        public int Unid_Nivel { get; set; }

        // Relación con Contrato
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}