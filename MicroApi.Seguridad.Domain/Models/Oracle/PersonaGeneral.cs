using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    [Table("PERSONAGENERAL", Schema = "GENERAL")]
    public class PersonaGeneral
    {
        [Key]
        [Column("PEGE_ID")] // Asignar nombre a la clave primaria
        public int PeGe_Id { get; set; }

        [Column("PEGE_DOCUMENTOIDENTIDAD")]
        public string PeGe_DocumentoIdentidad { get; set; }

        public virtual PersonaNaturalGeneral PersonaNaturalGeneral { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}