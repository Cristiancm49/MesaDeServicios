using MicroApi.Seguridad.Domain.Models.Oracle;
using Microsoft.EntityFrameworkCore;

namespace MicroApi.Seguridad.Data.Context
{
    public class ModelContextORACLE : DbContext
    {
        public ModelContextORACLE(DbContextOptions<ModelContextORACLE> options)
            : base(options)
        {
        }

        public DbSet<PersonaGeneral> PersonasGenerales { get; set; }
        public DbSet<PersonaNaturalGeneral> PersonasNaturalesGenerales { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<TipoNombramiento> TiposNombramiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contrato>(entity =>
            {
                // Relación con PersonaGeneral (Contratista)
                entity.HasOne(c => c.PersonaGeneral)
                      .WithMany(pg => pg.Contratos)
                      .HasForeignKey(c => c.PeGe_IdContratista)
                      .HasConstraintName("FK_CONTRATO_PERSONAGENERAL");

                // Relación con Unidad
                entity.HasOne(c => c.Unidad)
                      .WithMany(u => u.Contratos)
                      .HasForeignKey(c => c.Unid_Id)
                      .HasConstraintName("FK_CONTRATO_UNIDAD");

                // Relación con TipoNombramiento
                entity.HasOne(c => c.TipoNombramiento)
                      .WithMany(tn => tn.Contratos)
                      .HasForeignKey(c => c.Tnom_Id)
                      .HasConstraintName("FK_CONTRATO_TIPONOMBRAMIENTO");
            });
        }
    }
}