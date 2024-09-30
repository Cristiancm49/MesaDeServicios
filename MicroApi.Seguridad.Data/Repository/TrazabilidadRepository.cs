using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Trazabilidad;
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
        public async Task<RespuestaGeneral> ConsultarMisIncidenciasActivasAsync(long documentoIdentidad)
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
                                                where ut.TrEs_Id != 1 // Excluir estado 1
                                                && new[] { 3, 4, 5, 6 }.Contains(ut.TrEs_Id) // Filtrar por estados específicos
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
                                                    NombreUsuario = pg_usuario.PeGe_PrimerNombre + " " + pg_usuario.PeGe_SegundoNombre + " " +
                                                                   pg_usuario.PeGe_PrimerApellido + " " + pg_usuario.PeGe_SegundoApellido,
                                                    DocumentoUsuario = pg_usuario.PeGe_DocumentoIdentidad
                                                }).ToListAsync();


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
                respuesta.RequestId = Guid.NewGuid().ToString(); // Asignar un ID único para la solicitud
            }
            return respuesta;
        }
    }
}
