using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Inventario
{
    [Table("SalaB7")]
    public class SalaB7
    {
        [Key]
        public int Id_SalaB7 { get; set; }
        public string Nom_Sala { get; set; }
    }

}
