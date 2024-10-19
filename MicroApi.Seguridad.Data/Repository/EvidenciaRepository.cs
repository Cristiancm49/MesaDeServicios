using MicroApi.Seguridad.Data.Conections;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Evidencias;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MicroApi.Seguridad.Data.Repository
{
    public class EvidenciaRepository : IEvidenciaRepository
    {
        private readonly MongoConnection _mongoConnection;

        public EvidenciaRepository(MongoConnection mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        public async Task<RespuestaGeneral> InsertarEvidenciaAsync(InsertarEvidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                var evidencia = new Evidencia
                {
                    Inci_Id = dto.Inci_Id,
                    Soporte = dto.Soporte,
                    FechaCargue = DateTime.UtcNow.AddHours(-5)
                };

                var collection = _mongoConnection.GetCollection<Evidencia>("Evidencias");
                await collection.InsertOneAsync(evidencia);

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
                var collection = _mongoConnection.GetCollection<Evidencia>("Evidencias");
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