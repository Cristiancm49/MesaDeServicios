using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Diagnostico;

namespace MicroApi.Seguridad.Domain.Models.Usuarios
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Usua_Id { get; set; }

        [Required]
        public int Cont_Id { get; set; }

        [Required]
        public int UsRo_Id { get; set; }

        [Required]
        public DateTime Usua_FechaRegistro { get; set; }

        public double? Usua_PromedioEvaluacion { get; set; }

        [Required]
        public bool Usua_Estado { get; set; }

        // Relación con UsuarioRol
        [ForeignKey("UsRo_Id")]
        public virtual UsuarioRol UsuarioRol { get; set; }

        public virtual ICollection<Incidencia.Incidencia> Incidencia { get; set; }
        public virtual ICollection<IncidenciaDiagnostico> IncidenciasDiagnostico { get; set; }
        public virtual ICollection<HojaDeVida> HojaDeVidas { get; set; }
    }
}