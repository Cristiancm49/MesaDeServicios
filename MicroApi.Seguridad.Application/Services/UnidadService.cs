using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Persona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class UnidadService : IUnidadService
    {
        private readonly IUnidadRepository unidadRepository;

        public UnidadService(IUnidadRepository unidadRepository)
        {
            this.unidadRepository = unidadRepository;
        }

        public async Task<RespuestaGeneral> GetUnidadesAsync() // Cambiar a RespuestaGeneral
        {
            return await unidadRepository.GetUnidadesAsync();
        }

        public async Task<RespuestaGeneral> GetUnidadByIdAsync(int id) // Cambiar a RespuestaGeneral
        {
            return await unidadRepository.GetUnidadByIdAsync(id);

        }
    }
}