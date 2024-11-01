using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApi.Seguridad.Domain.Models.Oracle
{
    [Table("CONTRATO", Schema = "CONTRATOS")]
    public class Contrato
    {
        [Key]
        [Column("CONT_ID")]
        public int Cont_Id { get; set; }

        [Column("CONT_NUMERO")]
        public string? Cont_Numero { get; set; }

        [Required]
        [Column("CONT_FECHAINICIO")]
        public DateTime Cont_FechaInicio { get; set; }

        [Column("CONT_FECHAFIN")]
        public DateTime? Cont_FechaFin { get; set; }

        [Column("CONT_ESTADOCONTRATO")]
        public string Cont_EstadoContrato { get; set; }

        // Relaciones
        [ForeignKey("PeGe_IdContratista")]
        [Column("PEGE_IDCONTRATISTA")]
        public int PeGe_IdContratista { get; set; }
        public virtual PersonaGeneral PersonaGeneral { get; set; }

        [ForeignKey("Unid_Id")]
        [Column("UNID_ID")]
        public int Unid_Id { get; set; }
        public virtual Unidad Unidad { get; set; }

        [ForeignKey("Tnom_Id")]
        [Column("TNOM_ID")]
        public int Tnom_Id { get; set; }
        public virtual TipoNombramiento TipoNombramiento { get; set; }
    }
}