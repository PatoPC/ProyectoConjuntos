using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarioDB.Entidades;

public partial class ContextoDB_Permisos : DbContext
{
    public ContextoDB_Permisos()
    {
    }

    public ContextoDB_Permisos(DbContextOptions<ContextoDB_Permisos> options)
        : base(options)
    {
    }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioConjunto> UsuarioConjuntos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

    //=> optionsBuilder.UseSqlServer("server=PC_TI-Quito\\SQLEXPRESS;database=Condominios_Permisos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");
    => optionsBuilder.UseSqlServer("server=PATOPC\\SQLEXPRESS;database=Condominios_Permisos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");
    //=> optionsBuilder.UseSqlServer("server=PCPATOTI\\SQLEXPRESS;database=Condominios_Permisos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu);

            entity.ToTable("MENU");

            entity.Property(e => e.IdMenu)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_MENU");
            entity.Property(e => e.DatoIcono)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("DATO_ICONO");
            entity.Property(e => e.IdModulo)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_MODULO");
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_MENU");
            entity.Property(e => e.RutaMenu)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("RUTA_MENU");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdModulo)
                .HasConstraintName("FK_MENU_REFERENCE_MODULO");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo);

            entity.ToTable("MODULO");

            entity.Property(e => e.IdModulo)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_MODULO");
            entity.Property(e => e.IconoModulo)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ICONO_MODULO");
            entity.Property(e => e.IdRol)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Modulos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_MODULO_REFERENCE_ROL");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermisos);

            entity.ToTable("PERMISOS");

            entity.Property(e => e.IdPermisos)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_PERMISOS");
            entity.Property(e => e.Concedido).HasColumnName("CONCEDIDO");
            entity.Property(e => e.CssPermiso)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CSS_PERMISO");
            entity.Property(e => e.IdMenu)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_MENU");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE_PERMISO");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_PERMISOS_REFERENCE_MENU");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.AccesoTodos).HasColumnName("ACCESO_TODOS");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MODIFICACION");
            entity.Property(e => e.IdPaginaInicioRol).HasColumnName("ID_PAGINA_INICIO_ROL");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(30)
                .HasColumnName("NOMBRE_ROL");
            entity.Property(e => e.RolRestringido).HasColumnName("ROL_RESTRINGIDO");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_CREACION");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_MODIFICACION");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.ToTable("USUARIO");

            entity.Property(e => e.IdUsuario)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_USUARIO");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .HasColumnName("CONTRASENA");
            entity.Property(e => e.ContrasenaInicial).HasColumnName("CONTRASENA_INICIAL");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("CORREO_ELECTRONICO");
            entity.Property(e => e.Estado).HasColumnName("ESTADO");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("FECHA_CREACION");
            entity.Property(e => e.FechaModificacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_MODIFICACION");
            entity.Property(e => e.FechaUltimoIngreso)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ULTIMO_INGRESO");
            entity.Property(e => e.IdConjuntoDefault).HasColumnName("ID_CONJUNTO_DEFAULT");
            entity.Property(e => e.IdPersona)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_PERSONA");
            entity.Property(e => e.IdRol)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_ROL");
            entity.Property(e => e.IndicioContrasena)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("INDICIO_CONTRASENA");
            entity.Property(e => e.UsuarioCreacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_CREACION");
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(100)
                .HasColumnName("USUARIO_MODIFICACION");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_REFERENCE_ROL");
        });

        modelBuilder.Entity<UsuarioConjunto>(entity =>
        {
            entity.HasKey(e => e.IdUsuairoConjunto);

            entity.ToTable("USUARIO_CONJUNTO");

            entity.Property(e => e.IdUsuairoConjunto)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_USUAIRO_CONJUNTO");
            entity.Property(e => e.IdConjunto).HasColumnName("ID_CONJUNTO");
            entity.Property(e => e.IdUsuario)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_USUARIO");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioConjuntos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO__REFERENCE_USUARIO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
