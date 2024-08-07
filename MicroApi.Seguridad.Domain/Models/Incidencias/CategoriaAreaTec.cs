using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class CategoriaAreaTec
    {
        [Key]
        public int Id_CatAre { get; set; }
        public string Nom_CatAre { get; set; }
        public int Val_CatAre { get; set; }

    }
}
