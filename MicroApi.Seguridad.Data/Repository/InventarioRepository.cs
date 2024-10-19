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
	public class InventarioRepository : IInventarioRepository
    {
        private readonly ModelContextSQL modelContext;

        public InventarioRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }

        public async Task<RespuestaGeneral> ConsultarTipoEventosAsync()
        {
            var respuesta = new RespuestaGeneral();

            try
            {
                var TipoEventos = await (from te in modelContext.HojaDeVidaTipoEventos
                                         select new
                                         {
                                             TipoEventoId = te.TiEv_Id,
                                             Nombre = te.TiEv_Nombre,
                                             Descripcion = te.TiEv_Descripcion
                                         }).ToListAsync();

                if (TipoEventos.Any())
                {
                    respuesta.Status = "Success";
                    respuesta.Data = TipoEventos;
                    respuesta.StatusCode = 200;
                }
                else
                {
                    respuesta.Status = "NotFound";
                    respuesta.Answer = "No se encontraron tipos de eventos para la hoja de vid.";
                    respuesta.StatusCode = 404;
                }
            }
            catch (Exception ex)
            {
                respuesta.Status = "Error";
                respuesta.Answer = $"Error consultando los tipos de eventos: {ex.Message}";
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