using Microsoft.EntityFrameworkCore;
using MicroApi.Seguridad.Domain.Models.Inventario;
using MicroApi.Seguridad.Domain.Models.Incidencia;
using MicroApi.Seguridad.Domain.Models.Encuesta;
using MicroApi.Seguridad.Domain.Models.Trazabilidad;
using MicroApi.Seguridad.Domain.Models.Diagnostico;
using MicroApi.Seguridad.Domain.Models.Usuarios;

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
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }
        public DbSet<IncidenciaDiagnostico> IncidenciasDiagnostico { get; set; }
        public DbSet<IncidenciaDiagnosticoTipoSolucion> IncidenciasDiagnosticoTipoSolucion { get; set; }
        public DbSet<IncidenciaTrazabilidad> IncidenciasTrazabilidad { get; set; }
        public DbSet<IncidenciaTrazabilidadEstado> IncidenciasTrazabilidadEstado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la clave primaria compuesta para IncidenciaTrazabilidad
            modelBuilder.Entity<IncidenciaTrazabilidad>()
                .HasKey(it => new { it.InTr_Id, it.Inci_Id });

            // Configuración de la relación para IncidenciaTrazabilidad
            modelBuilder.Entity<IncidenciaTrazabilidad>()
                .HasOne(it => it.Incidencia)
                .WithMany(i => i.IncidenciasTrazabilidad)
                .HasForeignKey(it => it.Inci_Id);

            // Configuración de relación 1:1 entre EncuestaCalidad e Incidencia
            modelBuilder.Entity<EncuestaCalidad>()
                .HasOne(ec => ec.Incidencia)
                .WithOne(i => i.EncuestaCalidad)
                .HasForeignKey<EncuestaCalidad>(ec => ec.Inci_Id);

            // Configuración de relaciones para Incidencia
            modelBuilder.Entity<Incidencia>()
                .HasKey(i => i.Inci_Id); // Clave primaria para Incidencia

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.AdminExc)
                .WithMany(u => u.Incidencia)
                .HasForeignKey(i => i.Usua_IdAdminExc);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.AreaTecnica)
                .WithMany(at => at.Incidencia)
                .HasForeignKey(i => i.ArTe_Id);

            modelBuilder.Entity<Incidencia>()
                .HasOne(i => i.Prioridad)
                .WithMany(p => p.Incidencia)
                .HasForeignKey(i => i.InPr_Id);

            modelBuilder.Entity<Incidencia>()
                .Property(i => i.Inci_FechaRegistro)
                .HasDefaultValueSql("(((sysdatetimeoffset() AT TIME ZONE 'UTC') AT TIME ZONE 'Central Standard Time'))");

            modelBuilder.Entity<Incidencia>()
                .Property(i => i.Inci_ValorTotal)
                .HasDefaultValue(0);

            modelBuilder.Entity<Incidencia>()
                .Property(i => i.Inci_EstadoActual)
                .HasDefaultValue(1);

            // Configuración de relaciones para IncidenciaAreaTecnica
            modelBuilder.Entity<IncidenciaAreaTecnica>()
                .HasOne(iat => iat.Categoria)
                .WithMany(c => c.IncidenciasAreaTecnica)
                .HasForeignKey(iat => iat.CaAr_Id);

            // Configuración de relaciones para Usuario

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.UsuarioRol)
                .WithMany(ur => ur.Usuarios)
                .HasForeignKey(u => u.UsRo_Id);

            // Configuración de relaciones para IncidenciaDiagnostico
            modelBuilder.Entity<IncidenciaDiagnostico>()
                .HasOne(id => id.Usuario)
                .WithMany(u => u.IncidenciasDiagnostico)
                .HasForeignKey(id => id.Usua_Id);

            modelBuilder.Entity<IncidenciaDiagnostico>()
                .HasOne(id => id.TipoSolucion)
                .WithMany(ts => ts.IncidenciasDiagnostico)
                .HasForeignKey(id => id.TiSo_Id);

            // Configuración adicional para IncidenciaTrazabilidad
            modelBuilder.Entity<IncidenciaTrazabilidad>()
                .HasOne(it => it.Diagnostico)
                .WithMany(d => d.IncidenciaTrazabilidads)
                .HasForeignKey(it => it.Diag_Id);

            modelBuilder.Entity<IncidenciaTrazabilidad>()
                .HasOne(it => it.TrazabilidadEstado)
                .WithMany(te => te.IncidenciasTrazabilidad)
                .HasForeignKey(it => it.TrEs_Id);
        }
    }
}