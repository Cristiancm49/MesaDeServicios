using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.DTOs;
using MicroApi.Seguridad.Domain.DTOs.Usuario;
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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ModelContextSQL modelContext;

        public UsuarioRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }

        public async Task<RespuestaGeneral> ConsultarUsuarioRolAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var roles = await (from ur in modelContext.UsuariosRoles
                                             select new
                                             {
                                                 ur.UsRo_Id,
                                                 ur.UsRo_Nombre,
                                                 ur.UsRo_Nivel
                                             }).ToListAsync();

                if (roles.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = roles; // Guardar los resultados en Data
                    respuesta.StatusCode = 200; // Código de éxito
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron roles.";
                    respuesta.StatusCode = 404; // Código de no encontrado
                }

            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error Consultando los roles: {ex.Message}";
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

        public async Task<RespuestaGeneral> InsertarUsuarioAsync(InsertarUsuarioDTO dto)
        {
            var respuesta = new RespuestaGeneral();
            var errorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            try
            {
                var contIdParam = new SqlParameter("@Cont_Id", dto.Cont_Id);
                var usRoIdParam = new SqlParameter("@UsRo_Id", dto.UsRo_Id);

                await modelContext.Database.ExecuteSqlRawAsync(
                    "EXEC [dbo].[InsertarUsuario] @Cont_Id, @UsRo_Id, @ErrorMessage OUTPUT",
                    contIdParam,
                    usRoIdParam,
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
                    respuesta.Answer = "Usuario insertado correctamente.";
                    respuesta.StatusCode = 200;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error al insertar usuario: {ex.Message}";
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
    }
}