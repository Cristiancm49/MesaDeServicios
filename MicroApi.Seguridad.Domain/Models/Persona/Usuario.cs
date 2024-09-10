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

        public double? Usua_PromedioEvaluacion { get; set; }

        [Required]
        public int UsRo_Id { get; set; }

        [Required]
        public bool Usua_Estado { get; set; }

        [Required]
        public int Cont_Id { get; set; }

        [Required]
        public DateTime Usua_FechaRegistro { get; set; }

        // Relaciones
        [ForeignKey("Cont_Id")]
        public virtual Contrato Contrato { get; set; }

        [ForeignKey("UsRo_Id")]
        public virtual UsuarioRol UsuarioRol { get; set; }

        public virtual ICollection<Incidencia.Incidencia> Incidencia { get; set; }
        public virtual ICollection<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
        public virtual ICollection<HistorialCambios> HistorialesCambios { get; set; }
        public virtual ICollection<InventarioGeneral> InventariosGenerales { get; set; }
    }
}