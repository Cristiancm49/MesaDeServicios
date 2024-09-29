using MicroApi.Seguridad.Data.Context;
using MicroApi.Seguridad.Domain.Interfaces;
using MicroApi.Seguridad.Domain.Models.Persona;
using MicroApi.Seguridad.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Repository
{
    public class UnidadRepository : IUnidadRepository
    {
        private readonly ModelContextSQL modelContext;

        public UnidadRepository(ModelContextSQL modelContext)
        {
            this.modelContext = modelContext;
        }

        public async Task<RespuestaGeneral> GetUnidadesAsync()
        {
            try
            {
                var unidades = await modelContext.Unidades.ToListAsync();
                if (unidades == null || !unidades.Any())
                {
                    return new RespuestaGeneral
                    {
                        Status = "NotFound",
                        Answer = "No se encontraron unidades.",
                        StatusCode = 404
                    }; // Respuesta 404 si no hay unidades
                }

                return new RespuestaGeneral
                {
                    Status = "Success",
                    Answer = "Unidades recuperadas exitosamente.",
                    Data = unidades,
                    StatusCode = 200
                }; // Respuesta 200 con la lista de unidades
            }
            catch (Exception ex)
            {
                return new RespuestaGeneral
                {
                    Status = "Error",
                    Answer = $"Ocurrió un error al recuperar las unidades: {ex.Message}",
                    StatusCode = 500,
                    Errors = new List<string> { ex.Message }
                }; // Respuesta 500 en caso de excepción
            }
        }

        public async Task<RespuestaGeneral> GetUnidadByIdAsync(int id)
        {
            try
            {
                var unidad = await modelContext.Unidades.FindAsync(id);
                if (unidad == null)
                {
                    return new RespuestaGeneral
                    {
                        Status = "NotFound",
                        Answer = "Unidad no encontrada.",
                        StatusCode = 404
                    }; // Respuesta 404 si no se encuentra la unidad
                }

                return new RespuestaGeneral
                {
                    Status = "Success",
                    Answer = "Unidad recuperada exitosamente.",
                    Data = unidad,
                    StatusCode = 200
                }; // Respuesta 200 con la unidad encontrada
            }
            catch (Exception ex)
            {
                return new RespuestaGeneral
                {
                    Status = "Error",
                    Answer = $"Ocurrió un error al recuperar la unidad: {ex.Message}",
                    StatusCode = 500,
                    Errors = new List<string> { ex.Message }
                }; // Respuesta 500 en caso de excepción
            }
        }
    }
}
