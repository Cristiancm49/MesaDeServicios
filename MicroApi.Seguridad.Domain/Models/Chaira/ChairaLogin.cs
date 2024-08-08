using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroApi.Seguridad.Domain.Models.Incidencias;
using MicroApi.Seguridad.Domain.Models.PersonalModulo;

namespace MicroApi.Seguridad.Domain.Models.Chaira
{
    [Table("ChairaLogin")]
    public class ChairaLogin
    {
        [Key]
        public int Id_ChaLog { get; set; }
        public string Nom_ChaLog { get; set; }
        public string Ape_ChaLog { get; set; }
        public int Doc_ChaLog { get; set; }
        public string Cargo_ChaLog { get; set; }
        public int Id_DepenLog { get; set; }
        public DependenciaLogin DependenciaLogin { get; set; }
        public virtual ICollection<Incidencia> IncidenciasSolicitante { get; set; }
        public virtual ICollection<Incidencia> IncidenciasAdminExc { get; set; }
        public virtual ICollection<Personal> Personals { get; set; }
    }

}
