using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("Diagnosticos")]
    public class Diagnosticos
    {
        [Key]
        public int Id_Diag { get; set; }

        [Required]
        public DateTime FechaHora_Diag { get; set; }

        [Required]
        [StringLength(50)]
        public string Report_Diag { get; set; }

        [Required]
        public string ReportDetall_Diag { get; set; }

        [Required]
        public bool Soluc_Diag { get; set; }

        [Required]
        public bool Escal_Diag { get; set; }

        [Required]
        public int Id_Perso { get; set; }

        [Required]
        public int Id_Incidencia { get; set; }

        [Required]
        public bool Revisado_Diag { get; set; } = false;

        public string MotivDevol_Diag { get; set; }

       // [ForeignKey("Id_Perso")]
        //public virtual Personal Personal { get; set; }

        [ForeignKey("Id_Incidencia")]
        public virtual Incidencia Incidencia { get; set; }
    }
}
