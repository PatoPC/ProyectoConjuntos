INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 0, 'Tipo Persona', 'TIPPERSO', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TIPPERSO'), 
1, 'Condomino', 'OPCINQUI', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TIPPERSO'), 
1, 'Propietario', 'MDUEÑO', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

SELECT *
from Catalogo_Conjunto.dbo.CATALOGO c 
where c.CODIGO_CATALOGO ='OPCINQUI'

/*Tipo Areas*/
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 0, 'Tipo Areas', 'TIPAREAS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

/*Tipo Transacción*/
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 0, 'Tipo Transacción', 'PTTCOTP', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

/*MÓDULOS CONTABLES*/
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 0, 'Módulos Contables', 'MDLSCONT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='MDLSCONT'), 
2,'Adeudo Contable', 'MDLSADUD', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');


/*Catalogos para creación de Roles y permisos */
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 0, 'Modulos', 'MDLS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','fas fa-wrench');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ICONO)
VALUES( 1, 'Configuración', 'CONFG', 'Padre configuraciones', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','fas fa-wrench');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CONFG'), 
2, 'Logs', 'CONFGLOG', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Logs/IndexLog');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CONFG'), 
2 'Catálogos', 'CATLGS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Catalogo/GestionCatalogos');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CONFG'), 
2, 'Roles', 'ROLES', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Rol/AdministracionRol');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CONFG'), 
2, 'Roles', 'ROLES', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Rol/AdministracionRol');

/**Páginas de Inicio */
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( 0, 'Páginas Inicio', 'HOMEINI', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='HOMEINI'), 
1, 'Inicio Administrador', 'HomeAdmr', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/Home/Index');

/****************************************************/


/*Módulo Conjunto */
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='MDLS'), 
1, 'Gestión', 'GCONJT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCONJT'), 
2, 'Conjuntos', 'CONJT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Conjuntos/AdministrarConjuntos');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCONJT'), 
2, 'Adeudo', 'ADEUD', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Adeudo/GestionarAdeudo');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCONJT'), 
2, 'Comunicados', 'GCOMUN', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Comunicado/GestionarComunicado');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCONJT'), 
2, 'Persona', 'GCPERS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Persona/AdministrarPersona');


/****************************************************/


/*Módulo Contable */
INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='MDLS'), 
1, 'Contable', 'GCRCON', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCRCON'), 
2, ' Comprobantes', 'CONTCONT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Contabilidad/Comprobantes');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCRCON'), 
2, 'Gestión Maestro', 'CONTGEST', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_MaestroContable/GestionarMaestro');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='GCRCON'), 
2, 'Patrametros', 'CONTPATR', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','/C_Parametro/AdministrarParametros');

/****************************************************/

SELECT *
FROM Catalogo_Conjunto.dbo.CATALOGO c 
WHERE c.CODIGO_CATALOGO ='CONTCONT'


/**Tipo permiso*/

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( 0, 'Tipo Permiso', 'TPPMS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPPMS'), 
1, 'Edición', 'PEDC', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','btn btn-outline-success');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPPMS'), 
1, 'Búsquedas', 'CBSIS', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','btn btn-outline-dark');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPPMS'), 
1, 'Escritura', 'PESC', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','btn btn-outline-info');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPPMS'), 
1, 'Lectura', 'PLEC', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','btn btn-outline-primary');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, DATO_ADICIONAL)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPPMS'), 
1, 'Eliminar', 'PELI', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin','btn btn-outline-danger');


INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( 0, 'Tipo Identificación', 'TPIDENT', 'Padre tipo identificaci�n', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');

SELECT * 
from Catalogo_Conjunto.dbo.CATALOGO

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPIDENT'), 
0, 'Cédula', 'CEDULA', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPIDENT'), 
0, 'Pasaporte', 'PSPORT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');


INSERT INTO Condominios.dbo.PERSONA
(ID_TIPO_IDENTIFICACION, NOMBRES_PERSONA, APELLIDOS_PERSONA, IDENTIFICACION_PERSONA, TELEFONO_PERSONA, EMAIL_PERSONA, OBSERVACION_PERSONA, FECHA_CREACION, FECHA_MODIFICACION, 
USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES((select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CEDULA'), 
'Patricio', 'Córdova', '2200025787', '0982119036', 'patricio.cordova@outlook.com', '', getdate(), getdate(), 'admin', 'admin');

SELECT *
from Condominios.dbo.PERSONA

INSERT INTO Condominios_Permisos.dbo.ROL
(NOMBRE_ROL, ESTADO, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, ACCESO_TODOS, ROL_RESTRINGIDO)
VALUES('Administrador', 1, getdate(), getdate(), 'admin', 'admin', 1, 1);

SELECT ID_ROL from Condominios_Permisos.dbo.ROL where NOMBRE_ROL ='Administrador'
SELECT * from Condominios_Permisos.dbo.ROL

INSERT INTO Condominios_Permisos.dbo.MODULO
(ID_ROL, NOMBRE, ICONO_MODULO)
VALUES(
(SELECT ID_ROL from Condominios_Permisos.dbo.ROL where NOMBRE_ROL ='Administrador')
, 'Configuración', '');

SELECT ID_MODULO  from Condominios_Permisos.dbo.MODULO where NOMBRE ='Configuración'

INSERT INTO Condominios_Permisos.dbo.MENU
( ID_MODULO, NOMBRE_MENU, RUTA_MENU, DATO_ICONO)
VALUES(
(SELECT ID_MODULO  from Condominios_Permisos.dbo.MODULO where NOMBRE ='Configuración'), 
'Catálogos', '/C_Catalogo/GestionCatalogos', '');

SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBREMENU ='Catálogos'


INSERT INTO Condominios_Permisos.dbo.PERMISOS
(ID_MENU, NOMBRE_PERMISO, CONCEDIDO, CSS_PERMISO)
values 
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Catálogos'),'Eliminar', 1, 'btn btn-outline-danger'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Catálogos'),'Lectura', 1, 'badge badge-primary'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Catálogos'),'Escritura', 1, 'btn btn-outline-info'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Catálogos'),'Búsquedas', 1, 'badge badge-dark'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Catálogos'),'Edici�n', 1, 'badge badge-success')

INSERT INTO Condominios.dbo.CONJUNTO
(NOMBRE_CONJUNTO, RUC_CONJUNTO, DIRECCION_CONJUNTO, TELEFONO_CONJUNTO, MAIL_CONJUNTO, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES ('Condominio','2100025787001','esa misma','068264564','conjunto@conjunto.com',getdate(),getdate(),'admin','admin')

INSERT INTO Condominios_Permisos.dbo.USUARIO
(ID_ROL, ID_PERSONA, ID_CONJUNTO_DEFAULT, ESTADO, CORREO_ELECTRONICO, CONTRASENA_INICIAL, CONTRASENA, INDICIO_CONTRASENA, FECHA_ULTIMO_INGRESO, FECHA_CREACION, FECHA_MODIFICACION, 
USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES(
(SELECT ID_ROL from Condominios_Permisos.dbo.ROL where NOMBRE_ROL ='Administrador'), 
(select ID_PERSONA  from Condominios.dbo.PERSONA WHERE IDENTIFICACION_PERSONA ='2200025787'), 
(SELECT ID_CONJUNTO from Condominios.dbo.CONJUNTO WHERE NOMBRE_CONJUNTO ='Condominio'), 
1, 'patricio.cordova@outlook.com',0, 
'43fae6c11d7632acc6059de1cced9b09a58caaa878071308ad67f32ef6b11691004300fa00e600c1001d0076003200ac00c60005009d00e100cc00ed009b000900a5008c00aa00a8007800070013000800ad006700f3002e00f600',
'', getdate(),getdate(), getdate(), 'admin', 'admin')

SELECT *
FROM Condominios_Permisos.dbo.USUARIO

SELECT ID_USUARIO 
from Condominios_Permisos.dbo.USUARIO
where ID_PERSONA = (select ID_PERSONA  from Condominios.dbo.PERSONA WHERE IDENTIFICACION_PERSONA ='2200025787')

SELECT *
from Condominios_Permisos.dbo.USUARIO_CONJUNTO uc 

INSERT INTO Condominios_Permisos.dbo.USUARIO_CONJUNTO
(ID_USUARIO, ID_CONJUNTO)
VALUES(
(SELECT ID_USUARIO from Condominios_Permisos.dbo.USUARIO where ID_PERSONA = (select ID_PERSONA  from Condominios.dbo.PERSONA WHERE IDENTIFICACION_PERSONA ='2200025787')),
(SELECT ID_CONJUNTO from Condominios.dbo.CONJUNTO WHERE NOMBRE_CONJUNTO ='Condominio'))

SELECT *
from Condominios_Permisos.dbo.USUARIO u 

SELECT *
FROM Condominios_Permisos.dbo.USUARIO_CONJUNTO

SELECT ID_CONJUNTO from Condominios.dbo.CONJUNTO WHERE NOMBRE_CONJUNTO ='Condominio'

SELECT * 
from Condominios.dbo.CONJUNTO WHERE ID_CONJUNTO ='3476eaa4-0e8c-481f-b361-32524502bfc9'

SELECT * 
from Condominios.dbo.CONJUNTO WHERE ID_CONJUNTO ='3476eaa4-0e8c-481f-b361-32524502bfc9'

SELECT * 
from Condominios_Permisos.dbo.USUARIO WHERE ID_USUARIO  ='3476eaa4-0e8c-481f-b361-32524502bfc9'

select * 
from Condominios_Permisos.dbo.USUARIO

select * 
from Condominios.dbo.TIPO_PERSONA  

select *
from Condominios.dbo.CONJUNTO 
where NOMBRE_CONJUNTO !='Marianas'

select *
from Condominios.dbo.TORRES  
where ID_CONJUNTO in (
select ID_CONJUNTO 
from Condominios.dbo.CONJUNTO 
where NOMBRE_CONJUNTO !='Marianas')

SELECT *
from Condominios.dbo.DEPARTAMENTOS 
where ID_TORRES in (
select ID_TORRES
from Condominios.dbo.TORRES  
where ID_CONJUNTO in (
select ID_CONJUNTO 
from Condominios.dbo.CONJUNTO 
where NOMBRE_CONJUNTO !='Marianas'))


SELECT *
from Condominios.dbo.AREAS_DEPARTAMENTOS 
where ID_DEPARTAMENTO in (SELECT ID_DEPARTAMENTO 
from Condominios.dbo.DEPARTAMENTOS 
where ID_TORRES in (
select ID_TORRES
from Condominios.dbo.TORRES  
where ID_CONJUNTO in (
select ID_CONJUNTO 
from Condominios.dbo.CONJUNTO 
where NOMBRE_CONJUNTO !='Marianas' )))


SELECT *
from Condominios.dbo.PERSONA 
where IDENTIFICACION_PERSONA !='2200025787'

SELECT *
from Condominios_Permisos.dbo.USUARIO u 

SELECT *
from Condominios_Permisos.dbo.USUARIO_CONJUNTO 
where ID_CONJUNTO !='06e4199d-41f3-47f0-9c0b-2370f38e730f'
and ID_CONJUNTO !='ae40db51-0cf4-4e49-8837-72160f50c09a'
and ID_CONJUNTO != 'f02612fc-da7b-4a20-b37e-2a20edadb5e9'


SELECT *
from Condominios_Permisos.dbo.USUARIO_CONJUNTO 
where ID_USUARIO ='854A4AD5-2376-4F35-88A4-65AA621A45AD'






