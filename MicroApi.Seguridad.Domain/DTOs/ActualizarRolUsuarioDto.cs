using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs
{
    public class ActualizarRolUsuarioDto
    {
        public int NumeroDocumento { get; set; }
        public int NuevoRolId { get; set; }
    }
}