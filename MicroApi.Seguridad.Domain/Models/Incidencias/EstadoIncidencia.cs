using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class EstadoIncidencia
    {
        [Key]
        public int Id_Estado { get; set; }
        public string Tipo_Estado { get; set; }
    }
}
