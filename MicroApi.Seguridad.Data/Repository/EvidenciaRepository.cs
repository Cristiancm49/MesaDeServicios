using MicroApi.Seguridad.Data.Conections;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Mongo;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Repository
{
    public class EvidenciaRepository : IEvidenciaRepository
    {
        private readonly MongoConnection mongoConnection;

        public EvidenciaRepository(MongoConnection mongoConnection)
        {
            this.mongoConnection = mongoConnection;
        }

        public async Task<RespuestaGeneral> InsertarEvidenciaAsync(int inciId, IFormFile soporte)
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                // Verifica si el archivo es nulo o está vacío
                if (soporte == null || soporte.Length == 0)
                {
                    respuesta.Status = "Error";
                    respuesta.Answer = "El archivo no puede estar vacío.";
                    respuesta.StatusCode = 400; // Bad Request
                    return respuesta;
                }

                // Convertir el archivo IFormFile a un arreglo de bytes
                using var memoryStream = new MemoryStream();
                await soporte.CopyToAsync(memoryStream);
                var fileBytes = memoryStream.ToArray();

                // Crear un objeto BsonBinaryData con los bytes del archivo
                var bsonData = new BsonBinaryData(fileBytes);

                var evidencia = new Evidencia
                {
                    Inci_Id = inciId,            // Almacenar el ID de la incidencia directamente
                    Soporte = bsonData,          // Almacenar el archivo en formato BsonBinaryData
                    FechaCargue = DateTime.UtcNow.AddHours(-5)
                };

                var collection = mongoConnection.GetEvidenciaCollection(); // Obtener la colección de evidencias
                await collection.InsertOneAsync(evidencia); // Insertar la evidencia

                respuesta.Status = "Success";
                respuesta.Answer = "Evidencia insertada exitosamente";
                respuesta.StatusCode = 200;
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error insertando la evidencia: {ex.Message}";
                respuesta.StatusCode = 500;
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message;
            }
            finally
            {
                respuesta.RequestId = Guid.NewGuid().ToString();
            }
            return respuesta;
        }

        public async Task<RespuestaGeneral> ConsultarEvidenciasAsync(int inciId)
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                var collection = mongoConnection.GetEvidenciaCollection(); // Usar la colección adecuada
                var evidencias = await collection.Find(e => e.Inci_Id == inciId).ToListAsync();

                if (evidencias.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = evidencias;
                    respuesta.StatusCode = 200;
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron evidencias para el ID de incidencia proporcionado.";
                    respuesta.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las evidencias: {ex.Message}";
                respuesta.StatusCode = 500;
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message;
            }
            finally
            {
                respuesta.RequestId = Guid.NewGuid().ToString();
            }
            return respuesta;
        }
    }
}