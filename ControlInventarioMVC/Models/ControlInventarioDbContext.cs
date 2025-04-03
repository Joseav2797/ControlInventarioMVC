using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ControlInventarioMVC.Models;

public partial class ControlInventarioDbContext : DbContext
{
    public ControlInventarioDbContext()
    {
    }

    public ControlInventarioDbContext(DbContextOptions<ControlInventarioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<Ubicacion> Ubicaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-C1T7K8Q;Database=ControlInventarioDB;User Id=sa;Password=Abc147++*;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(100);

            // Relación de Articulo con MovimientoInventario
            entity.HasMany(e => e.MovimientoInventario)
                  .WithOne(e => e.Articulo)
                  .HasForeignKey(e => e.ArticuloId);
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.ToTable("MovimientosInventario");

            entity.HasIndex(e => e.ArticuloId, "IX_MovimientosInventario_ArticuloId");

            entity.HasIndex(e => e.UbicacionId, "IX_MovimientosInventario_UbicacionId");

            entity.Property(e => e.TipoMovimiento).HasMaxLength(50);

            entity.HasOne(d => d.Articulo)
                  .WithMany(p => p.MovimientoInventario)
                  .HasForeignKey(d => d.ArticuloId);

            entity.HasOne(d => d.Ubicacion)
                  .WithMany(p => p.MovimientoInventario)
                  .HasForeignKey(d => d.UbicacionId);
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
