using MicroApi.Seguridad.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface ITrazabilidadService
    {
        Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto);
    }
}