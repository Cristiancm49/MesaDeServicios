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
        [Column("PeGe_Id")]
        public int PeGe_Id { get; set; }

        [Required]
        [Column("PeGe_DocumentoIdentidad")]
        public int PeGe_DocumentoIdentidad { get; set; }

        [Required]
        [Column("PeGe_PrimerNombre")]
        public string PeGe_PrimerNombre { get; set; }

        [Column("PeGe_SegundoNombre")]
        public string? PeGe_SegundoNombre { get; set; }

        [Required]
        [Column("PeGe_PrimerApellido")]
        public string PeGe_PrimerApellido { get; set; }

        [Column("PeGe_SegundoApellido")]
        public string? PeGe_SegundoApellido { get; set; }
    }
}
