using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Incidencia;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;



namespace MicroApi.Seguridad.Data.Repository
{
    public class HistoricoRepository : IHistoricoRepository
    {
        private readonly ModelContextSQL modelContext;

        public HistoricoRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }

        public async Task<RespuestaGeneral> ConsultarHistoricoIncidenciasAsync()
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                var incidencias = await (from i in modelContext.Incidencias
                                         join ar in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals ar.ArTe_Id into areaTecnicaGroup
                                         from ar in areaTecnicaGroup.DefaultIfEmpty()
                                         join ca in modelContext.IncidenciasAreaTecnicaCategoria on ar.CaAr_Id equals ca.CaAr_Id into categoriaGroup
                                         from ca in categoriaGroup.DefaultIfEmpty()
                                         join p in modelContext.IncidenciasPrioridad on i.InPr_Id equals p.InPr_Id into prioridadGroup
                                         from p in prioridadGroup.DefaultIfEmpty()
                                         join admin in modelContext.Usuarios on i.Usua_IdAdminExc equals admin.Usua_Id into adminGroup
                                         from admin in adminGroup.DefaultIfEmpty()
                                         where i.Inci_EstadoActual == 9
                                         let lastTrazabilidad = modelContext.IncidenciasTrazabilidad
                                                                 .Where(it => it.Inci_Id == i.Inci_Id)
                                                                 .OrderByDescending(it => it.InTr_FechaGenerada)
                                                                 .Select(it => it.InTr_FechaGenerada)
                                                                 .FirstOrDefault()
                                         select new
                                         {
                                             i.Inci_Id,
                                             IdContratoSolicitante = i.Cont_IdSolicitante,
                                             i.Inci_Descripcion,
                                             i.Inci_FechaRegistro,
                                             ca.CaAr_Nombre,
                                             ar.ArTe_Nombre,
                                             NombrePrioridad = p.InPr_Nombre,
                                             IdContratoAdmin = admin != null ? (long?)admin.Usua_Id : null,
                                             InTr_FechaGenerada = lastTrazabilidad,
                                             i.Inci_EstadoActual
                                         })
                         .ToListAsync();

                if (incidencias.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidencias;
                    respuesta.StatusCode = 200;
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

        public async Task<RespuestaGeneral> ConsultarMisIncidenciaCerradasAsync(int IdContrato)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var ultimaTrazabilidad = from it in modelContext.IncidenciasTrazabilidad
                                         let rn = (from subIt in modelContext.IncidenciasTrazabilidad
                                                   where subIt.Inci_Id == it.Inci_Id
                                                   orderby subIt.InTr_FechaGenerada descending
                                                   select subIt).Take(1).Count() // Contar hasta el primer elemento
                                         where rn == 1
                                         select new
                                         {
                                             it.Inci_Id,
                                             it.TrEs_Id,
                                             it.Diag_Id,
                                             it.InTr_FechaGenerada // Incluir la fecha de la última trazabilidad
                                         };

                var incidenciasCerradas = await (from i in modelContext.Incidencias
                                                 join ut in ultimaTrazabilidad on i.Inci_Id equals ut.Inci_Id
                                                 join d in modelContext.IncidenciasDiagnostico on ut.Diag_Id equals d.Diag_Id
                                                 join u in modelContext.Usuarios on d.Usua_Id equals u.Usua_Id
                                                 join at in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals at.ArTe_Id
                                                 join ca in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals ca.CaAr_Id
                                                 join ip in modelContext.IncidenciasPrioridad on i.InPr_Id equals ip.InPr_Id
                                                 where i.Inci_EstadoActual == 9 // Filtrar por estado igual a 9
                                                 && i.Cont_IdSolicitante == IdContrato // Filtrar por documento de identidad
                                                 orderby i.Inci_FechaRegistro ascending
                                                 select new
                                                 {
                                                     i.Inci_Id,
                                                     ContIdSolicitante = i.Cont_IdSolicitante,
                                                     NombreCategoria = ca.CaAr_Nombre,
                                                     AreaTecnica = at.ArTe_Nombre,
                                                     i.Inci_Descripcion,
                                                     i.Inci_FechaRegistro,
                                                     ip.InPr_Nombre,
                                                     ContIdUsuarioAsignado = u.Cont_Id,
                                                     FechaUltimaTrazabilidad = ut.InTr_FechaGenerada // Incluir la fecha de la última trazabilidad
                                                 })
                                                 .GroupBy(x => x.Inci_Id) // Agrupar por Inci_Id
                                                 .Select(g => g.FirstOrDefault()) // Seleccionar el primer elemento de cada grupo
                                                 .ToListAsync();

                if (incidenciasCerradas.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidenciasCerradas; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron incidencias cerradas.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando las incidencias cerradas: {ex.Message}";
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

        public async Task<RespuestaGeneral> ConsultarMisSolicitudesAsync(int IdContrato, bool estado)
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var ultimaTrazabilidad = from it in modelContext.IncidenciasTrazabilidad
                                         let rn = (from subIt in modelContext.IncidenciasTrazabilidad
                                                   where subIt.Inci_Id == it.Inci_Id
                                                   orderby subIt.InTr_FechaGenerada descending
                                                   select subIt).Take(1).Count() // Contar hasta el primer elemento
                                         where rn == 1
                                         select new
                                         {
                                             it.Inci_Id,
                                             it.TrEs_Id,
                                             it.Diag_Id
                                         };

                var incidenciasActivas = await (from i in modelContext.Incidencias
                                                join ut in ultimaTrazabilidad on i.Inci_Id equals ut.Inci_Id into lastTrazabilidadGroup
                                                from ut in lastTrazabilidadGroup.DefaultIfEmpty() // LEFT JOIN
                                                join d in modelContext.IncidenciasDiagnostico on ut.Diag_Id equals d.Diag_Id into diagGroup
                                                from d in diagGroup.DefaultIfEmpty() // LEFT JOIN
                                                join u in modelContext.Usuarios on d.Usua_Id equals u.Usua_Id into usuarioGroup
                                                from u in usuarioGroup.DefaultIfEmpty() // LEFT JOIN
                                                join at in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals at.ArTe_Id into areaTecnicaGroup
                                                from at in areaTecnicaGroup.DefaultIfEmpty() // LEFT JOIN
                                                join ca in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals ca.CaAr_Id into categoriaGroup
                                                from ca in categoriaGroup.DefaultIfEmpty() // LEFT JOIN
                                                join ip in modelContext.IncidenciasPrioridad on i.InPr_Id equals ip.InPr_Id into prioridadGroup
                                                from ip in prioridadGroup.DefaultIfEmpty() // LEFT JOIN
                                                where (estado == true
                                                ? (i.Inci_EstadoActual != 2 && i.Inci_EstadoActual != 7 && i.Inci_EstadoActual != 9) // Filtrar por estados 3, 4, 5, 6, 8
                                                : i.Inci_EstadoActual == 9) // Si estado != 0, solo filtrar por estado 9
                                                && i.Cont_IdSolicitante == IdContrato // Filtrar por documento de identidad
                                                orderby i.Inci_FechaRegistro ascending
                                                select new
                                                {
                                                    i.Inci_Id,
                                                    ContatoSolicitante = i.Cont_IdSolicitante,
                                                    NombreCategoria = ca.CaAr_Nombre,
                                                    AreaTecnica = at.ArTe_Nombre,
                                                    i.Inci_Descripcion,
                                                    i.Inci_FechaRegistro,
                                                    ip.InPr_Nombre,
                                                    i.Inci_EstadoActual
                                                })
                                                  .GroupBy(x => x.Inci_Id) // Agrupar por Inci_Id
                                                  .Select(g => g.FirstOrDefault()) // Seleccionar el primer elemento de cada grupo
                                                  .ToListAsync();
                if (incidenciasActivas.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidenciasActivas; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    if (estado == true)
                    {
                        respuesta.Answer = "No se encontraron incidencias activas.";
                    }
                    else
                    {
                        respuesta.Answer = "No se encontraron incidencias cerradas.";
                    }
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

        public async Task<RespuestaGeneral> ConsultarEvaluacionIncidenciaAsync(int Inci_Id)
        {
            var respuesta = new RespuestaGeneral();
            try
            {
                // Realizar la consulta a la tabla EncuestaCalidad
                var evaluacion = await modelContext.EncuestasCalidad
                    .Where(ec => ec.Inci_Id == Inci_Id) // Filtrar por Inci_Id
                    .Select(ec => new
                    {
                        ec.Inci_Id,
                        ec.EnCa_Preg1,
                        ec.EnCa_Preg2,
                        ec.EnCa_Preg3,
                        ec.EnCa_Preg4,
                        ec.EnCa_Preg5,
                        ec.EnCa_PromedioEvaluacion,
                        ec.EnCa_FechaRespuesta
                    })
                    .ToListAsync();
                if (evaluacion.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = evaluacion; // Guardar resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontró registros de evaluacion.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando la evaluacion: {ex.Message}";
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