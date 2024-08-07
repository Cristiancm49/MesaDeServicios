using MicroApi.Seguridad.Api.Utilidades;
using MicroApi.Seguridad.Application.Interfaces;
using MicroApi.Seguridad.Application.Services;
using MicroApi.Seguridad.Data.Utilities;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.zEjemplos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroApi.Seguridad.Api.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class Estudiante : ControllerBase
    {
        private readonly WriteHttpRequests writeHttpRequests;
        private readonly IUtilitiesService utilitiesService;
        private readonly IEstudianteService estudianteService;
        private readonly string connectionString;
        private readonly Crypto _crypto;

        public Estudiante(IUtilitiesService utilitiesService, IEstudianteService estudianteService,
            IOptions<AppSettings> appSettings)
        {
            this.utilitiesService = utilitiesService;
            this.estudianteService = estudianteService;
            writeHttpRequests = new WriteHttpRequests();
            connectionString = appSettings.Value.OracleConnection;
            _crypto = new Crypto();
        }

        // POST api/<Estudiante>
        [HttpPost("xxxxxx")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<VsProgramahistoricoest>>> Post(SolicitudPG pg_ID)
        {
            utilitiesService.InsertAuditoria(writeHttpRequests.GetRequestBodyJson(pg_ID), HttpContext);
            return await estudianteService.VsProgramaHistorico(_crypto.Descrypt(pg_ID.Pg_ID).Answer);
        }

    }
}
