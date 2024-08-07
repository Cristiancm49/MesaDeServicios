using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("RolModulo")]
    public class RolModulo
    {
        [Key]
        public int Id_RolModulo { get; set; }
        public string Nom_RolModulo { get; set; }
        public ICollection<Personal> Personals { get; set; }
    }

}
