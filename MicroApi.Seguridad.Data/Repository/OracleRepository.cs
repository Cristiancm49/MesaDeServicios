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
        private readonly ModelContextORACLE modelContext;

        public OracleRepository(ModelContextORACLE modelContext)
        {
            this.modelContext = modelContext;
        }

        // Método para consultar una persona por documento de identidad
        public async Task<RespuestaGeneral> ConsultarContratosActivosAsync(string documentoIdentidad) //88213248
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var personas = await (from pg in modelContext.PersonasGenerales
                                      join png in modelContext.PersonasNaturalesGenerales on pg.PeGe_Id equals png.PeGe_Id
                                      join c in modelContext.Contratos on pg.PeGe_Id equals c.PeGe_IdContratista
                                      join un in modelContext.Unidades on c.Unid_Id equals un.Unid_Id
                                      join tina in modelContext.TiposNombramiento on c.Tnom_Id equals tina.Tnom_Id
                                      where c.Cont_EstadoContrato == "ACTIVO"
                                            && pg.PeGe_DocumentoIdentidad == documentoIdentidad
                                      select new
                                      {
                                          pg.PeGe_Id,
                                          pg.PeGe_DocumentoIdentidad,
                                          NombreCompleto = $"{png.PeNG_PrimerNombre} {png.PeNG_SegundoNombre ?? string.Empty} {png.PeNG_PrimerApellido} {png.PeNG_SegundoApellido ?? string.Empty}",
                                          un.Unid_Id,
                                          un.Unid_Nombre,
                                          un.Unid_Telefono,
                                          un.Unid_ExtTelefono,
                                          un.Unid_Nivel,
                                          c.Cont_Id,
                                          c.Cont_Numero,
                                          tina.Tnom_Descripcion,
                                          c.Cont_FechaInicio,
                                          FechaFinContrato = c.Cont_FechaFin ?? null,
                                          c.Cont_EstadoContrato
                                      }).ToListAsync();

                if (personas.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = personas; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron personas con el documento de identidad especificado.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando personas: {ex.Message}";
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
