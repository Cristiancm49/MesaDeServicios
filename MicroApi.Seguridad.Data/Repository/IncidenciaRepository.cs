using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
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

        public async Task<RespuestaGeneral> ConsultarCategoriaAreaTecnicaAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var areasTecnicas = await (from cat in modelContext.IncidenciasAreaTecnicaCategoria
                                           select new
                                           {
                                               cat.CaAr_Id,
                                               cat.CaAr_Nombre
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
                    respuesta.Answer = "No se encontraron categorias de áreas técnicas.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las categorias de áreas técnicas: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarAreaTecnicaAsync(int CategoriaId)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var areasTecnicas = await (from at in modelContext.IncidenciasAreaTecnica
                                           where at.CaAr_Id == CategoriaId
                                           select new
                                           {
                                               at.ArTe_Id,
                                               at.ArTe_Nombre
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

        public async Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var incidencias = await (from i in modelContext.Incidencias
                                         join it in modelContext.IncidenciasTrazabilidad on i.Inci_Id equals it.Inci_Id into trazabilidadGroup
                                         from it in trazabilidadGroup.DefaultIfEmpty() // LEFT JOIN
                                         join c in modelContext.Contratos on i.Cont_IdSolicitante equals c.Cont_Id into contratoGroup
                                         from c in contratoGroup.DefaultIfEmpty() // LEFT JOIN
                                         join pg in modelContext.PersonasGenerales on c.PeGe_Id equals pg.PeGe_Id into personaGroup
                                         from pg in personaGroup.DefaultIfEmpty() // LEFT JOIN
                                         join u in modelContext.Unidades on c.Unid_Id equals u.Unid_Id into unidadGroup
                                         from u in unidadGroup.DefaultIfEmpty() // LEFT JOIN
                                         join ar in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals ar.ArTe_Id into areaTecnicaGroup
                                         from ar in areaTecnicaGroup.DefaultIfEmpty() // LEFT JOIN
                                         join ca in modelContext.IncidenciasAreaTecnicaCategoria on ar.CaAr_Id equals ca.CaAr_Id into categoriaGroup
                                         from ca in categoriaGroup.DefaultIfEmpty() // LEFT JOIN
                                         join p in modelContext.IncidenciasPrioridad on i.InPr_Id equals p.InPr_Id into prioridadGroup
                                         from p in prioridadGroup.DefaultIfEmpty() // LEFT JOIN
                                         join admin in modelContext.Usuarios on i.Usua_IdAdminExc equals admin.Usua_Id into adminGroup
                                         from admin in adminGroup.DefaultIfEmpty() // LEFT JOIN
                                         join admin_c in modelContext.Contratos on new { admin.Cont_Id, admin.PeGe_Id, admin.Unid_Id } equals new { admin_c.Cont_Id, admin_c.PeGe_Id, admin_c.Unid_Id } into adminContratoGroup
                                         from admin_c in adminContratoGroup.DefaultIfEmpty() // LEFT JOIN
                                         join admin_pg in modelContext.PersonasGenerales on admin_c.PeGe_Id equals admin_pg.PeGe_Id into adminPersonaGroup
                                         from admin_pg in adminPersonaGroup.DefaultIfEmpty() // LEFT JOIN
                                         where it.InTr_FechaGenerada == (from itSub in modelContext.IncidenciasTrazabilidad
                                                                         where itSub.Inci_Id == i.Inci_Id
                                                                         select itSub.InTr_FechaGenerada).Max()
                                               && it.TrEs_Id == 1 // Solo incidencias con estado 1
                                         select new
                                         {
                                             i.Inci_Id,
                                             NombreCompletoSolicitante = pg.PeGe_PrimerNombre + " " +
                                                                          (pg.PeGe_SegundoNombre ?? "") + " " +
                                                                          pg.PeGe_PrimerApellido + " " +
                                                                          (pg.PeGe_SegundoApellido ?? ""),
                                             c.Cont_Cargo,
                                             u.Unid_Nombre,
                                             u.Unid_Telefono,
                                             ca.CaAr_Nombre,
                                             ar.ArTe_Nombre,
                                             i.Inci_Descripcion,
                                             it.InTr_FechaGenerada,
                                             NombrePrioridad = p.InPr_Nombre,
                                             NombreCompletoAdmin = admin_pg != null
                                                 ? admin_pg.PeGe_PrimerNombre + " " +
                                                   (admin_pg.PeGe_SegundoNombre ?? "") + " " +
                                                   admin_pg.PeGe_PrimerApellido + " " +
                                                   (admin_pg.PeGe_SegundoApellido ?? "")
                                                 : null // Muestra "null" si no hay datos

                                         }).ToListAsync();
                if (incidencias.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidencias; // Asignar los resultados a Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron incidencias.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las incidencias: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarRolesUsuariosAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var usuarioRoles = await (from ur in modelContext.UsuariosRoles
                                          select new
                                          {
                                              ur.UsRo_Id,
                                              ur.UsRo_Nombre
                                          }).ToListAsync();

                if (usuarioRoles.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = usuarioRoles; // Asignar los resultados a Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron roles de usuario.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando los roles de usuario: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarUsuariosAsync(int? usRoId = null)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var usuarios = await (from u in modelContext.Usuarios
                                      join c in modelContext.Contratos on new { u.Cont_Id, u.PeGe_Id, u.Unid_Id } equals new { c.Cont_Id, c.PeGe_Id, c.Unid_Id }
                                      join pg in modelContext.PersonasGenerales on c.PeGe_Id equals pg.PeGe_Id
                                      join ur in modelContext.UsuariosRoles on u.UsRo_Id equals ur.UsRo_Id
                                      where u.Usua_Estado == true
                                      && (!usRoId.HasValue || u.UsRo_Id == usRoId)
                                      select new
                                      {
                                          u.Usua_Id,
                                          NombreCompleto = $"{pg.PeGe_PrimerNombre} {pg.PeGe_SegundoNombre ?? string.Empty} {pg.PeGe_PrimerApellido} {pg.PeGe_SegundoApellido ?? string.Empty}",
                                          NumeroDocumento = pg.PeGe_DocumentoIdentidad,
                                          Rol = ur.UsRo_Nombre,
                                          PromedioEvaluacion = u.Usua_PromedioEvaluacion,
                                          NivelRol = ur.UsRo_Nivel
                                      }).ToListAsync();

                if (usuarios.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = usuarios; // Asignar los resultados a Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron personas con este rol activo.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando los usuarios: {ex.Message}";
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

        public async Task<RespuestaGeneral> ResolverIncidenciaAsync(ResolverIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC ResolverIncidencia @Inci_Id, @ErrorMessage OUTPUT",
                    inciIdParam,
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
                    respuesta.Answer = "Incidencia cerrada exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error cerrando la incidencia: {ex.Message}";
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