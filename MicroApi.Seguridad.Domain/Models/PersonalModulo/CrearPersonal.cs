using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Models.PersonalModulo
{
    public class CrearPersonal
    {
        [Required]
        public int Doc_ChaLog { get; set; }

        [Required]
        public int Id_RolModulo { get; set; }
    }
}