using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BEseguridad.Models;

public partial class SeguridadInformaticaContext : DbContext
{
    public SeguridadInformaticaContext()
    {
    }

    public SeguridadInformaticaContext(DbContextOptions<SeguridadInformaticaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Amenaza> Amenazas { get; set; }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<CategoriasAmenaza> CategoriasAmenazas { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=PC-Jota\\SQLEXPRESS;Database=SeguridadInformatica;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Amenaza>(entity =>
        {
            entity.HasKey(e => e.AmenazaId).HasName("PK__Amenazas__689ECFB26F86EEAC");

            entity.Property(e => e.AmenazaId).HasColumnName("AmenazaID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Amenazas)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__Amenazas__Catego__3F466844");
        });

        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.ArticuloId).HasName("PK__Articulo__C0D7258D122A1968");

            entity.Property(e => e.ArticuloId).HasColumnName("ArticuloID");
            entity.Property(e => e.FechaPublicacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(200);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Articulos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Articulos__Usuar__45F365D3");
        });

        modelBuilder.Entity<CategoriasAmenaza>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C560972FF0");

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__Recursos__82F2B1A43A8720E1");

            entity.Property(e => e.RecursoId).HasColumnName("RecursoID");
            entity.Property(e => e.EnlaceUrl)
                .HasMaxLength(255)
                .HasColumnName("EnlaceURL");
            entity.Property(e => e.Titulo).HasMaxLength(200);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798F537A4A5");

            entity.HasIndex(e => e.Username, "UQ__Usuarios__536C85E4AAD72617").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053422D632C9").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EsAdmin).HasDefaultValue(false);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
