using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroApi.Seguridad.Domain.Models.Chaira;
using MicroApi.Seguridad.Domain.Models.Incidencias;

namespace MicroApi.Seguridad.Domain.Models.PersonalModulo
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        public int Id_Perso { get; set; }
        public int Id_ChaLog { get; set; }
        public ChairaLogin ChairaLogin { get; set; }
        public int Id_RolModulo { get; set; }
        public RolModulo RolModulo { get; set; }
        public double? PromEval_Perso { get; set; }
        public ICollection<Incidencia> Incidencias { get; set; }
    }
}
