/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     4/11/2022 21:50:10                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DEPARTAMENTOS') and o.name = 'FK_DEPARTAM_REFERENCE_TORRES')
alter table DEPARTAMENTOS
   drop constraint FK_DEPARTAM_REFERENCE_TORRES
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('DETALLE_CONTABILIDAD') and o.name = 'FK_DETALLE__REFERENCE_ENCABEZA')
alter table DETALLE_CONTABILIDAD
   drop constraint FK_DETALLE__REFERENCE_ENCABEZA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('FACTURA_COMPRAS') and o.name = 'FK_FACTURA__REFERENCE_PROVEEDO')
alter table FACTURA_COMPRAS
   drop constraint FK_FACTURA__REFERENCE_PROVEEDO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('RECIBO_DETALLE') and o.name = 'FK_RECIBO_D_REFERENCE_RECIBO_C')
alter table RECIBO_DETALLE
   drop constraint FK_RECIBO_D_REFERENCE_RECIBO_C
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('TORRES') and o.name = 'FK_TORRES_REFERENCE_CONJUNTO')
alter table TORRES
   drop constraint FK_TORRES_REFERENCE_CONJUNTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ADEUDOS')
            and   type = 'U')
   drop table ADEUDOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('BANCOS')
            and   type = 'U')
   drop table BANCOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CONJUNTO')
            and   type = 'U')
   drop table CONJUNTO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COPROPIETARIO')
            and   type = 'U')
   drop table COPROPIETARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DEPARTAMENTOS')
            and   type = 'U')
   drop table DEPARTAMENTOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('DETALLE_CONTABILIDAD')
            and   type = 'U')
   drop table DETALLE_CONTABILIDAD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ENCABEZADO_CONTABILIDAD')
            and   type = 'U')
   drop table ENCABEZADO_CONTABILIDAD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('FACTURA_COMPRAS')
            and   type = 'U')
   drop table FACTURA_COMPRAS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PROVEEDORES')
            and   type = 'U')
   drop table PROVEEDORES
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RECIBO_CABECERA')
            and   type = 'U')
   drop table RECIBO_CABECERA
go

if exists (select 1
            from  sysobjects
           where  id = object_id('RECIBO_DETALLE')
            and   type = 'U')
   drop table RECIBO_DETALLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('TORRES')
            and   type = 'U')
   drop table TORRES
go

/*==============================================================*/
/* Table: ADEUDOS                                               */
/*==============================================================*/
create table ADEUDOS (
   ID_ADEUDOS           uniqueidentifier     not null default newid(),
   FECHA_ADEUDOS        date                 not null,
   CODIGO_DEPTO_ADEUDOS varchar(8)           not null,
   MONTO_ADEUDOS        money                null,
   STATUS_ADEUDOS       bit                  null,
   NOMBRE_ADEUDOS       varchar(20)          null,
   CODIGO_ADEUDOS       int                  null,
   constraint PK_ADEUDOS primary key (ID_ADEUDOS)
)
go

/*==============================================================*/
/* Table: BANCOS                                                */
/*==============================================================*/
create table BANCOS (
   ID_BANCOS            uniqueidentifier     not null default newid(),
   NOMBRE_BANCOS        varchar(50)          not null,
   CTACTE_BANCOS        varchar(20)          not null,
   FECHA_AP_BANCOS      date                 not null,
   CHEQUE_INICIO_BANCOS int                  not null,
   STATUS_BANCOS        bit                  null,
   constraint PK_BANCOS primary key (ID_BANCOS)
)
go

/*==============================================================*/
/* Table: CONJUNTO                                              */
/*==============================================================*/
create table CONJUNTO (
   ID_CONJUNTO          uniqueidentifier     not null default newid(),
   NOMBRE_CONJUNTO      varchar(60)          not null,
   RUC_CONJUNTO         varchar(13)          not null,
   DIRECCION_CONJUNTO   text                 not null,
   TELEFONO_CONJUNTO    varchar(40)          not null,
   MAIL_CONJUNTO        varchar(50)          not null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_CONJUNTO primary key (ID_CONJUNTO)
)
go

/*==============================================================*/
/* Table: COPROPIETARIO                                         */
/*==============================================================*/
create table COPROPIETARIO (
   ID_COPROPIETARIO     uniqueidentifier     not null default newid(),
   ID_ARRENDATARIO      uniqueidentifier     null,
   NOMBRE_CONDOMINO     varchar(80)          not null,
   RUC_CONDOMINO        varchar(13)          not null,
   TELEFONO_CONDOMINO   varchar(50)          null,
   EMAIL_CONDOMINO      varchar(80)          null,
   CODIGO_DEPTO_CONDOMINO varchar(8)           null,
   OBSERVACION_CONDOMINO TEXT                 null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_COPROPIETARIO primary key (ID_COPROPIETARIO)
)
go

/*==============================================================*/
/* Table: DEPARTAMENTOS                                         */
/*==============================================================*/
create table DEPARTAMENTOS (
   ID_DEPTO             uniqueidentifier     not null default newid(),
   ID_TORRES            uniqueidentifier     not null default newid(),
   ALIQ_DEPTO           money                not null,
   COIGO_DEPTO          varchar(10)          not null,
   METROS_DEPTO         numeric(6,2)         not null,
   SALDO_INICIAL_ANUAL  money                null,
   FECHA_CREACION       datetime             not null,
   USUARIO_CREACION     varchar(60)          not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_MODIFICACION varchar(60)          null,
   constraint PK_DEPARTAMENTOS primary key (ID_DEPTO)
)
go

/*==============================================================*/
/* Table: DETALLE_CONTABILIDAD                                  */
/*==============================================================*/
create table DETALLE_CONTABILIDAD (
   ID_DET_CONT          uniqueidentifier     not null default newid(),
   ID_ENC_CONT          uniqueidentifier     null default newid(),
   N_COMP_DET_CONT      int                  not null,
   TIPO_DOC_N_DET_CONT  int                  not null,
   FECHA_DET_CONT       date                 not null,
   CTACONT_DET_CONT     varchar(10)          not null,
   NRO_INT_DET_CONT     int                  null,
   NNRO_EXTERNO_DET_CONT varchar(20)          null,
   DETALLE_DET_CONT     varchar(60)          not null,
   DEBITO_DET_CONT      money                null,
   CREDITO_DET_CONT     money                null,
   CHEQUE_DET_CONT      int                  null,
   FECHA_VENCI_DET_CONT date                 null,
   TIPO_COD_DET_CONT    int                  null,
   CODIGO_PROV_DET_CONT int                  null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_DETALLE_CONTABILIDAD primary key (ID_DET_CONT)
)
go

/*==============================================================*/
/* Table: ENCABEZADO_CONTABILIDAD                               */
/*==============================================================*/
create table ENCABEZADO_CONTABILIDAD (
   ID_ENC_CONT          uniqueidentifier     not null default newid(),
   TIPO_DOC_N_ENC_CONT  int                  not null,
   N_COMP_ENC_CONT      int                  not null,
   FECHA_ENC_CONT       datetime             not null,
   CHEQUE_ENC_CONT      int                  null,
   CONCEPTO_ENC_CONT    varchar(60)          not null,
   NRO_RET_ENC_CONT     int                  null,
   ANULADO_ENC_CONT     bit                  null,
   CTACONT_ENC_CONT     varchar(10)          null,
   FEC_ANULA_ENC_CONT   datetime             null,
   FEC_VENCI_ENC_CONT   datetime             null,
   USUARIO_CREACION_ENC_CONT varchar(50)          not null,
   constraint PK_ENCABEZADO_CONTABILIDAD primary key (ID_ENC_CONT)
)
go

/*==============================================================*/
/* Table: FACTURA_COMPRAS                                       */
/*==============================================================*/
create table FACTURA_COMPRAS (
   ID_COMPRAS           uniqueidentifier     not null default newid(),
   ID_PROVEE            uniqueidentifier     null default newid(),
   SERIE_COMPRAS        varchar(7)           null,
   N_EXTERNO_COMPRAS    int                  null,
   TIPO_FAC_COMPRAS     int                  null,
   FECHA_COMPRA         date                 not null,
   FECHA_VENCI_COMPRA   date                 null,
   CODIGO_PROVEED_COMPRA int                  not null,
   DETALLE_COMPRA       varchar(40)          not null,
   ANULADO_COMPRA       bit                  null,
   SUBTOTAL_COMPRA      money                not null,
   SUBTOT_IVA_COMPRA    money                null,
   NO_GRAVADO_COMPRAS   money                null,
   IVA_COMPRAS          money                null,
   PORC_IVA_COMPRAS     money                null,
   SALDO_ANT_COMPRAS    money                null,
   CONCEP_RF_COMPRAS    varchar(4)           null,
   CONCEP_RF2_COMPRAS   varchar(4)           null,
   PORC_RF_COMPRAS      money                null,
   PORC_RF2_COMPRAS     money                null,
   VALRET_RF_COMPRAS    money                null,
   VALRET_RF2_COMPRAS   money                null,
   CONCEP_IVA_COMPRAS   varchar(4)           null,
   CONCEP_IVA2_COMPRAS  varchar(4)           null,
   PORCR_IVA_COMPRAS    money                null,
   PORC_IVA2_COMPRAS    money                null,
   VALRET_IVA_COMPRAS   money                null,
   VALRET_IVA2_COMPRAS  money                null,
   NRO_RETEN_COMPRAS    int                  null,
   AUT_SRI_COMPRAS      varchar(49)          null,
   SUSTENTO_COMPRAS     int                  null,
   FECHA_CAD_COMPRAS    date                 null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_FACTURA_COMPRAS primary key (ID_COMPRAS)
)
go

/*==============================================================*/
/* Table: PROVEEDORES                                           */
/*==============================================================*/
create table PROVEEDORES (
   ID_PROVEE            uniqueidentifier     not null default newid(),
   NOMBRE_PROVEE        varchar(70)          not null,
   RUC_PROVEE           varchar(13)          not null,
   CONTACTO_PROVEE      varchar(70)          null,
   CIUDAD_PROVEE        varchar(30)          not null,
   DIREC_PROVEE         varchar(100)         not null,
   TELEFONOS_PROVEE     varchar(50)          null,
   E_MAIL_PROVEE        varchar(100)         null,
   SALDO_ANT_PROVEE     money                null,
   SALDO_PEND_PROVEE    money                null,
   STATUS_PROVEE        bit                  null,
   constraint PK_PROVEEDORES primary key (ID_PROVEE)
)
go

/*==============================================================*/
/* Table: RECIBO_CABECERA                                       */
/*==============================================================*/
create table RECIBO_CABECERA (
   ID_RECIBO_CAB        uniqueidentifier     not null default newid(),
   NRO_RECIBO_RECIBO_CAB int                  not null,
   FECHA_RECIBO_CAB     date                 not null,
   COD_CLIENTE_RECIBO_CAB int                  not null,
   CONCEPTO_RECIBO_CAB  varchar(60)          not null,
   STATUS_RECIBO_CAB    bit                  null,
   FECHA_CREACION_RECIBO_CAB datetime             null,
   FECHA_MODIFICACION_RECIBO_CAB datetime             null,
   USUARIO_CREACION_RECIBO_CAB varchar(60)          null,
   USUARIO_MODIFICACION_RECIBO_CAB varchar(60)          null default newid(),
   constraint PK_RECIBO_CABECERA primary key (ID_RECIBO_CAB, NRO_RECIBO_RECIBO_CAB)
)
go

/*==============================================================*/
/* Table: RECIBO_DETALLE                                        */
/*==============================================================*/
create table RECIBO_DETALLE (
   ID_RECIBO_DET        uniqueidentifier     not null default newid(),
   ID_RECIBO_CAB        uniqueidentifier     not null default newid(),
   NRO_RECIBO_RECIBO_CAB int                  null,
   FECHA_RECIBO_DET     date                 null,
   TIPO_DOC_N_RECIBO_DET int                  null,
   TIPO_PAGO_RECIBO_DET int                  null,
   VALOR_RECIBO_DET     money                null,
   COD_CLI_RECIBO_DET   int                  null,
   COD_DEPTO_RECIBO_DET varchar(8)           null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_RECIBO_DETALLE primary key (ID_RECIBO_DET)
)
go

/*==============================================================*/
/* Table: TORRES                                                */
/*==============================================================*/
create table TORRES (
   ID_TORRES            uniqueidentifier     not null default newid(),
   ID_CONJUNTO          uniqueidentifier     not null default newid(),
   NOMBRE_TORRES        varchar(60)          not null,
   FECHA_CREACION       datetime             not null,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     varchar(70)          not null,
   USUARIO_MODIFICACION varchar(70)          null,
   constraint PK_TORRES primary key (ID_TORRES)
)
go

alter table DEPARTAMENTOS
   add constraint FK_DEPARTAM_REFERENCE_TORRES foreign key (ID_TORRES)
      references TORRES (ID_TORRES)
go

alter table DETALLE_CONTABILIDAD
   add constraint FK_DETALLE__REFERENCE_ENCABEZA foreign key (ID_ENC_CONT)
      references ENCABEZADO_CONTABILIDAD (ID_ENC_CONT)
go

alter table FACTURA_COMPRAS
   add constraint FK_FACTURA__REFERENCE_PROVEEDO foreign key (ID_PROVEE)
      references PROVEEDORES (ID_PROVEE)
go

alter table RECIBO_DETALLE
   add constraint FK_RECIBO_D_REFERENCE_RECIBO_C foreign key (ID_RECIBO_CAB, NRO_RECIBO_RECIBO_CAB)
      references RECIBO_CABECERA (ID_RECIBO_CAB, NRO_RECIBO_RECIBO_CAB)
go

alter table TORRES
   add constraint FK_TORRES_REFERENCE_CONJUNTO foreign key (ID_CONJUNTO)
      references CONJUNTO (ID_CONJUNTO)
go

