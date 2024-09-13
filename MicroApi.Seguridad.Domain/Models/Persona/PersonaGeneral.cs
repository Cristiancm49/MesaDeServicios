using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("PersonaGeneral")]
    public class PersonaGeneral
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PeGe_Id { get; set; }

        [Required]
        public int PeGe_DocumentoIdentidad { get; set; }

        [Required]
        [MaxLength(50)]
        public string PeGe_PrimerNombre { get; set; }

        [MaxLength(50)]
        public string? PeGe_SegundoNombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string PeGe_PrimerApellido { get; set; }

        [MaxLength(50)]
        public string? PeGe_SegundoApellido { get; set; }

        // Relación con Contrato
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
