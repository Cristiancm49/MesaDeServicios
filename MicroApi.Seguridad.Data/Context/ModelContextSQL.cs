using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models;

namespace MicroApi.Seguridad.Data.Context
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        // Demás clases se agregan aquí
    }
}