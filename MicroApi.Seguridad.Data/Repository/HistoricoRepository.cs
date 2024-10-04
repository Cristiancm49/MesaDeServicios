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
                                         where i.Inci_EstadoActual == 9
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
                                             i.Inci_Descripcion,
                                             i.Inci_FechaRegistro,
                                             ca.CaAr_Nombre,
                                             ar.ArTe_Nombre,
                                             NombrePrioridad = p.InPr_Nombre,
                                             NombreCompletoAdmin = admin_pg != null
                                                 ? admin_pg.PeGe_PrimerNombre + " " +
                                                   (admin_pg.PeGe_SegundoNombre ?? "") + " " +
                                                   admin_pg.PeGe_PrimerApellido + " " +
                                                   (admin_pg.PeGe_SegundoApellido ?? "")
                                                 : null, // Muestra "null" si no hay datos
                                             i.Inci_EstadoActual
                                         })
                                         .GroupBy(x => x.Inci_Id) // Agrupar por Inci_Id
                                         .Select(g => g.FirstOrDefault()) // Seleccionar el primer elemento de cada grupo
                                         .ToListAsync();
                if (incidencias.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = incidencias; // Guardar resultados en Data
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

        public async Task<RespuestaGeneral> ConsultarMisIncidenciaCerradasAsync(long documentoIdentidad)
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
                                                 join c_usuario in modelContext.Contratos on new { u.Cont_Id, u.PeGe_Id } equals new { c_usuario.Cont_Id, c_usuario.PeGe_Id }
                                                 join pg_usuario in modelContext.PersonasGenerales on c_usuario.PeGe_Id equals pg_usuario.PeGe_Id
                                                 join c_solicitante in modelContext.Contratos on i.Cont_IdSolicitante equals c_solicitante.Cont_Id
                                                 join pg_solicitante in modelContext.PersonasGenerales on c_solicitante.PeGe_Id equals pg_solicitante.PeGe_Id
                                                 join un_solicitante in modelContext.Unidades on c_solicitante.Unid_Id equals un_solicitante.Unid_Id
                                                 join at in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals at.ArTe_Id
                                                 join ca in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals ca.CaAr_Id
                                                 join ip in modelContext.IncidenciasPrioridad on i.InPr_Id equals ip.InPr_Id
                                                 where i.Inci_EstadoActual == 9 // Filtrar por estado igual a 9
                                                 && pg_usuario.PeGe_DocumentoIdentidad == documentoIdentidad // Filtrar por documento de identidad
                                                 orderby i.Inci_FechaRegistro ascending
                                                 select new
                                                 {
                                                     i.Inci_Id,
                                                     NombreSolicitante = pg_solicitante.PeGe_PrimerNombre + " " + pg_solicitante.PeGe_SegundoNombre + " " +
                                                                        pg_solicitante.PeGe_PrimerApellido + " " + pg_solicitante.PeGe_SegundoApellido,
                                                     DocumentoSolicitante = pg_solicitante.PeGe_DocumentoIdentidad,
                                                     CargoSolicitante = c_solicitante.Cont_Cargo,
                                                     NombreDependencia = un_solicitante.Unid_Nombre,
                                                     NombreCategoria = ca.CaAr_Nombre,
                                                     AreaTecnica = at.ArTe_Nombre,
                                                     i.Inci_Descripcion,
                                                     i.Inci_FechaRegistro,
                                                     ip.InPr_Nombre,
                                                     UsuarioAsignado = pg_usuario.PeGe_PrimerNombre + " " + pg_usuario.PeGe_SegundoNombre + " " +
                                                                    pg_usuario.PeGe_PrimerApellido + " " + pg_usuario.PeGe_SegundoApellido,
                                                     DocumentoUsuarioAsignado = pg_usuario.PeGe_DocumentoIdentidad,
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

        public async Task<RespuestaGeneral> ConsultarMisSolicitudesAsync(long documentoSolicitante, bool estado)
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
                                                join c_usuario in modelContext.Contratos on new { u.Cont_Id, u.PeGe_Id } equals new { c_usuario.Cont_Id, c_usuario.PeGe_Id } into contratoGroup
                                                from c_usuario in contratoGroup.DefaultIfEmpty() // LEFT JOIN
                                                join pg_usuario in modelContext.PersonasGenerales on c_usuario.PeGe_Id equals pg_usuario.PeGe_Id into personaGroup
                                                from pg_usuario in personaGroup.DefaultIfEmpty() // LEFT JOIN
                                                join c_solicitante in modelContext.Contratos on i.Cont_IdSolicitante equals c_solicitante.Cont_Id into contratoSolicitanteGroup
                                                from c_solicitante in contratoSolicitanteGroup.DefaultIfEmpty() // LEFT JOIN
                                                join pg_solicitante in modelContext.PersonasGenerales on c_solicitante.PeGe_Id equals pg_solicitante.PeGe_Id into personaSolicitanteGroup
                                                from pg_solicitante in personaSolicitanteGroup.DefaultIfEmpty() // LEFT JOIN
                                                join un_solicitante in modelContext.Unidades on c_solicitante.Unid_Id equals un_solicitante.Unid_Id into unidadGroup
                                                from un_solicitante in unidadGroup.DefaultIfEmpty() // LEFT JOIN
                                                join at in modelContext.IncidenciasAreaTecnica on i.ArTe_Id equals at.ArTe_Id into areaTecnicaGroup
                                                from at in areaTecnicaGroup.DefaultIfEmpty() // LEFT JOIN
                                                join ca in modelContext.IncidenciasAreaTecnicaCategoria on at.CaAr_Id equals ca.CaAr_Id into categoriaGroup
                                                from ca in categoriaGroup.DefaultIfEmpty() // LEFT JOIN
                                                join ip in modelContext.IncidenciasPrioridad on i.InPr_Id equals ip.InPr_Id into prioridadGroup
                                                from ip in prioridadGroup.DefaultIfEmpty() // LEFT JOIN
                                                where (estado == true
                                                ? (i.Inci_EstadoActual != 2 && i.Inci_EstadoActual != 7 && i.Inci_EstadoActual != 9) // Filtrar por estados 3, 4, 5, 6, 8
                                                : i.Inci_EstadoActual == 9) // Si estado != 0, solo filtrar por estado 9
                                                && pg_solicitante.PeGe_DocumentoIdentidad == documentoSolicitante // Filtrar por documento de identidad
                                                orderby i.Inci_FechaRegistro ascending
                                                select new
                                                {
                                                    i.Inci_Id,
                                                    NombreSolicitante = pg_solicitante.PeGe_PrimerNombre + " " + pg_solicitante.PeGe_SegundoNombre + " " +
                                                                       pg_solicitante.PeGe_PrimerApellido + " " + pg_solicitante.PeGe_SegundoApellido,
                                                    DocumentoSolicitante = pg_solicitante.PeGe_DocumentoIdentidad,
                                                    CargoSolicitante = c_solicitante.Cont_Cargo,
                                                    NombreDependencia = un_solicitante.Unid_Nombre,
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
    }
}