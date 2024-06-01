using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Data.Utilities;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Repository
{
    public class UtilitiesRepository : IUtilitiesRepository
    {
        private readonly ModelContext modelContext;
        Crypto crypto;

        public UtilitiesRepository(ModelContext modelContext)
        {
            this.modelContext = modelContext;
        }

        public RespuestaGeneral Descryp(string text)
        {
            crypto = new Crypto();

            return crypto.Descrypt(text);
        }

        public bool InsertAuditoria(string writeModel, HttpContext httpContext)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                ApiPeticion api = new ApiPeticion()
                {
                    ApiFecha = dateTime,
                    ApiIp = GetIpAddress(httpContext),
                    ApiEndpoint = GetEndPoint(httpContext),
                    ApiToken = "token",
                    ApiIdpeticion = 1,
                    ApiBody = writeModel
                };
                modelContext.Add(api);
                modelContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetIpAddress(HttpContext context)
        {
            // Obtener la dirección IP del cliente
            var ipAddress = context.Connection.RemoteIpAddress;

            // Verificar si se está utilizando una dirección IP IPv4 o IPv6
            ipAddress = ipAddress.MapToIPv4();

            return ipAddress.ToString();
        }

        private string GetEndPoint(HttpContext context)
        {
            // Obtener el endpoint actual
            var endpoint = context.Request.Path;

            return endpoint.ToString();
        }
    }
}
