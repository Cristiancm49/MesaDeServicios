using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Correo;
using MicroApi.Seguridad.Domain.DTOs.Evidencias;
using MicroApi.Seguridad.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
	public class EnviarCorreoService : IEnviarCorreoService
    {
        private readonly IEnviarCorreoRepository enviarCorreoRepository;

        public EnviarCorreoService(IEnviarCorreoRepository enviarCorreoRepository)
        {
            this.enviarCorreoRepository = enviarCorreoRepository;
        }

        public async Task NotificarCorreoAsync(NotificarCorreoDTO dto)
        {
            await enviarCorreoRepository.NotificarCorreoAsync(dto);
        }
    }
}