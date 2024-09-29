using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;



namespace MicroApi.Seguridad.Data.Repository
{
    public class IncidenciaRepository : IIncidenciaRepository
    {
        private readonly ModelContextSQL modelContext;

        public IncidenciaRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }

        public async Task<RespuestaGeneral> InsertarIncidenciaAsync(InsertarIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", System.Data.SqlDbType.NVarChar, 255)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            try
            {
                var documentoSolicitanteParam = new SqlParameter("@DocumentoSolicitante", dto.DocumentoSolicitante);
                var documentoAdminParam = new SqlParameter("@DocumentoAdmin", (object)dto.DocumentoAdmin ?? DBNull.Value);
                var areaTecnicaParam = new SqlParameter("@AreaTecnica", dto.AreaTecnica);
                var descripcionParam = new SqlParameter("@Descripcion", (object)dto.Descripcion ?? DBNull.Value);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC InsertarIncidencia @DocumentoSolicitante, @DocumentoAdmin, @AreaTecnica, @Descripcion, @ErrorMessage OUTPUT",
                    documentoSolicitanteParam,
                    documentoAdminParam,
                    areaTecnicaParam,
                    descripcionParam,
                    errorMessage);

                // Chequear si hubo un error
                if (!string.IsNullOrEmpty(errorMessage.Value?.ToString()))
                {
                    respuesta.Status = "Error";
                    respuesta.Answer = errorMessage.Value.ToString();
                    respuesta.StatusCode = 400; // Código de error
                    respuesta.Errors.Add(respuesta.Answer);
                }
                else
                {
                    respuesta.Status = "Success";
                    respuesta.Answer = "Incidencia insertada exitosamente";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error insertando la incidencia: {ex.Message}";
                respuesta.StatusCode = 500; // Código de error interno del servidor
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message; // Mensaje localizado si existe
            }
            finally
            {
                respuesta.Timestamp = DateTime.UtcNow;
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }

        public async Task<RespuestaGeneral> RechazarIncidenciaAsync(RechazarIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var motivoRechazoParam = new SqlParameter("@MotivoRechazo", dto.InTr_MotivoRechazo);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC RechazarIncidencia @Inci_Id, @MotivoRechazo, @ErrorMessage OUTPUT",
                    inciIdParam,
                    motivoRechazoParam,
                    errorMessage);

                // Chequear si hubo un error
                if (!string.IsNullOrEmpty(errorMessage.Value?.ToString()))
                {
                    respuesta.Status = "Error";
                    respuesta.Answer = errorMessage.Value.ToString();
                    respuesta.StatusCode = 400; // Código de error
                    respuesta.Errors.Add(respuesta.Answer);
                }
                else
                {
                    respuesta.Status = "Success";
                    respuesta.Answer = "Incidencia insertada exitosamente";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error insertando la incidencia: {ex.Message}";
                respuesta.StatusCode = 500; // Código de error interno del servidor
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message; // Mensaje localizado si existe
            }
            finally
            {
                respuesta.Timestamp = DateTime.UtcNow;
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }

        public async Task<RespuestaGeneral> AsignarIncidenciaAsync(AsignarIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var usuaIdParam = new SqlParameter("@Usua_Id", dto.Usua_Id);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC AsignarIncidencia @Inci_Id, @Usua_Id, @ErrorMessage OUTPUT",
                    inciIdParam,
                    usuaIdParam,
                    errorMessage);

                if (!string.IsNullOrEmpty(errorMessage.Value?.ToString()))
                {
                    respuesta.Status = "Error";
                    respuesta.Answer = errorMessage.Value.ToString();
                    respuesta.StatusCode = 400; // Código de error
                    respuesta.Errors.Add(respuesta.Answer);
                }
                else
                {
                    respuesta.Status = "Success";
                    respuesta.Answer = "Incidencia asignada exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error asignando la incidencia: {ex.Message}";
                respuesta.StatusCode = 500; // Código de error interno del servidor
                respuesta.Errors.Add(ex.Message);
                respuesta.LocalizedMessage = ex.InnerException?.Message; // Mensaje localizado si existe
            }
            finally
            {
                respuesta.Timestamp = DateTime.UtcNow;
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }


    }
}
