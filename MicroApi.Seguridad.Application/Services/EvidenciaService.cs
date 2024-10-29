using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class EvidenciaService : IEvidenciaService
    {
        private readonly IEvidenciaRepository evidenciaRepository;

        public EvidenciaService(IEvidenciaRepository evidenciaRepository)
        {
            this.evidenciaRepository = evidenciaRepository;
        }

        public Task<RespuestaGeneral> InsertarEvidenciaAsync(int inciId, IFormFile soporte)
        {
            return this.evidenciaRepository.InsertarEvidenciaAsync(inciId, soporte);
        }

        public Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId)
        {
            return this.evidenciaRepository.ConsultarEvidenciasAsync(inciId);
        }
    }
}