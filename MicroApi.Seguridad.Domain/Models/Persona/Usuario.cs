using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;
using MicroApi.Seguridad.Domain.Models.Inventario;

namespace MicroApi.Seguridad.Domain.Models.Persona
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usua_Id { get; set; }

        [Required] // Clave foránea de Contrato
        public int Cont_Id { get; set; }

        [Required] // Parte de la clave compuesta en Contrato
        public int Unid_Id { get; set; }

        [Required] // Parte de la clave compuesta en Contrato
        public int PeGe_Id { get; set; }

        [Required]
        public int UsRo_Id { get; set; }

        [Required]
        public DateTime Usua_FechaRegistro { get; set; }

        public double? Usua_PromedioEvaluacion { get; set; }

        [Required]
        public bool Usua_Estado { get; set; }


        // Relación con Contrato (clave foránea compuesta)
        [ForeignKey("Cont_Id, PeGe_Id, Unid_Id")]
        public virtual Contrato Contrato { get; set; }

        // Relación con UsuarioRol
        [ForeignKey("UsRo_Id")]
        public virtual UsuarioRol UsuarioRol { get; set; }

        public virtual ICollection<Incidencia.Incidencia> Incidencia { get; set; }
        public virtual ICollection<IncidenciaDiagnostico> IncidenciasDiagnostico { get; set; }
    }
}