using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("BloqueEdificio")]
    public class BloqueEdificio
    {
        [Key]
        public int Id_BloqEdi { get; set; }
        public string Nom_BloqEdi { get; set; }
    }
}
