using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class IncidenciaService : IIncidenciaService
    {
        private readonly IIncidenciaRepository incidenciaRepository;

        public IncidenciaService(IIncidenciaRepository incidenciaRepository)
        {
            this.incidenciaRepository = incidenciaRepository;
        }
        public async Task<RespuestaGeneral> ConsultarContratoAsync(long documentoPersona)
        {
            return await incidenciaRepository.ConsultarContratoAsync(documentoPersona);
        }
        public async Task<RespuestaGeneral> ConsultarAreaTecnicaYCategoriaAsync()
        {
            return await incidenciaRepository.ConsultarAreaTecnicaYCategoriaAsync();
        }

        public async Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto)
        {
            return await incidenciaRepository.InsertarIncidenciaAsync(dto);
        }

        public async Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto)
        {
            return await incidenciaRepository.RechazarIncidenciaAsync(dto);
        }
        public async Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto)
        {
            return await incidenciaRepository.AsignarIncidenciaAsync(dto);
        }
    }
}