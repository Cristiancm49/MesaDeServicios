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
    }
}
