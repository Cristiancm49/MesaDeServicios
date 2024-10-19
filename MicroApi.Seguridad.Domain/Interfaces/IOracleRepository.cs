using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IOracleRepository
    {
        Task<RespuestaGeneral> ConsultarContratosActivosAsync(string documentoIdentidad);
    }
}