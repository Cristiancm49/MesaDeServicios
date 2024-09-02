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
        [Column("Unid_Id")]
        public int Unid_Id { get; set; }

        [Required]
        [Column("Unid_Nombre")]
        public string Unid_Nombre { get; set; }

        [Required]
        [Column("Unid_Telefono")]
        public int Unid_Telefono { get; set; }

        [Required]
        [Column("Unid_ExtTelefono")]
        public int Unid_ExtTelefono { get; set; }

        [Required]
        [Column("Unid_Valor")]
        public double Unid_Valor { get; set; }
    }
}