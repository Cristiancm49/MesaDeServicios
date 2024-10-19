using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Data.Repository;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
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

        public async Task<RespuestaGeneral> ConsultarMisIncidenciasActivasAsync(int IdContrato)
        {
            return await trazabilidadRepository.ConsultarMisIncidenciasActivasAsync(IdContrato);
        }

        public async Task<RespuestaGeneral> ConsultarTipoSolucionAsync()
        {
            return await trazabilidadRepository.ConsultarTipoSolucionAsync();
        }

        public async Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto)
        {
            return await trazabilidadRepository.GenerarDiagnosticoAsync(dto);
        }

        public async Task<RespuestaGeneral> ReAsignarIncidenciaAsync(AsignarIncidenciaDTO dto)
        {
            return await trazabilidadRepository.ReAsignarIncidenciaAsync(dto);
        }

        public async Task<RespuestaGeneral> ConsultarEscalarInternoIncidenciaAsync(int IdContrato)
        {
            return await trazabilidadRepository.ConsultarEscalarInternoIncidenciaAsync(IdContrato);
        }

        public async Task<RespuestaGeneral> EscalarInternoIncidenciaAsync(AsignarIncidenciaDTO dto)
        {
            return await trazabilidadRepository.EscalarInternoIncidenciaAsync(dto);
        }
        
        public async Task<RespuestaGeneral> EscalarExternoIncidenciaAsync(EscalarExternoIncidenciaDTO dto)
        {
            return await trazabilidadRepository.EscalarExternoIncidenciaAsync(dto);
        }
    }
}