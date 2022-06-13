using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RETMINSISTEM.Models
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Model")
        {
        }

        public virtual DbSet<BODEGA> BODEGA { get; set; }
        public virtual DbSet<DESCRIPCION_KARDEX> DESCRIPCION_KARDEX { get; set; }
        public virtual DbSet<KARDEX> KARDEX { get; set; }
        public virtual DbSet<PROVEEDOR> PROVEEDOR { get; set; }
        public virtual DbSet<ROL> ROL { get; set; }
        public virtual DbSet<SUCURSAL> SUCURSAL { get; set; }
        public virtual DbSet<TIPO_KARDEX> TIPO_KARDEX { get; set; }
        public virtual DbSet<TRANSACCION> TRANSACCION { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<VEHICULO> VEHICULO { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BODEGA>()
                .Property(e => e.COD_BODEGA)
                .IsUnicode(false);

            modelBuilder.Entity<BODEGA>()
                .Property(e => e.UBICACION)
                .IsUnicode(false);

            modelBuilder.Entity<BODEGA>()
                .Property(e => e.DESCRIPCION_BODEGA)
                .IsUnicode(false);

            modelBuilder.Entity<BODEGA>()
                .HasMany(e => e.KARDEX)
                .WithRequired(e => e.BODEGA)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DESCRIPCION_KARDEX>()
                .Property(e => e.DESCRIPCION_KARDEX1)
                .IsUnicode(false);

            modelBuilder.Entity<KARDEX>()
                .Property(e => e.COD_KARDEX)
                .IsUnicode(false);

            modelBuilder.Entity<KARDEX>()
                .Property(e => e.ARTICULO)
                .IsUnicode(false);

            modelBuilder.Entity<KARDEX>()
                .Property(e => e.UNIDADES)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<KARDEX>()
                .Property(e => e.LOCALIZACION)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.COD_PROOVEDOR)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.NOMBRE_PROVEEDOR)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.APELLIDO_PROVEEDOR)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.EMPRESA_REPRESENTA)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.CONTACTO)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .Property(e => e.CORREO)
                .IsUnicode(false);

            modelBuilder.Entity<PROVEEDOR>()
                .HasMany(e => e.KARDEX)
                .WithRequired(e => e.PROVEEDOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ROL>()
                .Property(e => e.NOMBRE_ROL)
                .IsUnicode(false);

            modelBuilder.Entity<ROL>()
                .HasMany(e => e.USUARIO)
                .WithRequired(e => e.ROL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUCURSAL>()
                .Property(e => e.RUC_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<SUCURSAL>()
                .Property(e => e.COD_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<SUCURSAL>()
                .Property(e => e.NOMBRE_SUCURSAL)
                .IsUnicode(false);

            modelBuilder.Entity<SUCURSAL>()
                .Property(e => e.DIRECCION)
                .IsUnicode(false);

            modelBuilder.Entity<SUCURSAL>()
                .Property(e => e.TELEFONO)
                .IsUnicode(false);

            modelBuilder.Entity<SUCURSAL>()
                .HasMany(e => e.BODEGA)
                .WithRequired(e => e.SUCURSAL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUCURSAL>()
                .HasMany(e => e.USUARIO)
                .WithRequired(e => e.SUCURSAL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUCURSAL>()
                .HasMany(e => e.VEHICULO)
                .WithRequired(e => e.SUCURSAL)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TIPO_KARDEX>()
                .Property(e => e.NOMBRE_TIPO)
                .IsUnicode(false);

            modelBuilder.Entity<TIPO_KARDEX>()
                .HasMany(e => e.KARDEX)
                .WithRequired(e => e.TIPO_KARDEX)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRANSACCION>()
                .Property(e => e.NOMBRE_TRANSACCION)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.COD_USUARIO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.CEDULA)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.NOMBRE)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.APELLIDO)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .Property(e => e.USUARIO1)
                .IsUnicode(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.KARDEX)
                .WithRequired(e => e.USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VEHICULO>()
                .Property(e => e.COD_VEHICULO)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICULO>()
                .Property(e => e.MARCA)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICULO>()
                .Property(e => e.MODELO)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICULO>()
                .Property(e => e.COLOR)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICULO>()
                .Property(e => e.UBIC_ACTUAL)
                .IsUnicode(false);
        }
    }
}
