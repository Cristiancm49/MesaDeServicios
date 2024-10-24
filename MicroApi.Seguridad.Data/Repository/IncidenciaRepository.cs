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

        public async Task<RespuestaGeneral> ConsultarContratoAsync(int IdContrato)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var contratos = await (from us in modelContext.Usuarios
                                       where us.Usua_Estado == true && us.Cont_Id == IdContrato
                                       select new
                                       {
                                           IdContrato = us.Cont_Id,
                                           UsuarioId = (long?)us.Usua_Id,
                                           UsuarioRolId = (int?)us.UsRo_Id
                                       }).ToListAsync();

                if (contratos.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = contratos;
                    respuesta.StatusCode = 200;
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron contratos activos para el documento proporcionado.";
                    respuesta.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando el contrato: {ex.Message}";
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
            var inciId = new SqlParameter("@Inci_Id", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            try
            {
                var ContratoSolicitanteParam = new SqlParameter("@ContratoSolicitante", dto.IdContratoSolicitante);
                var ValorUnidadSolicitanteParam = new SqlParameter("@NivelUnidadSolicitante", dto.ValorUnidadSolicitante);
                var ContratoAdminParam = new SqlParameter("@ContratoAdmin", (object)dto.IdContratoAdmin ?? DBNull.Value);
                var areaTecnicaParam = new SqlParameter("@AreaTecnica", dto.AreaTecnica);
                var descripcionParam = new SqlParameter("@Descripcion", (object)dto.Descripcion ?? DBNull.Value);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC InsertarIncidencia @ContratoSolicitante, @NivelUnidadSolicitante, @ContratoAdmin, @AreaTecnica, @Descripcion, @ErrorMessage OUTPUT, @Inci_Id OUTPUT",
                    ContratoSolicitanteParam,
                    ValorUnidadSolicitanteParam,
                    ContratoAdminParam,
                    areaTecnicaParam,
                    descripcionParam,
                    errorMessage,
                    inciId);

                if (!string.IsNullOrEmpty(errorMessage.Value?.ToString()))
                {
                    respuesta.Status = "Error";
                    respuesta.Answer = errorMessage.Value.ToString();
                    respuesta.StatusCode = 400;
                    respuesta.Errors.Add(respuesta.Answer);
                }
                else
                {
                    respuesta.Status = "Success";
                    respuesta.Answer = "Incidencia insertada exitosamente";
                    respuesta.StatusCode = 200;
                    respuesta.Data = inciId.Value;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error insertando la incidencia: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarIncidenciasRegistradasAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var incidencias = await (from i in modelContext.Incidencias
                                         join it in modelContext.IncidenciasTrazabilidad on i.Inci_Id equals it.Inci_Id into trazabilidadGroup
                                         from it in trazabilidadGroup.DefaultIfEmpty() // LEFT JOIN
                                         join ar in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals ar.ArTe_Id into areaTecnicaGroup
                                         from ar in areaTecnicaGroup.DefaultIfEmpty() // LEFT JOIN
                                         join ca in modelContext.IncidenciasAreaTecnicaCategoria on ar.CaAr_Id equals ca.CaAr_Id into categoriaGroup
                                         from ca in categoriaGroup.DefaultIfEmpty() // LEFT JOIN
                                         join p in modelContext.IncidenciasPrioridad on i.InPr_Id equals p.InPr_Id into prioridadGroup
                                         from p in prioridadGroup.DefaultIfEmpty() // LEFT JOIN
                                         join admin in modelContext.Usuarios on i.Usua_IdAdminExc equals admin.Usua_Id into adminGroup
                                         from admin in adminGroup.DefaultIfEmpty() // LEFT JOIN
                                         where i.Inci_EstadoActual == 1 // Solo incidencias con estado 1
                                         select new
                                         {
                                             i.Inci_Id,
                                             i.Cont_IdSolicitante,
                                             ca.CaAr_Nombre,
                                             ar.ArTe_Nombre,
                                             i.Inci_Descripcion,
                                             it.InTr_FechaGenerada,
                                             NombrePrioridad = p.InPr_Nombre,
                                             IdAdmin = i.Usua_IdAdminExc ?? null
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
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }

        public async Task<RespuestaGeneral> ConsultarTipoPrioridadesAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var prioridades = await (from pr in modelContext.IncidenciasPrioridad
                                         select new
                                         {
                                             pr.InPr_Id,
                                             pr.InPr_Nombre
                                         }).ToListAsync();

                if (prioridades.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = prioridades; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron prioridades.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las prioridades: {ex.Message}";
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

        public async Task<RespuestaGeneral> CambiarPrioridadAsync(CambiarPrioridadDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var newPrioridadParam = new SqlParameter("@New_Prioridad", dto.New_Prioridad);
                var motivoCambioParam = new SqlParameter("@MotivoCambio", dto.MotivoCambio ?? (object)DBNull.Value);

                await modelContext.Database.ExecuteSqlRawAsync(
                    "EXEC CambiarPorioridad @Inci_Id, @New_Prioridad, @MotivoCambio, @ErrorMessage OUTPUT",
                    inciIdParam,
                    newPrioridadParam,
                    motivoCambioParam,
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
                    respuesta.Answer = "Prioridad de la incidencia actualizada exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error actualizando la prioridad de la incidencia: {ex.Message}";
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
                                      join ur in modelContext.UsuariosRoles on u.UsRo_Id equals ur.UsRo_Id
                                      join i in modelContext.Incidencias on u.Usua_Id equals i.Inci_UsuarioAsignado into incidencias
                                      from i in incidencias.Where(i => i.Inci_EstadoActual == 3 || i.Inci_EstadoActual == 4 || i.Inci_EstadoActual == 5 || i.Inci_EstadoActual == 6).DefaultIfEmpty()
                                      where u.Usua_Estado == true
                                      && (!usRoId.HasValue || u.UsRo_Id == usRoId)
                                      group new { u, ur, i } by new
                                      {
                                          u.Usua_Id,
                                          u.Cont_Id,
                                          ur.UsRo_Nombre,
                                          ur.UsRo_Nivel,
                                          u.Usua_PromedioEvaluacion
                                      } into g
                                      select new
                                      {
                                          g.Key.Usua_Id,
                                          Cont_Id = g.Key.Cont_Id,
                                          Rol = g.Key.UsRo_Nombre,
                                          PromedioEvaluacion = g.Key.Usua_PromedioEvaluacion,
                                          NivelRol = g.Key.UsRo_Nivel,
                                          IncidenciasActivas = g.Count(i => i.i != null) // Conteo de incidencias activas
                                      }).OrderByDescending(u => u.IncidenciasActivas)
                      .ToListAsync();

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
                    respuesta.Answer = "Incidencia resuelta exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error resolviendo la incidencia: {ex.Message}";
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

        public async Task<RespuestaGeneral> ValidarEstadoResueltoAsync(int inciId)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var incidencia = await (from i in modelContext.Incidencias
                                        where i.Inci_EstadoActual == 8 && i.Inci_Id == inciId
                                        select new
                                        {
                                            i.Inci_Id,
                                            i.Inci_EstadoActual
                                        }).FirstOrDefaultAsync();

                if (incidencia != null)
                {
                    respuesta.Data = "True";
                }
                else
                {
                    respuesta.Data = "False";
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando la incidencia: {ex.Message}";
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

        public async Task<RespuestaGeneral> EvaluarCerrarIncidenciaAsync(EvaluarCerrarIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var preg1Param = new SqlParameter("@EnCa_Preg1", dto.EnCa_Preg1);
                var preg2Param = new SqlParameter("@EnCa_Preg2", dto.EnCa_Preg2);
                var preg3Param = new SqlParameter("@EnCa_Preg3", dto.EnCa_Preg3);
                var preg4Param = new SqlParameter("@EnCa_Preg4", dto.EnCa_Preg4);
                var preg5Param = new SqlParameter("@EnCa_Preg5", dto.EnCa_Preg5);

                // Ejecutar el procedimiento almacenado
                await modelContext.Database.ExecuteSqlRawAsync(
                    "EXEC EvaluarCerrarIncidencia @Inci_Id, @EnCa_Preg1, @EnCa_Preg2, @EnCa_Preg3, @EnCa_Preg4, @EnCa_Preg5, @ErrorMessage OUTPUT",
                    inciIdParam, preg1Param, preg2Param, preg3Param, preg4Param, preg5Param, errorMessage);

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
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }
    }
}