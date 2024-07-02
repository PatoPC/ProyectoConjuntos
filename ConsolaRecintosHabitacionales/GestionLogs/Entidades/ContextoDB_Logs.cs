using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionLogs.Entidades;

public partial class ContextoDB_Logs : DbContext
{
    public ContextoDB_Logs()
    {
    }

    public ContextoDB_Logs(DbContextOptions<ContextoDB_Logs> options)
        : base(options)
    {
    }

    public virtual DbSet<LogsExcepcione> LogsExcepciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-6EB8AME\\SQLEXPRESS;database=LogsConjuntos;persist security info=True; Encrypt=False;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogsExcepcione>(entity =>
        {
            entity.HasKey(e => e.IdLogsExcepciones);

            entity.ToTable("LOGS_EXCEPCIONES");

            entity.Property(e => e.IdLogsExcepciones)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID_LOGS_EXCEPCIONES");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Entidad)
                .HasMaxLength(60)
                .HasColumnName("ENTIDAD");
            entity.Property(e => e.Error)
                .HasColumnType("text")
                .HasColumnName("ERROR");
            entity.Property(e => e.FechaError)
                .HasColumnType("datetime")
                .HasColumnName("FECHA_ERROR");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.Metodo)
                .HasMaxLength(60)
                .HasColumnName("METODO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
