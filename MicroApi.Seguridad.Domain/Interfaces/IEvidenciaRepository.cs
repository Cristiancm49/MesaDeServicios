using MicroApi.Seguridad.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IEvidenciaRepository
    {
        Task<RespuestaGeneral> InsertarEvidenciaAsync(int inciId, IFormFile soporte);
        Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId);
    }
}