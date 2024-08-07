using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class AreaTecnica
    {
        [Key]
        public int Id_AreaTec { get; set; }
        public string Nom_AreaTec { get; set; }
        public int Val_AreaTec { get; set; }
        public int Id_CatAre { get; set; }

    }
}
