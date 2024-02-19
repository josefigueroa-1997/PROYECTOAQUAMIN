using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiAquamin.Models
{
    public partial class AQUAMINContext : DbContext
    {
        public AQUAMINContext()
        {
        }

        public AQUAMINContext(DbContextOptions<AQUAMINContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boletum> Boleta { get; set; } = null!;
        public virtual DbSet<Comuna> Comunas { get; set; } = null!;
        public virtual DbSet<Direccion> Direccions { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RutaDespacho> RutaDespachos { get; set; } = null!;
        public virtual DbSet<Rutum> Ruta { get; set; } = null!;
        public virtual DbSet<Sector> Sectors { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<VentaProducto> VentaProductos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=AQUAMIN.mssql.somee.com; DataBase=AQUAMIN;user=pepelechero_SQLLogin_1;pwd=87zhqvm9wv;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boletum>(entity =>
            {
                entity.ToTable("BOLETA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DETALLE");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.Property(e => e.Rut)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RUT");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.Boleta)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID_VENTABOLETA_FK");
            });

            modelBuilder.Entity<Comuna>(entity =>
            {
                entity.ToTable("COMUNA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdSector).HasColumnName("ID_SECTOR");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdSectorNavigation)
                    .WithMany(p => p.Comunas)
                    .HasForeignKey(d => d.IdSector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTOR_FK");
            });

            modelBuilder.Entity<Direccion>(entity =>
            {
                entity.ToTable("DIRECCION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Calle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CALLE");

                entity.Property(e => e.IdComuna).HasColumnName("ID_COMUNA");

                entity.Property(e => e.Numero).HasColumnName("NUMERO");

                entity.HasOne(d => d.IdComunaNavigation)
                    .WithMany(p => p.Direccions)
                    .HasForeignKey(d => d.IdComuna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COMUNA_FK");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("PRODUCTO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("PRECIO");

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.Property(e => e.TipoProducto)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_PRODUCTO");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("ROL");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<RutaDespacho>(entity =>
            {
                entity.ToTable("RUTA_DESPACHO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comentario)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("COMENTARIO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Foto).HasColumnName("FOTO");

                entity.Property(e => e.IdRuta).HasColumnName("ID_RUTA");

                entity.Property(e => e.IdVenta).HasColumnName("ID_VENTA");

                entity.HasOne(d => d.IdRutaNavigation)
                    .WithMany(p => p.RutaDespachos)
                    .HasForeignKey(d => d.IdRuta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID_RUTA_FK");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.RutaDespachos)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID_VENTA_FK");
            });

            modelBuilder.Entity<Rutum>(entity =>
            {
                entity.ToTable("RUTA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Tiporuta)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TIPORUTA");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("SECTOR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Constrasena).HasColumnName("CONSTRASENA");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.IdDireccion).HasColumnName("ID_DIRECCION");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.HasOne(d => d.IdDireccionNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdDireccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DIRECCION_FK");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ROL_FK");
            });

            modelBuilder.Entity<VentaProducto>(entity =>
            {
                entity.ToTable("VENTA_PRODUCTO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");

                entity.Property(e => e.Detalle)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DETALLE");

                entity.Property(e => e.EstadoPago)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO_PAGO");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.MetodoPago)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("METODO_PAGO");

                entity.Property(e => e.TipoVenta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_VENTA");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("TOTAL");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.VentaProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID_PRODUCTO_FK");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.VentaProductos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ID_USUARIO_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
