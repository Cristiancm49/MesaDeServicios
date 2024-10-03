using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IHistoricoService
    {
        Task<RespuestaGeneral> ConsultarHistoricoIncidenciasAsync();
    }
}
