using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Data.Repository;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Usuario;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public async Task<RespuestaGeneral> ConsultarUsuarioRolAsync()
        {
            return await usuarioRepository.ConsultarUsuarioRolAsync();
        }

        public async Task<RespuestaGeneral> ConsultarUsuariosAsync(int? UsRoId)
        {
            return await usuarioRepository.ConsultarUsuariosAsync(UsRoId);
        }

        public async Task<RespuestaGeneral> InsertarUsuarioAsync(InsertarUsuarioDTO dto)
        {
            return await usuarioRepository.InsertarUsuarioAsync(dto);
        }

        public async Task<RespuestaGeneral> ActualizarUsuarioAsync(ActualizarUsuarioDTO dto)
        {
            return await usuarioRepository.ActualizarUsuarioAsync(dto);
        }
    }
}

