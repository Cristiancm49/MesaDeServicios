using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Chaira
{
    [Table("DependenciaLogin")]
    public class DependenciaLogin
    {
        [Key]
        public int Id_DepenLog { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom_DepenLog { get; set; }

        [Required]
        [StringLength(50)]
        public string Tel_DepenLog { get; set; }

        [Required]
        [StringLength(50)]
        public string IndiTel_DepenLog { get; set; }

        [Required]
        public int Val_DepenLog { get; set; }

        public virtual ICollection<ChairaLogin> ChairaLogins { get; set; }
    }
}
