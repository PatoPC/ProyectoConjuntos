/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2016                    */
/* Created on:     17/12/2022 16:21:08                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MENU') and o.name = 'FK_MENU_REFERENCE_MODULO')
alter table MENU
   drop constraint FK_MENU_REFERENCE_MODULO
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('MODULO') and o.name = 'FK_MODULO_REFERENCE_ROL')
alter table MODULO
   drop constraint FK_MODULO_REFERENCE_ROL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('PERMISOS') and o.name = 'FK_PERMISOS_REFERENCE_MENU')
alter table PERMISOS
   drop constraint FK_PERMISOS_REFERENCE_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO') and o.name = 'FK_USUARIO_REFERENCE_ROL')
alter table USUARIO
   drop constraint FK_USUARIO_REFERENCE_ROL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('USUARIO_CONJUNTO') and o.name = 'FK_USUARIO__REFERENCE_USUARIO')
alter table USUARIO_CONJUNTO
   drop constraint FK_USUARIO__REFERENCE_USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MENU')
            and   type = 'U')
   drop table MENU
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MODULO')
            and   type = 'U')
   drop table MODULO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PERMISOS')
            and   type = 'U')
   drop table PERMISOS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ROL')
            and   type = 'U')
   drop table ROL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO')
            and   type = 'U')
   drop table USUARIO
go

if exists (select 1
            from  sysobjects
           where  id = object_id('USUARIO_CONJUNTO')
            and   type = 'U')
   drop table USUARIO_CONJUNTO
go

/*==============================================================*/
/* Table: MENU                                                  */
/*==============================================================*/
create table MENU (
   ID_MENU              uniqueidentifier     not null default newid(),
   ID_MODULO            uniqueidentifier     null default newid(),
   NOMBRE_MENU          varchar(30)          not null,
   RUTA_MENU            varchar(70)          not null,
   DATO_ICONO           varchar(45)          null,
   constraint PK_MENU primary key (ID_MENU)
)
go

/*==============================================================*/
/* Table: MODULO                                                */
/*==============================================================*/
create table MODULO (
   ID_MODULO            uniqueidentifier     not null default newid(),
   ID_ROL               uniqueidentifier     null default newid(),
   NOMBRE               varchar(50)          not null,
   ICONO_MODULO         varchar(45)          null,
   constraint PK_MODULO primary key (ID_MODULO)
)
go

/*==============================================================*/
/* Table: PERMISOS                                              */
/*==============================================================*/
create table PERMISOS (
   ID_PERMISOS          uniqueidentifier     not null default newid(),
   ID_MENU              uniqueidentifier     null default newid(),
   NOMBRE_PERMISO       varchar(50)          not null,
   CONCEDIDO            bit                  not null,
   CSS_PERMISO          varchar(30)          null,
   constraint PK_PERMISOS primary key (ID_PERMISOS)
)
go

/*==============================================================*/
/* Table: ROL                                                   */
/*==============================================================*/
create table ROL (
   ID_ROL               uniqueidentifier     not null default newid(),
   NOMBRE_ROL           nvarchar(30)         not null,
   ESTADO               bit                  not null,
   ID_PAGINA_INICIO_ROL uniqueidentifier     null,
   FECHA_CREACION       datetime             not null default current_timestamp,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     nvarchar(100)        not null,
   USUARIO_MODIFICACION nvarchar(100)        not null,
   ACCESO_TODOS         bit                  not null,
   ROL_RESTRINGIDO      bit                  not null,
   constraint PK_ROL primary key (ID_ROL)
)
go

/*==============================================================*/
/* Table: USUARIO                                               */
/*==============================================================*/
create table USUARIO (
   ID_USUARIO           uniqueidentifier     not null default newid(),
   ID_ROL               uniqueidentifier     not null default newid(),
   ID_PERSONA           uniqueidentifier     not null default newid(),
   ID_CONJUNTO_DEFAULT  uniqueidentifier     not null,
   ESTADO               bit                  null,
   CORREO_ELECTRONICO   nchar(60)            null,
   CONTRASENA_INICIAL   bit                  not null,
   CONTRASENA           nvarchar(200)        not null,
   INDICIO_CONTRASENA   nchar(50)            null,
   FECHA_ULTIMO_INGRESO datetime             null,
   FECHA_CREACION       datetime             not null default current_timestamp,
   FECHA_MODIFICACION   datetime             null,
   USUARIO_CREACION     nvarchar(100)        not null,
   USUARIO_MODIFICACION nvarchar(100)        not null,
   constraint PK_USUARIO primary key (ID_USUARIO)
)
go

/*==============================================================*/
/* Table: USUARIO_CONJUNTO                                      */
/*==============================================================*/
create table USUARIO_CONJUNTO (
   ID_USUAIRO_CONJUNTO  uniqueidentifier     not null default newid(),
   ID_USUARIO           uniqueidentifier     not null default newid(),
   ID_CONJUNTO          uniqueidentifier     not null,
   constraint PK_USUARIO_CONJUNTO primary key (ID_USUAIRO_CONJUNTO)
)
go

alter table MENU
   add constraint FK_MENU_REFERENCE_MODULO foreign key (ID_MODULO)
      references MODULO (ID_MODULO)
go

alter table MODULO
   add constraint FK_MODULO_REFERENCE_ROL foreign key (ID_ROL)
      references ROL (ID_ROL)
go

alter table PERMISOS
   add constraint FK_PERMISOS_REFERENCE_MENU foreign key (ID_MENU)
      references MENU (ID_MENU)
go

alter table USUARIO
   add constraint FK_USUARIO_REFERENCE_ROL foreign key (ID_ROL)
      references ROL (ID_ROL)
go

alter table USUARIO_CONJUNTO
   add constraint FK_USUARIO__REFERENCE_USUARIO foreign key (ID_USUARIO)
      references USUARIO (ID_USUARIO)
go

