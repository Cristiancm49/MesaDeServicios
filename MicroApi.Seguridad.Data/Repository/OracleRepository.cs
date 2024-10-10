using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.Models.Oracle;
using MicroApi.Seguridad.Domain.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Incidencia;

namespace MicroApi.Seguridad.Data.Repository
{
    public class OracleRepository : IOracleRepository
    {
        private readonly ModelContextORACLE _modelContext;

        public OracleRepository(ModelContextORACLE modelContext)
        {
            _modelContext = modelContext;
        }

        // Método para consultar una persona por documento de identidad
        public async Task<RespuestaGeneral> ConsultarPersonaGeneralAsync(string documentoIdentidad)
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                var persona = await _modelContext.Personas
                    .Where(p => p.PeGe_DocumentoIdentidad == documentoIdentidad)
                    .FirstOrDefaultAsync();

                if (persona != null)
                {
                    respuesta.Status = "Success";
                    respuesta.Data = persona; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron personas.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (DbUpdateException dbEx) // Captura excepciones de base de datos
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error en la consulta: {dbEx.Message}";
                respuesta.StatusCode = 500; // Código de error interno del servidor
                respuesta.Errors.Add(dbEx.Message);
                respuesta.LocalizedMessage = dbEx.InnerException?.Message; // Mensaje localizado si existe
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las personas: {ex.Message}";
                respuesta.StatusCode = 500; // Código de error interno del servidor
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message; // Mensaje localizado si existe
            }
            finally
            {
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }

            return respuesta;
        }
    }
}
