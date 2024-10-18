using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Evidencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IEvidenciaService
    {
        Task<RespuestaGeneral> InsertarEvidenciaAsync(InsertarEvidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId);
    }
}