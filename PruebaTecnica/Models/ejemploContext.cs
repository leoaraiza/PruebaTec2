using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Models
{
    public partial class ejemploContext : DbContext
    {
        public ejemploContext()
        {
        }

        public ejemploContext(DbContextOptions<ejemploContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;


        public async Task<Producto> Get(int? id)
        {
            return await Productos.FirstOrDefaultAsync(x => x.Idproductos == id);
        }

        public async Task<Venta> GetVenta(int? id)
        {
            return await Ventas.FirstOrDefaultAsync(x => x.Idventas == id);
        }

        public async Task<bool> ActualizaVenta(RegistrarVenta venta)

        {
            var producto = await this.Get(venta.Idproductos);
            if (producto != null)
            {
                producto.Existencias -= venta.CantidadVendida;
                EntityEntry<Producto> respuesta = Productos.Update(producto);
                await SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task<ArticuloComprado> RegistraVenta(RegistrarVenta item)
        { 
                Venta venta = new Venta
                {
                    Idventas = null,
                    Idproductos = item.Idproductos,
                    Fecha = item.Fecha,
                    CantidadVendida = item.CantidadVendida
                };

                EntityEntry<Venta> respuesta = await Ventas.AddAsync(venta);
                await SaveChangesAsync();
                var obj=await GetVenta(respuesta.Entity.Idventas);
               

                ArticuloComprado articuloComprado = new ArticuloComprado
                {
                    Idventas = obj.Idventas,
                    Idproductos = item.Idproductos,
                    Titulo = item.Titulo,
                    Descripcion= item.Descripcion,
                    PrecioUnitario= item.PrecioUnitario,
                    CantidadVendida = item.CantidadVendida,
                    Fecha = item.Fecha
                };
            
            return articuloComprado;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproductos)
                    .HasName("PK__Producto__D6304974851D5CBC");

                entity.Property(e => e.Idproductos).HasColumnName("IDProductos");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(7, 2)");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.Idventas)
                    .HasName("PK__Ventas__A19910318235AA2D");

                entity.Property(e => e.Idventas).HasColumnName("IDVentas");

                entity.Property(e => e.Idproductos).HasColumnName("IDProductos");

                entity.HasOne(d => d.IdproductosNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Idproductos)
                    .HasConstraintName("FK__Ventas__IDProduc__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
