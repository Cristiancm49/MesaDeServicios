using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.PersonalModulo;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Incidencias;


namespace MicroApi.Seguridad.Api
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options)
            : base(options)
        {
        }

        public DbSet<Personal> Personals { get; set; }
        public DbSet<ChairaLogin> ChairaLogins { get; set; }
        public DbSet<RolModulo> RolModulos { get; set; }
        public DbSet<DependenciaLogin> DependenciaLogins { get; set; }
        public DbSet<SalaB7> SalaB7s { get; set; }
        public DbSet<BloqueEdificio> BloqueEdificios { get; set; }
        public DbSet<TipoDispositivo> TipoDispositivos { get; set; }
        public DbSet<AreaTecnica> AreasT { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para la tabla Personal
            modelBuilder.Entity<Personal>(entity =>
            {
                entity.ToTable("Personal");
                entity.HasKey(e => e.Id_Perso);

                entity.HasOne(e => e.ChairaLogin)
                    .WithMany()
                    .HasForeignKey(e => e.Id_ChaLog)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.RolModulo)
                    .WithMany()
                    .HasForeignKey(e => e.Id_RolModulo)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para la tabla ChairaLogin
            modelBuilder.Entity<ChairaLogin>(entity =>
            {
                entity.ToTable("ChairaLogin");
                entity.HasKey(e => e.Id_ChaLog);

                entity.HasOne(e => e.DependenciaLogin)
                    .WithMany()
                    .HasForeignKey(e => e.Id_DepenLog)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para la tabla RolModulo
            modelBuilder.Entity<RolModulo>(entity =>
            {
                entity.ToTable("RolModulo");
                entity.HasKey(e => e.Id_rolModulo);
            });

            // Configuración para la tabla DependenciaLogin
            modelBuilder.Entity<DependenciaLogin>(entity =>
            {
                entity.ToTable("DependenciaLogin");
                entity.HasKey(e => e.Id_DepenLog);
            });

            modelBuilder.Entity<AreaTecnica>(entity =>
            {
                entity.ToTable("AreaTecnica");
                entity.HasKey(e => e.id_AreaTec);
            });
        }
    }
}
