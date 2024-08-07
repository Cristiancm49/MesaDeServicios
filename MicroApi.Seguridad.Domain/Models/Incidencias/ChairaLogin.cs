using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class ChairaLogin
    {
        [Key]
        public int Id_ChaLog { get; set; }
        public string Nom_ChaLog { get; set; }
        public string Ape_ChaLog { get; set; }
        public int Doc_ChaLog { get; set; }
        public string Cargo_ChaLog { get; set; }
        public int Id_DepenLog { get; set; }

        // Propiedad de navegación
        public virtual DependenciaLogin DependenciaLogin { get; set; }

        // Relaciones con Incidencia
        public virtual ICollection<Incidencia> IncidenciasSolicitante { get; set; }
        public virtual ICollection<Incidencia> IncidenciasAdminExc { get; set; }

        // Relación con Personal
        public virtual ICollection<Personal> Personales { get; set; }
    }
}
