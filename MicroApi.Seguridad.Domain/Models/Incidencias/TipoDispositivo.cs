using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.Incidencias
{
    [Table("TipoDispositivo")]
    public class TipoDispositivo
    {
        [Key]
        public int Id_TipDispo { get; set; }
        public string Nom_TipDispo { get; set; }
    }
}
