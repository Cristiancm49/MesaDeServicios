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

        // DbSet que representa la tabla 'Persona'
        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla 'PersonaGeneral' y 'PersonaNaturalGeneral'
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("PERSONAGENERAL", "SCHEMA_NAME"); // Especifica el esquema de Oracle
                entity.HasKey(e => e.PeGe_Id);

                entity.Property(e => e.PeGe_Id).HasColumnName("PEGE_ID");
                entity.Property(e => e.PeGe_DocumentoIdentidad).HasColumnName("PEGE_DOCUMENTOIDENTIDAD");

                entity.Property(e => e.PeNG_PrimerApellido).HasColumnName("PENG_PRIMERAPELLIDO");
                entity.Property(e => e.PeNG_SegundoApellido).HasColumnName("PENG_SEGUNDOAPELLIDO");
                entity.Property(e => e.PeNG_PrimerNombre).HasColumnName("PENG_PRIMERNOMBRE");
                entity.Property(e => e.PeNG_SegundoNombre).HasColumnName("PENG_SEGUNDONOMBRE");
            });
        }
    }
}