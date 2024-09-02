using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Domain.Models.Persona;
using MicroApi.Seguridad.Domain.Models.EncuestaCalidad;

namespace MicroApi.Seguridad.Api
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options)
            : base(options)
        {
        }

        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<IncidenciaAreaTecnica> IncidenciaAreaTecnicas { get; set; }
        public DbSet<IncidenciaAreaTecnicaCategoria> IncidenciaAreaTecnicaCategorias { get; set; }
        public DbSet<IncidenciaEstado> IncidenciaEstados { get; set; }
        public DbSet<IncidenciaPrioridad> IncidenciaPrioridades { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<PersonaGeneral> PersonaGenerales { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<InventarioBloqueEdificio> InventarioBloqueEdificios { get; set; }
        public DbSet<InventarioPisoOficina> InventarioPisoOficinas { get; set; }
        public DbSet<EncuestaCalidad> EncuestasCalidad { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Incidencia
            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.HasKey(e => e.Inci_Id);

                entity.HasOne(e => e.ContratoSolicitante)
                    .WithMany()
                    .HasForeignKey(e => e.Cont_IdSolicitante)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ContratoAdminExc)
                    .WithMany()
                    .HasForeignKey(e => e.Cont_IdAdminExc)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.IncidenciaAreaTecnica)
                    .WithMany()
                    .HasForeignKey(e => e.ArTe_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.IncidenciaEstado)
                    .WithMany()
                    .HasForeignKey(e => e.InEs_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.IncidenciaPrioridad)
                    .WithMany()
                    .HasForeignKey(e => e.InPr_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.Usua_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de IncidenciaAreaTecnica
            modelBuilder.Entity<IncidenciaAreaTecnica>(entity =>
            {
                entity.HasKey(e => e.ArTe_Id);

                entity.HasOne(e => e.IncidenciaAreaTecnicaCategoria)
                    .WithMany()
                    .HasForeignKey(e => e.CaAr_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de IncidenciaAreaTecnicaCategoria
            modelBuilder.Entity<IncidenciaAreaTecnicaCategoria>(entity =>
            {
                entity.HasKey(e => e.CaAr_Id);
            });

            // Configuración de IncidenciaEstado
            modelBuilder.Entity<IncidenciaEstado>(entity =>
            {
                entity.HasKey(e => e.InEs_Id);
            });

            // Configuración de IncidenciaPrioridad
            modelBuilder.Entity<IncidenciaPrioridad>(entity =>
            {
                entity.HasKey(e => e.InPr_Id);
            });

            // Configuración de Contrato
            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(e => e.Cont_Id);

                entity.HasOne(e => e.PersonaGeneral)
                    .WithMany()
                    .HasForeignKey(e => e.PeGe_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Unidad)
                    .WithMany()
                    .HasForeignKey(e => e.Unid_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de PersonaGeneral
            modelBuilder.Entity<PersonaGeneral>(entity =>
            {
                entity.HasKey(e => e.PeGe_Id);
            });

            // Configuración de Unidad
            modelBuilder.Entity<Unidad>(entity =>
            {
                entity.HasKey(e => e.Unid_Id);
            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Clave primaria
                entity.HasKey(e => e.Usua_Id);

                // Configuración de la relación con la entidad Contrato
                entity.HasOne(e => e.Contrato)
                    .WithMany()
                    .HasForeignKey(e => e.Cont_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuración de la relación con la entidad UsuarioRol
                entity.HasOne(e => e.UsuarioRol)
                    .WithMany()
                    .HasForeignKey(e => e.UsRo_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuración de la restricción de unicidad en Cont_Id
                entity.HasIndex(e => e.Cont_Id)
                    .IsUnique()
                    .HasDatabaseName("UQ_Usuario_Cont_Id");
            });


            // Configuración de UsuarioRol
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.UsRo_Id);
            });

            // Configuración de InventarioBloqueEdificio
            modelBuilder.Entity<InventarioBloqueEdificio>(entity =>
            {
                entity.HasKey(e => e.BlEd_Id);
                entity.Property(e => e.BlEd_Nombre).IsRequired().HasMaxLength(50);

                // Configuración de la relación
                entity.HasMany(e => e.PisosOficinas)
                      .WithOne(p => p.BloqueEdificio)
                      .HasForeignKey(p => p.BlEd_Id)
                      .OnDelete(DeleteBehavior.Cascade); // o DeleteBehavior.Restrict si prefieres
            });

            // Configuración de InventarioPisoOficina
            modelBuilder.Entity<InventarioPisoOficina>(entity =>
            {
                entity.HasKey(e => e.PiOf_Id);
                entity.Property(e => e.PiOf_Nombre).IsRequired().HasMaxLength(50);

                // Configuración de la relación
                entity.HasOne(e => e.BloqueEdificio)
                      .WithMany(b => b.PisosOficinas)
                      .HasForeignKey(e => e.BlEd_Id)
                      .OnDelete(DeleteBehavior.Cascade); // o DeleteBehavior.Restrict si prefieres
            });
        }
    }
}