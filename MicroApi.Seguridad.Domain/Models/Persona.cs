using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models
{
    public class Personal
    {
        public int Id_Perso { get; set; }
        public int Id_ChaLog { get; set; }
        public int Id_RolModulo { get; set; }
        public float? PromEval_Perso { get; set; }

        public virtual ChairaLogin ChairaLogin { get; set; }
        public virtual RolModulo RolModulo { get; set; }
    }

    public class ChairaLogin
    {
        public int Id_ChaLog { get; set; }
        public string Nom_ChaLog { get; set; }
        public string Ape_ChaLog { get; set; }
        public int Doc_ChaLog { get; set; }  // Asegúrate de que sea int
        public string Cargo_ChaLog { get; set; }
        public int Rol_ChaLog { get; set; }
        public int Id_DepenLog { get; set; }

        public virtual DependenciaLogin DependenciaLogin { get; set; }
    }

    public class RolModulo
    {
        public int Id_rolModulo { get; set; }
        public string Nom_rolModulo { get; set; }
    }

    public class DependenciaLogin
    {
        public int Id_DepenLog { get; set; }
        public string Nom_DepenLog { get; set; }
        public string Tel_DepenLog { get; set; }
        public string IndiTel_DepenLog { get; set; }
        public int Val_DepenLog { get; set; }
    }
}
