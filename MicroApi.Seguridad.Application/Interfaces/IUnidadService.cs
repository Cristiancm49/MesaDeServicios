using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IUnidadService
    {
        Task<RespuestaGeneral> GetUnidadesAsync(); // Cambiar a RespuestaGeneral
        Task<RespuestaGeneral> GetUnidadByIdAsync(int id); // Cambiar a RespuestaGeneral
    }
}