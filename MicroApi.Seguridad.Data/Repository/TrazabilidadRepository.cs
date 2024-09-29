using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Repository
{
    public class TrazabilidadRepository : ITrazabilidadRepository
    {
        private readonly ModelContextSQL modelContext;

        public TrazabilidadRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }
        public async Task<RespuestaGeneral> GenerarDiagnosticoAsync(GenerarDiagnosticoDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var documentoUsuarioParam = new SqlParameter("@DocumentoUsuario", dto.DocumentoUsuario);
                var diagDescripcionParam = new SqlParameter("@Diag_DescripcionDiagnostico", dto.Diag_DescripcionDiagnostico);
                var diagSolucionadoParam = new SqlParameter("@Diag_Solucionado", dto.Diag_Solucionado);
                var tiSoIdParam = new SqlParameter("@TiSo_Id", (object)dto.TiSo_Id ?? DBNull.Value);
                var diagEscalableParam = new SqlParameter("@Diag_Escalable", dto.Diag_Escalable);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC GenerarDiagnosticos @Inci_Id, @DocumentoUsuario, @Diag_DescripcionDiagnostico, @Diag_Solucionado, @TiSo_Id, @Diag_Escalable, @ErrorMessage OUTPUT",
                    inciIdParam,
                    documentoUsuarioParam,
                    diagDescripcionParam,
                    diagSolucionadoParam,
                    tiSoIdParam,
                    diagEscalableParam,
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
                    respuesta.Answer = "Diagnóstico generado exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error generando el diagnóstico: {ex.Message}";
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
