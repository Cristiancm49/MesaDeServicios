using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MicroApi.Seguridad.Api.Utilidades
{
    public class WriteHttpRequests
    {
        public bool WriteDocument(WriteModel writeModel, HttpContext httpContext)
        {
            try
            {
                DateTime now = DateTime.Now;
                var ruta = $"./wwwroot/{now.ToString("yyyy-MM-dd")}.txt";
                using (StreamWriter writer = new StreamWriter(ruta, append: true))
                {
                    writer.WriteLine($"{GetIpAddress(httpContext)} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")} {GetEndPoint(httpContext)}, Body: {writeModel.Body}");
                }
                return true;
            }
            catch
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

        public string GetRequestBodyJson(object requestBody)
        {
            string requestBodyJson = "";
            try
            {
                requestBodyJson = JsonSerializer.Serialize(requestBody);
            }
            catch (JsonException)
            {
                requestBodyJson = requestBody.ToString();
            }
            return requestBodyJson;
        }
    }
}
