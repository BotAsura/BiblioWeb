using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BiblioWeb.Models
{
    public partial class BiblioWebDbContext : DbContext
    {
        public BiblioWebDbContext()
        {
        }

        public BiblioWebDbContext(DbContextOptions<BiblioWebDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCliente> TbCliente { get; set; }
        public virtual DbSet<TbLibro> TbLibro { get; set; }
        public virtual DbSet<TbUsuario> TbUsuario { get; set; }
        public virtual DbSet<TbVentas> TbVentas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=Asura;Initial Catalog=BiblioWebDb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__TbClient__3DD0A8CB7A9BA3A6");

                entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

                entity.Property(e => e.ApellidoMat)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.ApellidoPat)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbCliente)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario");
            });

            modelBuilder.Entity<TbLibro>(entity =>
            {
                entity.HasKey(e => e.IdLibro)
                    .HasName("PK__TbLibro__FFFE46404BF1AF6A");

                entity.Property(e => e.IdLibro).HasColumnName("Id_Libro");

                entity.Property(e => e.Autor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Precio)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__TbUsuari__63C76BE266B4115F");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TbVentas>(entity =>
            {
                entity.HasKey(e => e.IdVentas)
                    .HasName("PK__TbVentas__464C581F36A67FF4");

                entity.Property(e => e.IdVentas).HasColumnName("Id_Ventas");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdLibro).HasColumnName("Id_Libro");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.HasOne(d => d.IdLibroNavigation)
                    .WithMany(p => p.TbVentas)
                    .HasForeignKey(d => d.IdLibro)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Libto");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TbVentas)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Usuario_Venta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
