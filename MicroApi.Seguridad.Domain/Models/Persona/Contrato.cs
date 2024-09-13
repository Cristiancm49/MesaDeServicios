using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Inventario;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("Contrato")]
    public class Contrato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cont_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Cont_Cargo { get; set; }

        [Required]
        public DateTime Cont_FechaInicio { get; set; }

        [Required]
        public DateTime Cont_FechaFin { get; set; }

        [Required]
        public bool Cont_Estado { get; set; }

        [Required]
        public int Unid_Id { get; set; }

        [Required]
        public int PeGe_Id { get; set; }

        // Relaciones
        [ForeignKey("PeGe_Id")]
        public virtual PersonaGeneral PersonaGeneral { get; set; }

        [ForeignKey("Unid_Id")]
        public virtual Unidad Unidad { get; set; }

        // Relación con Usuario
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Incidencia.Incidencia> Incidencia { get; set; }
        public virtual ICollection<Responsable> Responsables{ get; set; }
    }
}
