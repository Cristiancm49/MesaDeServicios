using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Incidencia;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
using MicroApi.Seguridad.Domain.Interfaces;
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
    public class SeguimientoRepository : ISeguimientoRepository
    {
        private readonly ModelContextSQL modelContext;

        public SeguimientoRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }
        public async Task<RespuestaGeneral> ConsultarSeguimientoIncidenciaAsync()
        {

            var respuesta = new RespuestaGeneral();
            try
            {
                // Consulta LINQ equivalente a la consulta SQL proporcionada
                var incidencias = await (from i in modelContext.Incidencias
                                         join c in modelContext.Contratos on i.Cont_IdSolicitante equals c.Cont_Id into contratoJoin
                                         from c in contratoJoin.DefaultIfEmpty() // Left join con contrato del solicitante

                                         join pg in modelContext.PersonasGenerales on c.PeGe_Id equals pg.PeGe_Id into pgJoin
                                         from pg in pgJoin.DefaultIfEmpty() // Left join con PersonaGeneral del solicitante

                                         join u in modelContext.Unidades on c.Unid_Id equals u.Unid_Id into unidadJoin
                                         from u in unidadJoin.DefaultIfEmpty() // Left join con Unidad del solicitante

                                         join p in modelContext.IncidenciasPrioridad on i.InPr_Id equals p.InPr_Id into prioridadJoin
                                         from p in prioridadJoin.DefaultIfEmpty() // Left join con IncidenciaPrioridad

                                         join it in modelContext.IncidenciasTrazabilidad on i.Inci_Id equals it.Inci_Id into trazabilidadJoin
                                         from it in trazabilidadJoin.DefaultIfEmpty() // Left join con Trazabilidad

                                         join encargado in modelContext.Usuarios on i.Inci_UsuarioAsignado equals encargado.Usua_Id into encargadoJoin
                                         from encargado in encargadoJoin.DefaultIfEmpty() // Left join con Usuario encargado

                                         join encargado_c in modelContext.Contratos on new { encargado.Cont_Id, encargado.PeGe_Id, encargado.Unid_Id }
                                            equals new { encargado_c.Cont_Id, encargado_c.PeGe_Id, encargado_c.Unid_Id } into encargadoContratoJoin
                                         from encargado_c in encargadoContratoJoin.DefaultIfEmpty() // Left join con contrato del encargado

                                         join encargado_pg in modelContext.PersonasGenerales on encargado_c.PeGe_Id equals encargado_pg.PeGe_Id into encargadoPgJoin
                                         from encargado_pg in encargadoPgJoin.DefaultIfEmpty() // Left join con PersonaGeneral del encargado

                                         join estado in modelContext.IncidenciasTrazabilidadEstado on i.Inci_EstadoActual equals estado.TrEs_Id

                                         where i.Inci_EstadoActual != 1 && i.Inci_EstadoActual != 2 && i.Inci_EstadoActual != 8 && i.Inci_EstadoActual != 9
                                         && it.InTr_FechaGenerada == (from sub_it in modelContext.IncidenciasTrazabilidad
                                                                      where sub_it.Inci_Id == i.Inci_Id
                                                                      select sub_it.InTr_FechaGenerada).Max() // Subconsulta para obtener la fecha máxima

                                         orderby i.Inci_FechaRegistro ascending
                                         select new
                                         {
                                             i.Inci_Id,
                                             NombreCompletoSolicitante = $"{pg.PeGe_PrimerNombre} {pg.PeGe_SegundoNombre ?? string.Empty} {pg.PeGe_PrimerApellido} {pg.PeGe_SegundoApellido ?? string.Empty}",
                                             CargoSolicitante = c.Cont_Cargo,
                                             UnidadSolicitante = u.Unid_Nombre,
                                             i.Inci_FechaRegistro,
                                             NombrePrioridad = p.InPr_Nombre,
                                             NombreCompletoEncargado = $"{encargado_pg.PeGe_PrimerNombre} {encargado_pg.PeGe_SegundoNombre ?? string.Empty} {encargado_pg.PeGe_PrimerApellido} {encargado_pg.PeGe_SegundoApellido ?? string.Empty}",
                                             UltimaFechaReporte = it.InTr_FechaGenerada,
                                             Estado = estado.TrEs_Nombre,
                                             Revisado = it.InTr_Revisado
                                         }).ToListAsync();
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
