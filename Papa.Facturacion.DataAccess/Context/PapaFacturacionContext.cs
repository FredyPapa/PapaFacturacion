using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Papa.Facturacion.DataAccess.Context;

public partial class PapaFacturacionContext : DbContext
{
    public PapaFacturacionContext()
    {
    }

    public PapaFacturacionContext(DbContextOptions<PapaFacturacionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<CatalogoDetalle> CatalogoDetalles { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comprobante> Comprobantes { get; set; }

    public virtual DbSet<ComprobanteDetalle> ComprobanteDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Catalogo__0AB5B5A5CA15FCD9");

            entity.ToTable("Catalogo", "sch_maestro");

            entity.HasIndex(e => e.VCodigo, "UQ__Catalogo__78E9250EC9B52749").IsUnique();

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_catalogo])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");
            entity.Property(e => e.VCodigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("vCodigo");
            entity.Property(e => e.VDescripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vDescripcion");
            entity.Property(e => e.VNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vNombre");
        });

        modelBuilder.Entity<CatalogoDetalle>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Catalogo__8A2462EC743B7446");

            entity.ToTable("CatalogoDetalle", "sch_maestro");

            entity.HasIndex(e => e.VCodigo, "UQ__Catalogo__78E9250E49813D6E").IsUnique();

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_catalogo_detalle])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.ICatalogo).HasColumnName("iCatalogo");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");
            entity.Property(e => e.VCodigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("vCodigo");
            entity.Property(e => e.VDescripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vDescripcion");

            entity.HasOne(d => d.ICatalogoNavigation).WithMany(p => p.CatalogoDetalles)
                .HasForeignKey(d => d.ICatalogo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogoDetalle_ToCatalogo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Cliente__02838A5EAFE3D02A");

            entity.ToTable("Cliente", "sch_facturacion");

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_clientes])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.ITipoDocumentoCat).HasColumnName("iTipoDocumentoCat");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");
            entity.Property(e => e.VApellidoMaterno)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("vApellidoMaterno");
            entity.Property(e => e.VApellidoPaterno)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("vApellidoPaterno");
            entity.Property(e => e.VCelular)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("vCelular");
            entity.Property(e => e.VCorreoElectronico)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vCorreoElectronico");
            entity.Property(e => e.VDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vDireccion");
            entity.Property(e => e.VNombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vNombres");
            entity.Property(e => e.VNumeroDocumento)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("vNumeroDocumento");

            entity.HasOne(d => d.ITipoDocumentoCatNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.ITipoDocumentoCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_ToTipoDocumento");
        });

        modelBuilder.Entity<Comprobante>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Comproba__18E73436631A135D");

            entity.ToTable("Comprobante", "sch_facturacion");

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_comprobantes])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.DcIgv)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcIgv");
            entity.Property(e => e.DcTotaNeto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcTotaNeto");
            entity.Property(e => e.DcTotalBruto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcTotalBruto");
            entity.Property(e => e.ICliente).HasColumnName("iCliente");
            entity.Property(e => e.ITipoComprobanteCat).HasColumnName("iTipoComprobanteCat");
            entity.Property(e => e.ITipoPagoCat).HasColumnName("iTipoPagoCat");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");

            entity.HasOne(d => d.IClienteNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.ICliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comprobante_ToCliente");

            entity.HasOne(d => d.ITipoComprobanteCatNavigation).WithMany(p => p.ComprobanteITipoComprobanteCatNavigations)
                .HasForeignKey(d => d.ITipoComprobanteCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comprobante_ToTipoComprobante");

            entity.HasOne(d => d.ITipoPagoCatNavigation).WithMany(p => p.ComprobanteITipoPagoCatNavigations)
                .HasForeignKey(d => d.ITipoPagoCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comprobante_ToTipoPago");
        });

        modelBuilder.Entity<ComprobanteDetalle>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Comproba__632E8AC6BE4D87B7");

            entity.ToTable("ComprobanteDetalle", "sch_facturacion");

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_comprobantes_detalle])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.DcPrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcPrecioUnitario");
            entity.Property(e => e.DcTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcTotal");
            entity.Property(e => e.ICantidad)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("iCantidad");
            entity.Property(e => e.IComprobante).HasColumnName("iComprobante");
            entity.Property(e => e.IProducto).HasColumnName("iProducto");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");

            entity.HasOne(d => d.IComprobanteNavigation).WithMany(p => p.ComprobanteDetalles)
                .HasForeignKey(d => d.IComprobante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprobanteDetalle_ToComprobante");

            entity.HasOne(d => d.IProductoNavigation).WithMany(p => p.ComprobanteDetalles)
                .HasForeignKey(d => d.IProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprobanteDetalle_ToProducto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IId).HasName("PK__Producto__252C106A1FA22924");

            entity.ToTable("Producto", "sch_facturacion");

            entity.Property(e => e.IId)
                .HasDefaultValueSql("(NEXT VALUE FOR [seq_productos])")
                .HasColumnName("iId");
            entity.Property(e => e.BEstado)
                .HasDefaultValue(true)
                .HasColumnName("bEstado");
            entity.Property(e => e.DFechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dFechaCreacion");
            entity.Property(e => e.DFechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("dFechaModificacion");
            entity.Property(e => e.DcPrecioUnitario)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("dcPrecioUnitario");
            entity.Property(e => e.ICategoriaCat).HasColumnName("iCategoriaCat");
            entity.Property(e => e.ILaboratorioCat).HasColumnName("iLaboratorioCat");
            entity.Property(e => e.IMarcaCat).HasColumnName("iMarcaCat");
            entity.Property(e => e.IStock).HasColumnName("iStock");
            entity.Property(e => e.IUsuarioCreacion)
                .HasDefaultValue(1)
                .HasColumnName("iUsuarioCreacion");
            entity.Property(e => e.IUsuarioModificacion).HasColumnName("iUsuarioModificacion");
            entity.Property(e => e.VDescripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("vDescripcion");
            entity.Property(e => e.VNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("vNombre");

            entity.HasOne(d => d.ICategoriaCatNavigation).WithMany(p => p.ProductoICategoriaCatNavigations)
                .HasForeignKey(d => d.ICategoriaCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_ToCategoria");

            entity.HasOne(d => d.ILaboratorioCatNavigation).WithMany(p => p.ProductoILaboratorioCatNavigations)
                .HasForeignKey(d => d.ILaboratorioCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_ToLaboratorio");

            entity.HasOne(d => d.IMarcaCatNavigation).WithMany(p => p.ProductoIMarcaCatNavigations)
                .HasForeignKey(d => d.IMarcaCat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_ToMarca");
        });
        modelBuilder.HasSequence("seq_catalogo");
        modelBuilder.HasSequence("seq_catalogo_detalle");
        modelBuilder.HasSequence("seq_clientes");
        modelBuilder.HasSequence("seq_comprobantes");
        modelBuilder.HasSequence("seq_comprobantes_detalle");
        modelBuilder.HasSequence("seq_productos");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
