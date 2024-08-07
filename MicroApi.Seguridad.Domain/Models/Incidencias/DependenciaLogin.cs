using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("DependenciaLogin")]
    public class DependenciaLogin
    {
        [Key]
        public int Id_DepenLog { get; set; }
        public string Nom_DepenLog { get; set; }
        public string Tel_DepenLog { get; set; }
        public string IndiTel_DepenLog { get; set; }
        public int Val_DepenLog { get; set; }
        public ICollection<ChairaLogin> ChairaLogins { get; set; }
    }
}
