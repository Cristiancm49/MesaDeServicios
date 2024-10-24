using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.DTOs.Correo
{
    public class NotificarCorreoDTO
    {
        public string Correo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
    }
}