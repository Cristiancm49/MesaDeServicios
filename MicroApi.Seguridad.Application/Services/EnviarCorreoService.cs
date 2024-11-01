using System.Threading.Tasks;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs.Correo;
using MicroApi.Seguridad.Domain.Interfaces;

namespace MicroApi.Seguridad.Application.Services
{
    public class EnviarCorreoService : IEnviarCorreoService
    {
        private readonly IEnviarCorreoRepository enviarCorreoRepository;

        public EnviarCorreoService(IEnviarCorreoRepository enviarCorreoRepository)
        {
            this.enviarCorreoRepository = enviarCorreoRepository;
        }

        public async Task NotificarCorreoAsync(NotificarCorreoDTO dto)
        {
            await enviarCorreoRepository.NotificarCorreoAsync(dto);
        }
    }
}