﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntidadesCatalogos.Entidades;

public partial class ContextoDB_Catalogos : DbContext
{
    public ContextoDB_Catalogos()
    {
    }

    public ContextoDB_Catalogos(DbContextOptions<ContextoDB_Catalogos> options)
        : base(options)
    {
    }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=181.39.23.39;database=Catalogo_Conjunto;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogo>(entity =>
        {
            entity.HasKey(e => e.IdCatalogo);

            entity.ToTable("CATALOGO");

            entity.Property(e => e.IdCatalogo)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_CATALOGO");
            entity.Property(e => e.CodigoCatalogo)
                .HasMaxLength(8)
                .HasColumnName("CODIGO_CATALOGO");
            entity.Property(e => e.DatoAdicional)
                .HasMaxLength(80)
                .HasColumnName("DATO_ADICIONAL");
            entity.Property(e => e.DatoIcono)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("DATO_ICONO");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Editable).HasColumnName("EDITABLE");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MODIFICACION");
            entity.Property(e => e.IdCatalogopadre).HasColumnName("ID_CATALOGOPADRE");
            entity.Property(e => e.IdConjunto).HasColumnName("ID_CONJUNTO");
            entity.Property(e => e.NivelCatalogo).HasColumnName("NIVEL_CATALOGO");
            entity.Property(e => e.NombreCatalogo)
                .HasMaxLength(150)
                .HasColumnName("NOMBRE_CATALOGO");
            entity.Property(e => e.TieneVigencia).HasColumnName("TIENE_VIGENCIA");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_CREACION");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_MODIFICACION");

            entity.HasOne(d => d.IdCatalogopadreNavigation).WithMany(p => p.InverseIdCatalogopadreNavigation)
                .HasForeignKey(d => d.IdCatalogopadre)
                .HasConstraintName("FK_CATALOGO_FK_CATALO_CATALOGO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
