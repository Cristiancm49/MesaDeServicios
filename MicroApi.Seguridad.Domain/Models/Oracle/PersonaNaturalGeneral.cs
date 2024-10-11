using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    [Table("PERSONANATURALGENERAL", Schema = "GENERAL")]
    public class PersonaNaturalGeneral
    {
        [Key]
        [Column("PENG_PK")]
        public int Peng_Pk { get; set; }

        [ForeignKey("PersonaGeneral")]
        [Column("PEGE_ID")]
        public int PeGe_Id { get; set; }

        [Column("PENG_PRIMERNOMBRE")]
        public string PeNG_PrimerNombre { get; set; }

        [Column("PENG_SEGUNDONOMBRE")]
        public string PeNG_SegundoNombre { get; set; }

        [Column("PENG_PRIMERAPELLIDO")]
        public string PeNG_PrimerApellido { get; set; }

        [Column("PENG_SEGUNDOAPELLIDO")]
        public string PeNG_SegundoApellido { get; set; }

        public virtual PersonaGeneral PersonaGeneral { get; set; }
    }
}
