using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{

    [Table("CategoriaAreaTec")]
    public class CategoriaAreaTec
    {
        [Key]
        public int Id_CatAre { get; set; }
        public string Nom_CatAre { get; set; }
        public ICollection<AreaTecnica> AreaTecnicas { get; set; }
    }

}
