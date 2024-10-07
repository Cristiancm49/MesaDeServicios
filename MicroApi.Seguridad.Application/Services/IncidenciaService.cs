using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
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

        public async Task<RespuestaGeneral> ConsultarCategoriaAreaTecnicaAsync()
        {
            return await incidenciaRepository.ConsultarCategoriaAreaTecnicaAsync();
        }

        public async Task<RespuestaGeneral> ConsultarAreaTecnicaAsync(int CategoriaId)
        {
            return await incidenciaRepository.ConsultarAreaTecnicaAsync(CategoriaId);
        }

        public async Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto)
        {
            return await incidenciaRepository.InsertarIncidenciaAsync(dto);
        }

        public async Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync()
        {
            return await incidenciaRepository.ConsultarIncidenciasRegistradasAsync();
        }

        public async Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto)
        {
            return await incidenciaRepository.RechazarIncidenciaAsync(dto);
        }

        public async Task<RespuestaGeneral> ConsultarTipoPrioridadesAsync()
        {
            return await incidenciaRepository.ConsultarTipoPrioridadesAsync();
        }

        public async Task<RespuestaGeneral> CambiarPrioridadAsync(CambiarPrioridadDTO dto)
        {
            return await incidenciaRepository.CambiarPrioridadAsync(dto);
        }

        public async Task<RespuestaGeneral> ConsultarRolesUsuariosAsync()
        {
            return await incidenciaRepository.ConsultarRolesUsuariosAsync();
        }

        public async Task<RespuestaGeneral> ConsultarUsuariosAsync(int? usRoId = null)
        {
            return await incidenciaRepository.ConsultarUsuariosAsync(usRoId);
        }

        public async Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto)
        {
            return await incidenciaRepository.AsignarIncidenciaAsync(dto);
        }

        public async Task<RespuestaGeneral> ResolverIncidenciaAsync(ResolverIncidenciaDTO dto)
        {
            return await incidenciaRepository.ResolverIncidenciaAsync(dto);
        }
        public async Task<RespuestaGeneral> EvaluarCerrarIncidenciaAsync(EvaluarCerrarIncidenciaDTO dto)
        {
            return await incidenciaRepository.EvaluarCerrarIncidenciaAsync(dto);
        }
    }
}