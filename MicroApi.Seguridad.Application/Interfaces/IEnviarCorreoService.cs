using MicroApi.Seguridad.Domain.DTOs.Correo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Interfaces
{
    public interface IEnviarCorreoService
    {
        Task NotificarCorreoAsync(NotificarCorreoDTO dto);
    }
}