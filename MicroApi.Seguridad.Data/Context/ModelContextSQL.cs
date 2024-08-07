using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models;
using MicroApi.Seguridad.Domain.Models.Incidencias;


namespace MicroApi.Seguridad.Api
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options) : base(options)
        {
        }

        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<ChairaLogin> ChairaLogins { get; set; }
        public DbSet<DependenciaLogin> DependenciaLogins { get; set; }
        public DbSet<AreaTecnica> AreaTecnicas { get; set; }
        public DbSet<CategoriaAreaTec> CategoriaAreaTec { get; set; }
        public DbSet<EstadoIncidencia> EstadoIncidencias { get; set; }
        public DbSet<Prioridad> Prioridades { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<RolModulo> RolModulos { get; set; }
        public DbSet<SalaB7> SalaB7s { get; set; }
        public DbSet<TipoDispositivo> TipoDispositivos { get; set; }
        public DbSet<BloqueEdificio> BloqueEdificios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación de Incidencia con ChairaLogin (Solicitante)
            modelBuilder.Entity<Incidencia>()
                .HasOne<ChairaLogin>()
                .WithMany()
                .HasForeignKey(i => i.IdSolicitante_Incidencias)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Incidencia con ChairaLogin (Administrador)
            modelBuilder.Entity<Incidencia>()
                .HasOne<ChairaLogin>()
                .WithMany()
                .HasForeignKey(i => i.IdAdmin_IncidenciasExc)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Incidencia con AreaTecnica
            modelBuilder.Entity<Incidencia>()
                .HasOne<AreaTecnica>()
                .WithMany()
                .HasForeignKey(i => i.Id_AreaTec)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Incidencia con EstadoIncidencia
            modelBuilder.Entity<Incidencia>()
                .HasOne<EstadoIncidencia>()
                .WithMany()
                .HasForeignKey(i => i.Id_Estado)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Incidencia con Prioridad
            modelBuilder.Entity<Incidencia>()
                .HasOne<Prioridad>()
                .WithMany()
                .HasForeignKey(i => i.Id_Priori)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Incidencia con Personal
            modelBuilder.Entity<Incidencia>()
                .HasOne<Personal>()
                .WithMany()
                .HasForeignKey(i => i.Id_Perso)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de ChairaLogin con DependenciaLogin
            modelBuilder.Entity<ChairaLogin>()
                .HasOne<DependenciaLogin>()
                .WithMany()
                .HasForeignKey(c => c.Id_DepenLog)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Personal con ChairaLogin
            modelBuilder.Entity<Personal>()
                .HasOne<ChairaLogin>()
                .WithMany()
                .HasForeignKey(p => p.Id_ChaLog)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de Personal con RolModulo
            modelBuilder.Entity<Personal>()
                .HasOne<RolModulo>()
                .WithMany()
                .HasForeignKey(p => p.Id_RolModulo)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de la relación de AreaTecnica con CategoriaAreaTec
            modelBuilder.Entity<AreaTecnica>()
                .HasOne<CategoriaAreaTec>()
                .WithMany()
                .HasForeignKey(a => a.Id_CatAre)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
