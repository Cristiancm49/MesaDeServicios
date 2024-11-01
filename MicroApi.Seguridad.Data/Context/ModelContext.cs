using MicroApi.Seguridad.Domain.Models.zEjemplos;
using Microsoft.EntityFrameworkCore;

namespace MicroApi.Seguridad.Data.Context;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ApiPeticion> ApiAuditoria { get; set; }
    public virtual DbSet<VsProgramahistoricoest> VsProgramahistoricoests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApiPeticion>(entity =>
        {
            entity.HasKey(e => e.ApiIdpeticion).HasName("API_PETICION_PK");

            entity.ToTable("API_PETICION", "API_NET6");

            entity.Property(e => e.ApiIdpeticion)
                .HasColumnType("NUMBER(20)")
                .HasColumnName("API_IDPETICION");
            entity.Property(e => e.ApiBody)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("API_BODY");
            entity.Property(e => e.ApiEndpoint)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("API_ENDPOINT");
            entity.Property(e => e.ApiFecha)
                .HasColumnType("DATE")
                .HasColumnName("API_FECHA");
            entity.Property(e => e.ApiIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("API_IP");
            entity.Property(e => e.ApiToken)
                .HasColumnType("CLOB")
                .HasColumnName("API_TOKEN");
        });

        modelBuilder.Entity<VsProgramahistoricoest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VS_PROGRAMAHISTORICOEST", "API_NET6");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ESTADO");
            entity.Property(e => e.Idestp)
                .HasColumnType("NUMBER")
                .HasColumnName("IDESTP");
            entity.Property(e => e.Idprog)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("IDPROG");
            entity.Property(e => e.PegeId)
                .HasColumnType("NUMBER(30)")
                .HasColumnName("PEGE_ID");
            entity.Property(e => e.Pensumdescripcion)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("PENSUMDESCRIPCION");
            entity.Property(e => e.Programa)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("PROGRAMA");
            entity.Property(e => e.Promedio)
                .HasColumnType("NUMBER(10,4)")
                .HasColumnName("PROMEDIO");
            entity.Property(e => e.Semestre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SEMESTRE");
        });

        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
