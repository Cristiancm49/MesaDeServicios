using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class TrazabilidadService : ITrazabilidadService
    {
        private readonly ITrazabilidadRepository trazabilidadRepository;

        public TrazabilidadService(ITrazabilidadRepository trazabilidadRepository)
        {
            this.trazabilidadRepository = trazabilidadRepository;
        }

        public async Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto)
        {
            return await trazabilidadRepository.GenerarDiagnosticoAsync(dto);
        }
    }
}