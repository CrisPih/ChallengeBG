using System;
using System.Collections.Generic;
using ChallengeBG.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChallengeBG.Infrastructure.Data;

public partial class ChallengeBGContext : DbContext
{
    public ChallengeBGContext(DbContextOptions<ChallengeBGContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ProductoProveedor> ProductoProveedor { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA39BCBE434");

            entity.HasIndex(e => e.Nombre, "UQ__Producto__75E3EFCF1024D7C1").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductoProveedor>(entity =>
        {
            entity.HasKey(e => e.ProductoProveedorId).HasName("PK__Producto__63D43B14664E3D02");

            entity.Property(e => e.NumeroLote)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoProveedor)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("FK__ProductoP__Produ__5535A963");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.ProductoProveedor)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoP__Prove__5629CD9C");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266A5977FFAF6A");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B845E0E503");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuario__6B0F5AE0B370285B").IsUnique();

            entity.Property(e => e.ClaveHash)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
