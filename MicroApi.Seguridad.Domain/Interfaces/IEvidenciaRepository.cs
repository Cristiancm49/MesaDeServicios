using MicroApi.Seguridad.Domain.DTOs.Evidencias;
using MicroApi.Seguridad.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IEvidenciaRepository
    {
        Task<RespuestaGeneral> InsertarEvidenciaAsync(InsertarEvidenciaDTO dto);
        Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId);
    }
}