using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models;

namespace MicroApi.Seguridad.Data.Context
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options) : base(options)
        {
        }
        public DbSet<Persona> Persona { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().HasKey(c => c.Pk_Persona); // Definir IdCategoria como clave primaria
        }
    }
}