using MicroApi.Seguridad.Domain.Models.Oracle;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroApi.Seguridad.Data.Context
{
    public class ModelContextORACLE : DbContext
    {
        public ModelContextORACLE(DbContextOptions<ModelContextORACLE> options)
            : base(options)
        {
        }

        // DbSet que representa la tabla 'areatecnica'
        public DbSet<Contrato> contratos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla 'areatecnica'
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("CONTRATO", "CONTRATOS");
                entity.HasKey(e => e.CONT_ID);
                entity.Property(e => e.CONT_ID)
                    .HasColumnName("CONT_ID");
            });
        }
    }
}
