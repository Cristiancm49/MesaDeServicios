using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models
{
    public class AreaTecnica
    {

            public int id_AreaTec { get; set; }
            public string Nom_AreaTec { get; set; }
            public int Val_AreaTec { get; set; }
            public int id_CatAre { get; set; }

    }
}
