using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    public class Personal
    {
        [Key]
        public int Id_Perso { get; set; }
        public int Id_ChaLog { get; set; }
        public int Id_RolModulo { get; set; }

        // Propiedades de navegación
        [ForeignKey("Id_ChaLog")]
        public virtual ChairaLogin ChairaLogin { get; set; }

        [ForeignKey("Id_RolModulo")]
        public virtual RolModulo RolModulo { get; set; }

        // Relación con Incidencia
        public virtual ICollection<Incidencia> Incidencias { get; set; }
    }
}
