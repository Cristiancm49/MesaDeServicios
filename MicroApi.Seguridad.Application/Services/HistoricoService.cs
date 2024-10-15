using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Data.Repository;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class HistoricoService : IHistoricoService
    {
        private readonly IHistoricoRepository historicoRepository;

        public HistoricoService(IHistoricoRepository historicoRepository)
        {
            this.historicoRepository = historicoRepository;
        }

        public async Task<RespuestaGeneral> ConsultarHistoricoIncidenciasAsync()
        {
            return await historicoRepository.ConsultarHistoricoIncidenciasAsync();
        }

        public async Task<RespuestaGeneral> ConsultarMisIncidenciaCerradasAsync(int IdContrato)
        {
            return await historicoRepository.ConsultarMisIncidenciaCerradasAsync(IdContrato);
        }

        public async Task<RespuestaGeneral> ConsultarMisSolicitudesAsync(int IdContrato, bool estado)
        {
            return await historicoRepository.ConsultarMisSolicitudesAsync(IdContrato, estado);
        }

        public async Task<RespuestaGeneral> ConsultarEvaluacionIncidenciaAsync(int Inci_Id)
        {
            return await historicoRepository.ConsultarEvaluacionIncidenciaAsync(Inci_Id);
        }
    }
}