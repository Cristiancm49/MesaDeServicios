using System;
namespace MicroApi.Seguridad.Domain.DTOs.Usuario
{
	public class ActualizarUsuarioDTO
	{
        public int Usua_Id { get; set; }
        public int? Cont_Id { get; set; }
        public int? UsRo_Id { get; set; }
        public bool? Usua_Estado { get; set; }
    }
}