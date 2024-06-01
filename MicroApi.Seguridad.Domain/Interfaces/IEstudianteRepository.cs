using MicroApi.Seguridad.Domain.Models.Estudiante;
using MicroApi.Seguridad.Domain.Models.Solicitud;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Domain.Interfaces
{
    public interface IEstudianteRepository
    {
        Task<ActionResult<List<VsProgramahistoricoest>>> VsProgramaHistorico(string pg_Id);
    }
}
