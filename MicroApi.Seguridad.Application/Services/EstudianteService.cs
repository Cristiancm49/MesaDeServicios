using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Data.Conections;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.zEjemplos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository estudianteRepository;

        public EstudianteService(IEstudianteRepository estudianteRepository)
        {
            this.estudianteRepository = estudianteRepository;
        }

        public Task<ActionResult<List<VsProgramahistoricoest>>> VsProgramaHistorico(string pg_Id)
        {
            return estudianteRepository.VsProgramaHistorico(pg_Id);
        }
    }
}
