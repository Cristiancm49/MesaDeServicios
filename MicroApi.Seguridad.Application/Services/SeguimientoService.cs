using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Data.Repository;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.Interfaces;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class SeguimientoService : ISeguimientoService
    {
        private readonly ISeguimientoRepository seguimientoRepository;

        public SeguimientoService(ISeguimientoRepository seguimientoRepository)
        {
            this.seguimientoRepository = seguimientoRepository;
        }

        public async Task<RespuestaGeneral> ConsultarSeguimientoIncidenciaAsync()
        {
            return await seguimientoRepository.ConsultarSeguimientoIncidenciaAsync();
        }

        public async Task<RespuestaGeneral> ConsultarTrazabilidadIncidenciaAsync(int incidenciaId)
        {
            return await seguimientoRepository.ConsultarTrazabilidadIncidenciaAsync(incidenciaId);
        }

        public async Task<RespuestaGeneral> ConsultarTrazabilidadGeneralAsync(int incidenciaId)
        {
            return await seguimientoRepository.ConsultarTrazabilidadGeneralAsync(incidenciaId);
        }
    }
}