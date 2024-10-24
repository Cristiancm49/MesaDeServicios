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
        public string Nombres { get; set; } // Nombre del destinatario o del usuario
        public string Correo { get; set; }   // Dirección de correo electrónico del destinatario
        public string Asunto { get; set; }   // Asunto del correo (opcional)
        public string Mensaje { get; set; }   // Mensaje adicional (opcional)
    }
}