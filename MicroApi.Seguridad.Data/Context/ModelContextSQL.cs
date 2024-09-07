using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Domain.Models.Persona;
using MicroApi.Seguridad.Domain.Models.Encuesta;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;

namespace MicroApi.Seguridad.Data.Context
{
    public class ModelContextSQL : DbContext
    {
        public ModelContextSQL(DbContextOptions<ModelContextSQL> options)
            : base(options)
        {
        }

        public DbSet<EncuestaCalidad> EncuestasCalidad { get; set; }
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<IncidenciaAreaTecnica> IncidenciasAreaTecnica { get; set; }
        public DbSet<IncidenciaAreaTecnicaCategoria> IncidenciasAreaTecnicaCategoria { get; set; }
        public DbSet<IncidenciaPrioridad> IncidenciasPrioridad { get; set; }
        public DbSet<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
        public DbSet<IncidenciaTrazabilidadEstado> IncidenciasTrazabilidadEstado { get; set; }
        public DbSet<IncidenciaTrazabilidadTipoSolucion> IncidenciasTrazabilidadTipoSolucion { get; set; }
        public DbSet<HistorialCambios> HistorialesCambios { get; set; }
        public DbSet<HistorialTipoCambio> HistorialesTipoCambio { get; set; }
        public DbSet<InventarioBloqueEdificio> InventariosBloquesEdificio { get; set; }
        public DbSet<InventarioGeneral> InventariosGenerales { get; set; }
        public DbSet<InventarioPisoOficina> InventariosPisosOficina { get; set; }
        public DbSet<InventarioTipo> InventariosTipos { get; set; }
        public DbSet<Responsable> Responsables { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<PersonaGeneral> PersonasGenerales { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para EncuestaCalidad
            modelBuilder.Entity<EncuestaCalidad>(entity =>
            {
                entity.HasKey(e => e.EnCa_Id);

                entity.Property(e => e.EnCa_PromedioEvaluacion)
                    .HasColumnType("decimal(5, 2)");

                entity.HasOne(e => e.Incidencia)
                    .WithMany(i => i.EncuestaCalidad)
                    .HasForeignKey(e => e.Inci_Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para Incidencia
            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.HasKey(e => e.Inci_Id);

                entity.Property(e => e.Inci_Descripcion)
                    .HasMaxLength(500);

                entity.HasOne(e => e.AreaTecnica)
                    .WithMany(a => a.Incidencia)
                    .HasForeignKey(e => e.ArTe_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Solicitante)
                    .WithMany(s => s.Incidencia)
                    .HasForeignKey(e => e.Cont_IdSolicitante)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Prioridad)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(e => e.InPr_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para IncidenciaAreaTecnica
            modelBuilder.Entity<IncidenciaAreaTecnica>(entity =>
            {
                entity.HasKey(e => e.ArTe_Id);

                entity.Property(e => e.ArTe_Nombre)
                    .HasMaxLength(50);

                entity.HasOne(e => e.Categoria)
                    .WithMany(c => c.IncidenciasAreaTecnica)
                    .HasForeignKey(e => e.CaAr_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para IncidenciaAreaTecnicaCategoria
            modelBuilder.Entity<IncidenciaAreaTecnicaCategoria>(entity =>
            {
                entity.HasKey(e => e.CaAr_Id);

                entity.Property(e => e.CaAr_Nombre)
                    .HasMaxLength(50);
            });

            // Configuración para HistorialCambios
            modelBuilder.Entity<HistorialCambios>(entity =>
            {
                entity.HasKey(e => e.HiCa_Id);

                entity.HasOne(e => e.InventarioGeneral)
                    .WithMany(i => i.HistorialesCambios)
                    .HasForeignKey(e => e.InGe_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TipoCambio)
                    .WithMany(tc => tc.HistorialesCambios)
                    .HasForeignKey(e => e.HiTiCa_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para HistorialTipoCambio
            modelBuilder.Entity<HistorialTipoCambio>(entity =>
            {
                entity.HasKey(e => e.HiTiCa_id);

                entity.Property(e => e.HiTiCa_Nombre)
                    .HasMaxLength(50);
            });

            // Configuración para InventarioGeneral
            modelBuilder.Entity<InventarioGeneral>(entity =>
            {
                entity.HasKey(e => e.InGe_Id);

                entity.Property(e => e.InGe_Serial)
                    .HasMaxLength(50);

                entity.HasOne(e => e.PisoOficina)
                    .WithMany(p => p.InventariosGenerales)
                    .HasForeignKey(e => e.PiOf_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tipo)
                    .WithMany(t => t.InventariosGenerales)
                    .HasForeignKey(e => e.InTi_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.InventariosGenerales)
                    .HasForeignKey(e => e.Usua_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para InventarioPisoOficina
            modelBuilder.Entity<InventarioPisoOficina>(entity =>
            {
                entity.HasKey(e => e.PiOf_Id);

                entity.Property(e => e.PiOf_Nombre)
                    .HasMaxLength(50);

                entity.HasOne(e => e.BloqueEdificio)
                    .WithMany(b => b.PisosOficina)
                    .HasForeignKey(e => e.BlEd_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para InventarioBloqueEdificio
            modelBuilder.Entity<InventarioBloqueEdificio>(entity =>
            {
                entity.HasKey(e => e.BlEd_Id);

                entity.Property(e => e.BlEd_Nombre)
                    .HasMaxLength(50);
            });

            // Configuración para Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Usua_Id);

                entity.HasOne(e => e.Contrato)
                    .WithMany(c => c.Usuarios)
                    .HasForeignKey(e => e.Cont_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UsuarioRol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(e => e.UsRo_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para IncidenciaTrazabilidad
            modelBuilder.Entity<IncidenciaTrazabilidad>(entity =>
            {
                entity.HasKey(e => e.InTr_Id);

                entity.Property(e => e.InTr_descripcion)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Incidencia)
                    .WithMany(i => i.IncidenciasTrazabilidad)
                    .HasForeignKey(e => e.Inci_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.IncidenciasTrazabilidad)
                    .HasForeignKey(e => e.Usua_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TrazabilidadEstado)
                    .WithMany(te => te.IncidenciasTrazabilidad)
                    .HasForeignKey(e => e.InTrEs_Id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TrazabilidadTipoSolucion)
                    .WithMany(ts => ts.IncidenciasTrazabilidad)
                    .HasForeignKey(e => e.InTrTiSo_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para UsuarioRol
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(e => e.UsRo_Id);

                entity.Property(e => e.UsRo_Nombre)
                    .HasMaxLength(50);
            });

            //Trigger de incidencia
            modelBuilder.Entity<Incidencia>()
                .ToTable(tb => tb.HasTrigger("trg_UpdateIncidencia"));
        }
    }
}