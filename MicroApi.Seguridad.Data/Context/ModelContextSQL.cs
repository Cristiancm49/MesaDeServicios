using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models.Incidencias;
using MicroApi.Seguridad.Domain.Models.Chaira;
using MicroApi.Seguridad.Domain.Models.PersonalModulo;
using MicroApi.Seguridad.Domain.Models.Inventario;


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
        public DbSet<AreaTecnica> AreaTecnicas { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<CategoriaAreaTec> Categorias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la entidad Incidencia
            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.ChairaLoginSolicitante)
                .WithMany(c => c.IncidenciasSolicitante)
                .HasForeignKey(i => i.IdSolicitante_Incidencias)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.ChairaLoginAdminExc)
                .WithMany(c => c.IncidenciasAdminExc)
                .HasForeignKey(i => i.IdAdmin_IncidenciasExc)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.AreaTecnica)
                .WithMany(a => a.Incidencias)
                .HasForeignKey(i => i.Id_AreaTec)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.EstadoIncidencia)
                .WithMany(e => e.Incidencias)
                .HasForeignKey(i => i.Id_Estado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Prioridad)
                .WithMany(p => p.Incidencias)
                .HasForeignKey(i => i.Id_Priori)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Personal)
                .WithMany(p => p.Incidencias)
                .HasForeignKey(i => i.Id_Perso)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChairaLogin>()
                .HasOne(c => c.DependenciaLogin)
                .WithMany(d => d.ChairaLogins)
                .HasForeignKey(c => c.Id_DepenLog)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Personal>()
                .HasOne(p => p.ChairaLogin)
                .WithMany(c => c.Personals)
                .HasForeignKey(p => p.Id_ChaLog)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Personal>()
                .HasOne(p => p.RolModulo)
                .WithMany(r => r.Personals)
                .HasForeignKey(p => p.Id_RolModulo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AreaTecnica>()
                .HasOne(a => a.CategoriaAreaTec)
                .WithMany(c => c.AreaTecnicas)
                .HasForeignKey(a => a.Id_CatAre)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoriaAreaTec>()
                .HasKey(c => c.Id_CatAre);
        }

    }
}