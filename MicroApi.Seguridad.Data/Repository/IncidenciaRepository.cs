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
        public async Task<RespuestaGeneral> ConsultarContratoAsync(long documentoPersona)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var contratos = await (from c in modelContext.Contratos
                                       join pg in modelContext.PersonasGenerales on c.PeGe_Id equals pg.PeGe_Id
                                       join u in modelContext.Unidades on c.Unid_Id equals u.Unid_Id
                                       join us in modelContext.Usuarios on new { c.Cont_Id, c.PeGe_Id, c.Unid_Id } equals new { us.Cont_Id, us.PeGe_Id, us.Unid_Id } into usuarioJoin
                                       from us in usuarioJoin.DefaultIfEmpty() // LEFT JOIN
                                       where pg.PeGe_DocumentoIdentidad == documentoPersona && c.Cont_Estado == true
                                       select new
                                       {
                                           NumeroDocumento = pg.PeGe_DocumentoIdentidad,
                                           NombreCompleto = $"{pg.PeGe_PrimerNombre} {pg.PeGe_SegundoNombre ?? string.Empty} {pg.PeGe_PrimerApellido} {pg.PeGe_SegundoApellido ?? string.Empty}",
                                           Cargo = c.Cont_Cargo,
                                           NombreUnidad = u.Unid_Nombre,
                                           UsuarioId = (long?)us.Usua_Id,
                                           UsuarioRolId = (int?)us.UsRo_Id
                                       }).ToListAsync();

                if (contratos.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = contratos; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron contratos activos para el documento proporcionado.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando el contrato: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarAreaTecnicaYCategoriaAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var areasTecnicas = await (from at in modelContext.IncidenciasAreaTecnica
                                           join cat in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals cat.CaAr_Id
                                           select new
                                           {
                                               at.ArTe_Id,
                                               at.ArTe_Nombre,
                                               CategoriaNombre = cat.CaAr_Nombre
                                           }).ToListAsync();

                if (areasTecnicas.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = areasTecnicas; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron áreas técnicas.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las áreas técnicas: {ex.Message}";
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
