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
    public class OracleService : IOracleService
    {
        private readonly IOracleRepository oracleRepository;

        public OracleService(IOracleRepository oracleRepository)
        {
            this.oracleRepository = oracleRepository;
        }

        public async Task<RespuestaGeneral> ConsultarContratosActivosAsync(string documentoIdentidad)
        {
            return await oracleRepository.ConsultarContratosActivosAsync(documentoIdentidad);
        }
    }
}