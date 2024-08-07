using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("Prioridad")]
    public class Prioridad
    {
        [Key]
        public int Id_Priori { get; set; }
        public string Nom_Priori { get; set; }
        public string Tipo_Priori { get; set; }
        public ICollection<Incidencia> Incidencias { get; set; }
    }

}
