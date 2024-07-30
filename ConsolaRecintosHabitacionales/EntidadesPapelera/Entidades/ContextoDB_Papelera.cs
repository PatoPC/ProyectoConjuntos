using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntidadesPapelera.Entidades;

public partial class ContextoDB_Papelera : DbContext
{
    public ContextoDB_Papelera()
    {
    }

    public ContextoDB_Papelera(DbContextOptions<ContextoDB_Papelera> options)
        : base(options)
    {
    }

    public virtual DbSet<DatosEliminado> DatosEliminados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         //=> optionsBuilder.UseSqlServer("server=DESKTOP-6EB8AME\\SQLEXPRESS;database=Papelera_Conjuntos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");
         => optionsBuilder.UseSqlServer("server=DESKTOP-26QEGBC\\SQLEXPRESS;database=LogsConjuntos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");
    //=> optionsBuilder.UseSqlServer("server=PCPATOTI\\SQLEXPRESS;database=Papelera_Conjuntos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatosEliminado>(entity =>
        {
            entity.HasKey(e => e.IdDatosEliminados);

            entity.ToTable("DATOS_ELIMINADOS");

            entity.Property(e => e.IdDatosEliminados)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_DATOS_ELIMINADOS");
            entity.Property(e => e.DatosEliminados)
                .HasColumnType("text")
                .HasColumnName("DATOS_ELIMINADOS");
            entity.Property(e => e.FechaEliminacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ELIMINACION");
            entity.Property(e => e.IdObjetoEliminado).HasColumnName("ID_OBJETO_ELIMINADO");
            entity.Property(e => e.IdPersonaEliminar).HasColumnName("ID_PERSONA_ELIMINAR");
            entity.Property(e => e.IdUsuarioElimina).HasColumnName("ID_USUARIO_ELIMINA");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("NUMERO_IDENTIFICACION");
            entity.Property(e => e.RutaArchivo)
                .HasColumnType("text")
                .HasColumnName("RUTA_ARCHIVO");
            entity.Property(e => e.TipoDatoEliminado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TIPO_DATO_ELIMINADO");
            entity.Property(e => e.UsuarioElimina)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("USUARIO_ELIMINA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
