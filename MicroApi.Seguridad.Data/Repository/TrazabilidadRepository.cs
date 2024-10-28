using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Oracle;
using MicroApi.Seguridad.Domain.Models.Persona;
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

        public async Task<RespuestaGeneral> ConsultarMisIncidenciasActivasAsync(int IdContrato)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var ultimaTrazabilidad = from it in modelContext.IncidenciasTrazabilidad
                                         let rn = (from subIt in modelContext.IncidenciasTrazabilidad
                                                   where subIt.Inci_Id == it.Inci_Id
                                                   orderby subIt.InTr_FechaGenerada descending
                                                   select subIt).FirstOrDefault()
                                         where rn != null && rn.InTr_Revisado == true // Solo incluir si InTr_Revisado es 1
                                         select new
                                         {
                                             it.Inci_Id,
                                             it.TrEs_Id,
                                             it.Diag_Id
                                         };

                var incidenciasActivas = await (from i in modelContext.Incidencias
                                                join ut in ultimaTrazabilidad on i.Inci_Id equals ut.Inci_Id
                                                join d in modelContext.IncidenciasDiagnostico on ut.Diag_Id equals d.Diag_Id
                                                join u in modelContext.Usuarios on d.Usua_Id equals u.Usua_Id
                                                join at in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals at.ArTe_Id
                                                join ca in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals ca.CaAr_Id
                                                join ip in modelContext.IncidenciasPrioridad on i.InPr_Id equals ip.InPr_Id
                                                where (i.Inci_EstadoActual == 3 || i.Inci_EstadoActual == 4 ||
                                                       i.Inci_EstadoActual == 5 || i.Inci_EstadoActual == 6 ||
                                                       i.Inci_EstadoActual == 8) // Filtrar por estados 3, 4, 5, 6, 8
                                                       && u.Cont_Id == IdContrato // Filtrar por contrato del usuario
                                                orderby i.Inci_FechaRegistro ascending
                                                select new
                                                {
                                                    i.Inci_Id,
                                                    ContratoSolicitante = i.Cont_IdSolicitante,
                                                    NombreCategoria = ca.CaAr_Nombre,
                                                    AreaTecnica = at.ArTe_Nombre,
                                                    i.Inci_Descripcion,
                                                    i.Inci_FechaRegistro,
                                                    ip.InPr_Nombre,
                                                    ContratoUsuarioAsignado = u.Cont_Id
                                                }).Distinct().ToListAsync();

                if (incidenciasActivas.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidenciasActivas; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron incidencias activas.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las incidencias activas: {ex.Message}";
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
                var ContratoUsuarioParam = new SqlParameter("@ContratoUsuario", dto.IdContratoUsuario);
                var diagDescripcionParam = new SqlParameter("@Diag_DescripcionDiagnostico", dto.Diag_DescripcionDiagnostico);
                var diagSolucionadoParam = new SqlParameter("@Diag_Solucionado", dto.Diag_Solucionado);
                var tiSoIdParam = new SqlParameter("@TiSo_Id", (object)dto.TiSo_Id ?? DBNull.Value);
                var diagEscalableParam = new SqlParameter("@Diag_Escalable", dto.Diag_Escalable);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC GenerarDiagnosticos @Inci_Id, @ContratoUsuario, @Diag_DescripcionDiagnostico, @Diag_Solucionado, @TiSo_Id, @Diag_Escalable, @ErrorMessage OUTPUT",
                    inciIdParam,
                    ContratoUsuarioParam,
                    diagDescripcionParam,
                    diagSolucionadoParam,
                    tiSoIdParam,
                    diagEscalableParam,
                    errorMessage);

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
                    respuesta.Answer = "Diagnóstico generado exitosamente.";
                    respuesta.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error generando el diagnóstico: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarTipoSolucionAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var tiposSoluciones = await (from ts in modelContext.IncidenciasDiagnosticoTipoSolucion
                                             select new
                                             {
                                                 ts.TiSo_Id,
                                                 ts.TiSo_Nombre,
                                                 ts.TiSo_Descripcion
                                             }).ToListAsync();

                if (tiposSoluciones.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = tiposSoluciones; // Guardar los resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron tipos de soluciones.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }

            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error Consultando los tipos de solución: {ex.Message}";
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

        public async Task<RespuestaGeneral> ReAsignarIncidenciaAsync(AsignarIncidenciaDTO dto)
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

                await modelContext.Database.ExecuteSqlRawAsync("EXEC ReAsignarIncidencia @Inci_Id, @Usua_Id, @ErrorMessage OUTPUT",
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
                    respuesta.Answer = "Incidencia ReAsignada exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error ReAsignando la incidencia: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarEscalarInternoIncidenciaAsync(int IdContrato)
        {
            var respuesta = new RespuestaGeneral();

            try { 
                var rolNivel = await (from u in modelContext.Usuarios
                                      join ur in modelContext.UsuariosRoles on u.UsRo_Id equals ur.UsRo_Id
                                      where u.Cont_Id == IdContrato
                                      select ur.UsRo_Nivel)
                                      .FirstOrDefaultAsync();

                var usuarios = await (from u in modelContext.Usuarios
                                      join ur in modelContext.UsuariosRoles on u.UsRo_Id equals ur.UsRo_Id
                                      join i in modelContext.Incidencias on u.Usua_Id equals i.Inci_UsuarioAsignado into incidencias
                                      from i in incidencias.Where(i => i.Inci_EstadoActual == 3 || i.Inci_EstadoActual == 4 || i.Inci_EstadoActual == 5 || i.Inci_EstadoActual == 6).DefaultIfEmpty()
                                      where u.Usua_Estado == true
                                      && (rolNivel == null || u.UsRo_Id > rolNivel)
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
                                          IdContrato = g.Key.Cont_Id,
                                          Rol = g.Key.UsRo_Nombre,
                                          PromedioEvaluacion = g.Key.Usua_PromedioEvaluacion,
                                          NivelRol = g.Key.UsRo_Nivel,
                                          IncidenciasActivas = g.Count(i => i.i != null)
                                      }).OrderByDescending(u => u.IncidenciasActivas)
                                      .ToListAsync();

                if (usuarios.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = usuarios; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron roles superiores.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las incidencias activas: {ex.Message}";
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

        public async Task<RespuestaGeneral> EscalarInternoIncidenciaAsync(AsignarIncidenciaDTO dto)
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

                await modelContext.Database.ExecuteSqlRawAsync("EXEC EscalarInternoIncidencia @Inci_Id, @Usua_Id, @ErrorMessage OUTPUT",
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
                    respuesta.Answer = "Incidencia Escalada (interno) exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error Escalando (interno) la incidencia: {ex.Message}";
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

        public async Task<RespuestaGeneral> EscalarExternoIncidenciaAsync(EscalarExternoIncidenciaDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var inciIdParam = new SqlParameter("@Inci_Id", dto.Inci_Id);
                var usuaIdParam = new SqlParameter("@Descripcion", dto.Descripcion);

                await modelContext.Database.ExecuteSqlRawAsync("EXEC EscalarExternoIncidencia @Inci_Id, @Descripcion, @ErrorMessage OUTPUT",
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
                    respuesta.Answer = "Incidencia Escalada (externo) exitosamente.";
                    respuesta.StatusCode = 200; // Código de éxito
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error Escalando (externo) la incidencia: {ex.Message}";
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