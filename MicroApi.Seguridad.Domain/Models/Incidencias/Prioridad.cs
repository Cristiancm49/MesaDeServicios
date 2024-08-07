using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class Prioridad
    {
        [Key]
        public int Id_Priori { get; set; }
        public string Tipo_Priori { get; set; }
    }
}
