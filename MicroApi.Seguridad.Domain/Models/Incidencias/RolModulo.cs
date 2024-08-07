using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class RolModulo
    {
        [Key]
        public int Id_RolModulo { get; set; }
        public string Nom_RolModulo { get; set; }
    }
}
