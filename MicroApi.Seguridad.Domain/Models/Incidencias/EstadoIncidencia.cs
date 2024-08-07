using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("EstadoIncidencia")]
    public class EstadoIncidencia
    {
        [Key]
        public int Id_Estado { get; set; }
        public string Nom_Estado { get; set; }
        public string Tipo_Estado { get; set; }
        public ICollection<Incidencia> Incidencias { get; set; }
    }
}
