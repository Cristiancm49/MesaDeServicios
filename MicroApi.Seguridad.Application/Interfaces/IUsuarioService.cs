using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Usuario;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IUsuarioService
	{
        Task<RespuestaGeneral> ConsultarUsuarioRolAsync();
        Task<RespuestaGeneral> ConsultarUsuariosAsync(int UsRoId);
        Task<RespuestaGeneral> InsertarUsuarioAsync(InsertarUsuarioDTO dto);
        Task<RespuestaGeneral> ActualizarUsuarioAsync(ActualizarUsuarioDTO dto);
    }
}