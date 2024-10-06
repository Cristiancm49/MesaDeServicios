using MicroApi.Seguridad.Data.Conections;
using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.zEjemplos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Repository
{
    public class EstudianteRepository : IEstudianteRepository
    {/*
        private readonly ModelContext modelContext;
        private readonly BdConection bdConection;

        public EstudianteRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
            this.bdConection = new BdConection();
        }
        public async Task<ActionResult<List<VsProgramahistoricoest>>> VsProgramaHistorico(string pg_Id)
        {
            try
            {
                return await modelContext.VsProgramahistoricoests
                .Where(x => x.PegeId == Int32.Parse(pg_Id))
                .ToListAsync();
            }
            catch (Exception e)
            {
                return new List<VsProgramahistoricoest>();
            }
        }*/
    }
}
