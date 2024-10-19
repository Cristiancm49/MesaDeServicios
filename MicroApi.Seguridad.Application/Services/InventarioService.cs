using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class InventarioService : IInventarioService
    {
        private readonly IInventarioRepository inventarioRepository;

        public InventarioService(IInventarioRepository inventarioRepository)
        {
            this.inventarioRepository = inventarioRepository;
        }

        public async Task<RespuestaGeneral> ConsultarTipoEventosAsync()
        {
            return await inventarioRepository.ConsultarTipoEventosAsync();
        }
    }
}