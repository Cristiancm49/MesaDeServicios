using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IEvidenciaService
    {
        Task<RespuestaGeneral> InsertarEvidenciaAsync(int inciId, IFormFile soporte);
        Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId);
    }
}