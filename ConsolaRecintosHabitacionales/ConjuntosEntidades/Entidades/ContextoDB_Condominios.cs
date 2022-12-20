﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConjuntosEntidades.Entidades
{
    public partial class ContextoDB_Condominios : DbContext
    {
        public ContextoDB_Condominios()
        {
        }

        public ContextoDB_Condominios(DbContextOptions<ContextoDB_Condominios> options)
            : base(options)
        {
        }

        public virtual DbSet<Adeudo> Adeudos { get; set; } = null!;
        public virtual DbSet<Banco> Bancos { get; set; } = null!;
        public virtual DbSet<Conjunto> Conjuntos { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<DetalleContabilidad> DetalleContabilidads { get; set; } = null!;
        public virtual DbSet<EncabezadoContabilidad> EncabezadoContabilidads { get; set; } = null!;
        public virtual DbSet<FacturaCompra> FacturaCompras { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<ReciboCabecera> ReciboCabeceras { get; set; } = null!;
        public virtual DbSet<ReciboDetalle> ReciboDetalles { get; set; } = null!;
        public virtual DbSet<TipoPersona> TipoPersonas { get; set; } = null!;
        public virtual DbSet<Torre> Torres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=181.39.23.39;database=Condominios;persist security info=True;user id=AdminSQLUser;password=1915.*@Ort.;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adeudo>(entity =>
            {
                entity.HasKey(e => e.IdAdeudos);

                entity.ToTable("ADEUDOS");

                entity.Property(e => e.IdAdeudos)
                    .HasColumnName("ID_ADEUDOS")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodigoAdeudos).HasColumnName("CODIGO_ADEUDOS");

                entity.Property(e => e.CodigoDeptoAdeudos)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DEPTO_ADEUDOS");

                entity.Property(e => e.FechaAdeudos)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_ADEUDOS");

                entity.Property(e => e.MontoAdeudos)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_ADEUDOS");

                entity.Property(e => e.NombreAdeudos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_ADEUDOS");

                entity.Property(e => e.StatusAdeudos).HasColumnName("STATUS_ADEUDOS");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBancos);

                entity.ToTable("BANCOS");

                entity.Property(e => e.IdBancos)
                    .HasColumnName("ID_BANCOS")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChequeInicioBancos).HasColumnName("CHEQUE_INICIO_BANCOS");

                entity.Property(e => e.CtacteBancos)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CTACTE_BANCOS");

                entity.Property(e => e.FechaApBancos)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_AP_BANCOS");

                entity.Property(e => e.NombreBancos)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_BANCOS");

                entity.Property(e => e.StatusBancos).HasColumnName("STATUS_BANCOS");
            });

            modelBuilder.Entity<Conjunto>(entity =>
            {
                entity.HasKey(e => e.IdConjunto);

                entity.ToTable("CONJUNTO");

                entity.Property(e => e.IdConjunto)
                    .HasColumnName("ID_CONJUNTO")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DireccionConjunto)
                    .HasColumnType("text")
                    .HasColumnName("DIRECCION_CONJUNTO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.MailConjunto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MAIL_CONJUNTO");

                entity.Property(e => e.NombreConjunto)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CONJUNTO");

                entity.Property(e => e.RucConjunto)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("RUC_CONJUNTO");

                entity.Property(e => e.TelefonoConjunto)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_CONJUNTO");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento);

                entity.ToTable("DEPARTAMENTOS");

                entity.Property(e => e.IdDepartamento)
                    .HasColumnName("ID_DEPARTAMENTO")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AliqDepartamento)
                    .HasColumnType("money")
                    .HasColumnName("ALIQ_DEPARTAMENTO");

                entity.Property(e => e.CodigoDepartamento)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_DEPARTAMENTO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.IdTorres)
                    .HasColumnName("ID_TORRES")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.MetrosDepartamento)
                    .HasColumnType("numeric(6, 2)")
                    .HasColumnName("METROS_DEPARTAMENTO");

                entity.Property(e => e.SaldoInicialAnual)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_INICIAL_ANUAL");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.HasOne(d => d.IdTorresNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.IdTorres)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEPARTAM_REFERENCE_TORRES");
            });

            modelBuilder.Entity<DetalleContabilidad>(entity =>
            {
                entity.HasKey(e => e.IdDetCont);

                entity.ToTable("DETALLE_CONTABILIDAD");

                entity.Property(e => e.IdDetCont)
                    .HasColumnName("ID_DET_CONT")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ChequeDetCont).HasColumnName("CHEQUE_DET_CONT");

                entity.Property(e => e.CodigoProvDetCont).HasColumnName("CODIGO_PROV_DET_CONT");

                entity.Property(e => e.CreditoDetCont)
                    .HasColumnType("money")
                    .HasColumnName("CREDITO_DET_CONT");

                entity.Property(e => e.CtacontDetCont)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CTACONT_DET_CONT");

                entity.Property(e => e.DebitoDetCont)
                    .HasColumnType("money")
                    .HasColumnName("DEBITO_DET_CONT");

                entity.Property(e => e.DetalleDetCont)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DETALLE_DET_CONT");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaDetCont)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_DET_CONT");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FechaVenciDetCont)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_VENCI_DET_CONT");

                entity.Property(e => e.IdEncCont)
                    .HasColumnName("ID_ENC_CONT")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NCompDetCont).HasColumnName("N_COMP_DET_CONT");

                entity.Property(e => e.NnroExternoDetCont)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NNRO_EXTERNO_DET_CONT");

                entity.Property(e => e.NroIntDetCont).HasColumnName("NRO_INT_DET_CONT");

                entity.Property(e => e.TipoCodDetCont).HasColumnName("TIPO_COD_DET_CONT");

                entity.Property(e => e.TipoDocNDetCont).HasColumnName("TIPO_DOC_N_DET_CONT");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.HasOne(d => d.IdEncContNavigation)
                    .WithMany(p => p.DetalleContabilidads)
                    .HasForeignKey(d => d.IdEncCont)
                    .HasConstraintName("FK_DETALLE__REFERENCE_ENCABEZA");
            });

            modelBuilder.Entity<EncabezadoContabilidad>(entity =>
            {
                entity.HasKey(e => e.IdEncCont);

                entity.ToTable("ENCABEZADO_CONTABILIDAD");

                entity.Property(e => e.IdEncCont)
                    .HasColumnName("ID_ENC_CONT")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AnuladoEncCont).HasColumnName("ANULADO_ENC_CONT");

                entity.Property(e => e.ChequeEncCont).HasColumnName("CHEQUE_ENC_CONT");

                entity.Property(e => e.ConceptoEncCont)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO_ENC_CONT");

                entity.Property(e => e.CtacontEncCont)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CTACONT_ENC_CONT");

                entity.Property(e => e.FecAnulaEncCont)
                    .HasColumnType("datetime")
                    .HasColumnName("FEC_ANULA_ENC_CONT");

                entity.Property(e => e.FecVenciEncCont)
                    .HasColumnType("datetime")
                    .HasColumnName("FEC_VENCI_ENC_CONT");

                entity.Property(e => e.FechaEncCont)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ENC_CONT");

                entity.Property(e => e.NCompEncCont).HasColumnName("N_COMP_ENC_CONT");

                entity.Property(e => e.NroRetEncCont).HasColumnName("NRO_RET_ENC_CONT");

                entity.Property(e => e.TipoDocNEncCont).HasColumnName("TIPO_DOC_N_ENC_CONT");

                entity.Property(e => e.UsuarioCreacionEncCont)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION_ENC_CONT");
            });

            modelBuilder.Entity<FacturaCompra>(entity =>
            {
                entity.HasKey(e => e.IdCompras);

                entity.ToTable("FACTURA_COMPRAS");

                entity.Property(e => e.IdCompras)
                    .HasColumnName("ID_COMPRAS")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AnuladoCompra).HasColumnName("ANULADO_COMPRA");

                entity.Property(e => e.AutSriCompras)
                    .HasMaxLength(49)
                    .IsUnicode(false)
                    .HasColumnName("AUT_SRI_COMPRAS");

                entity.Property(e => e.CodigoProveedCompra).HasColumnName("CODIGO_PROVEED_COMPRA");

                entity.Property(e => e.ConcepIva2Compras)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CONCEP_IVA2_COMPRAS");

                entity.Property(e => e.ConcepIvaCompras)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CONCEP_IVA_COMPRAS");

                entity.Property(e => e.ConcepRf2Compras)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CONCEP_RF2_COMPRAS");

                entity.Property(e => e.ConcepRfCompras)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CONCEP_RF_COMPRAS");

                entity.Property(e => e.DetalleCompra)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DETALLE_COMPRA");

                entity.Property(e => e.FechaCadCompras)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_CAD_COMPRAS");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_COMPRA");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FechaVenciCompra)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_VENCI_COMPRA");

                entity.Property(e => e.IdProvee)
                    .HasColumnName("ID_PROVEE")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IvaCompras)
                    .HasColumnType("money")
                    .HasColumnName("IVA_COMPRAS");

                entity.Property(e => e.NExternoCompras).HasColumnName("N_EXTERNO_COMPRAS");

                entity.Property(e => e.NoGravadoCompras)
                    .HasColumnType("money")
                    .HasColumnName("NO_GRAVADO_COMPRAS");

                entity.Property(e => e.NroRetenCompras).HasColumnName("NRO_RETEN_COMPRAS");

                entity.Property(e => e.PorcIva2Compras)
                    .HasColumnType("money")
                    .HasColumnName("PORC_IVA2_COMPRAS");

                entity.Property(e => e.PorcIvaCompras)
                    .HasColumnType("money")
                    .HasColumnName("PORC_IVA_COMPRAS");

                entity.Property(e => e.PorcRf2Compras)
                    .HasColumnType("money")
                    .HasColumnName("PORC_RF2_COMPRAS");

                entity.Property(e => e.PorcRfCompras)
                    .HasColumnType("money")
                    .HasColumnName("PORC_RF_COMPRAS");

                entity.Property(e => e.PorcrIvaCompras)
                    .HasColumnType("money")
                    .HasColumnName("PORCR_IVA_COMPRAS");

                entity.Property(e => e.SaldoAntCompras)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_ANT_COMPRAS");

                entity.Property(e => e.SerieCompras)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("SERIE_COMPRAS");

                entity.Property(e => e.SubtotIvaCompra)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOT_IVA_COMPRA");

                entity.Property(e => e.SubtotalCompra)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOTAL_COMPRA");

                entity.Property(e => e.SustentoCompras).HasColumnName("SUSTENTO_COMPRAS");

                entity.Property(e => e.TipoFacCompras).HasColumnName("TIPO_FAC_COMPRAS");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.Property(e => e.ValretIva2Compras)
                    .HasColumnType("money")
                    .HasColumnName("VALRET_IVA2_COMPRAS");

                entity.Property(e => e.ValretIvaCompras)
                    .HasColumnType("money")
                    .HasColumnName("VALRET_IVA_COMPRAS");

                entity.Property(e => e.ValretRf2Compras)
                    .HasColumnType("money")
                    .HasColumnName("VALRET_RF2_COMPRAS");

                entity.Property(e => e.ValretRfCompras)
                    .HasColumnType("money")
                    .HasColumnName("VALRET_RF_COMPRAS");

                entity.HasOne(d => d.IdProveeNavigation)
                    .WithMany(p => p.FacturaCompras)
                    .HasForeignKey(d => d.IdProvee)
                    .HasConstraintName("FK_FACTURA__REFERENCE_PROVEEDO");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("PERSONA");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("ID_PERSONA")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ApellidosPersona)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS_PERSONA");

                entity.Property(e => e.EmailPersona)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL_PERSONA");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.IdTipoIdentificacion).HasColumnName("ID_TIPO_IDENTIFICACION");

                entity.Property(e => e.IdentificacionPersona)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("IDENTIFICACION_PERSONA");

                entity.Property(e => e.NombresPersona)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRES_PERSONA");

                entity.Property(e => e.ObservacionPersona)
                    .HasColumnType("text")
                    .HasColumnName("OBSERVACION_PERSONA");

                entity.Property(e => e.TelefonoPersona)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_PERSONA");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProvee);

                entity.ToTable("PROVEEDORES");

                entity.Property(e => e.IdProvee)
                    .HasColumnName("ID_PROVEE")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CiudadProvee)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CIUDAD_PROVEE");

                entity.Property(e => e.ContactoProvee)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CONTACTO_PROVEE");

                entity.Property(e => e.DirecProvee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DIREC_PROVEE");

                entity.Property(e => e.EMailProvee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("E_MAIL_PROVEE");

                entity.Property(e => e.NombreProvee)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_PROVEE");

                entity.Property(e => e.RucProvee)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("RUC_PROVEE");

                entity.Property(e => e.SaldoAntProvee)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_ANT_PROVEE");

                entity.Property(e => e.SaldoPendProvee)
                    .HasColumnType("money")
                    .HasColumnName("SALDO_PEND_PROVEE");

                entity.Property(e => e.StatusProvee).HasColumnName("STATUS_PROVEE");

                entity.Property(e => e.TelefonosProvee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONOS_PROVEE");
            });

            modelBuilder.Entity<ReciboCabecera>(entity =>
            {
                entity.HasKey(e => new { e.IdReciboCab, e.NroReciboReciboCab });

                entity.ToTable("RECIBO_CABECERA");

                entity.Property(e => e.IdReciboCab)
                    .HasColumnName("ID_RECIBO_CAB")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NroReciboReciboCab).HasColumnName("NRO_RECIBO_RECIBO_CAB");

                entity.Property(e => e.CodClienteReciboCab).HasColumnName("COD_CLIENTE_RECIBO_CAB");

                entity.Property(e => e.ConceptoReciboCab)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO_RECIBO_CAB");

                entity.Property(e => e.FechaCreacionReciboCab)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION_RECIBO_CAB");

                entity.Property(e => e.FechaModificacionReciboCab)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION_RECIBO_CAB");

                entity.Property(e => e.FechaReciboCab)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_RECIBO_CAB");

                entity.Property(e => e.StatusReciboCab).HasColumnName("STATUS_RECIBO_CAB");

                entity.Property(e => e.UsuarioCreacionReciboCab)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION_RECIBO_CAB");

                entity.Property(e => e.UsuarioModificacionReciboCab)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION_RECIBO_CAB")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ReciboDetalle>(entity =>
            {
                entity.HasKey(e => e.IdReciboDet);

                entity.ToTable("RECIBO_DETALLE");

                entity.Property(e => e.IdReciboDet)
                    .HasColumnName("ID_RECIBO_DET")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodCliReciboDet).HasColumnName("COD_CLI_RECIBO_DET");

                entity.Property(e => e.CodDeptoReciboDet)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("COD_DEPTO_RECIBO_DET");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FechaReciboDet)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_RECIBO_DET");

                entity.Property(e => e.IdReciboCab)
                    .HasColumnName("ID_RECIBO_CAB")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NroReciboReciboCab).HasColumnName("NRO_RECIBO_RECIBO_CAB");

                entity.Property(e => e.TipoDocNReciboDet).HasColumnName("TIPO_DOC_N_RECIBO_DET");

                entity.Property(e => e.TipoPagoReciboDet).HasColumnName("TIPO_PAGO_RECIBO_DET");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.Property(e => e.ValorReciboDet)
                    .HasColumnType("money")
                    .HasColumnName("VALOR_RECIBO_DET");

                entity.HasOne(d => d.ReciboCabecera)
                    .WithMany(p => p.ReciboDetalles)
                    .HasForeignKey(d => new { d.IdReciboCab, d.NroReciboReciboCab })
                    .HasConstraintName("FK_RECIBO_D_REFERENCE_RECIBO_C");
            });

            modelBuilder.Entity<TipoPersona>(entity =>
            {
                entity.HasKey(e => e.IdTipoPersona);

                entity.ToTable("TIPO_PERSONA");

                entity.Property(e => e.IdTipoPersona)
                    .HasColumnName("ID_TIPO_PERSONA")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.IdDepartamento)
                    .HasColumnName("ID_DEPARTAMENTO")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdPersona)
                    .HasColumnName("ID_PERSONA")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IdTipoPersonaDepartamento).HasColumnName("ID_TIPO_PERSONA_DEPARTAMENTO");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.TipoPersonas)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_PER_REFERENCE_DEPARTAM");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.TipoPersonas)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_PER_REFERENCE_PERSONA");
            });

            modelBuilder.Entity<Torre>(entity =>
            {
                entity.HasKey(e => e.IdTorres);

                entity.ToTable("TORRES");

                entity.Property(e => e.IdTorres)
                    .HasColumnName("ID_TORRES")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.IdConjunto)
                    .HasColumnName("ID_CONJUNTO")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.NombreTorres)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_TORRES");

                entity.Property(e => e.UsuarioCreacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_CREACION");

                entity.Property(e => e.UsuarioModificacion)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICACION");

                entity.HasOne(d => d.IdConjuntoNavigation)
                    .WithMany(p => p.Torres)
                    .HasForeignKey(d => d.IdConjunto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TORRES_REFERENCE_CONJUNTO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
