INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA,   FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( 0, 'Tipo Identificaci�n', 'TPIDENT', 'Padre tipo identificaci�n', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');

SELECT * 
from Catalogo_Conjunto.dbo.CATALOGO

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPIDENT'), 
0, 'C�dula', 'CDULA', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');

INSERT INTO Catalogo_Conjunto.dbo.CATALOGO
(ID_CATALOGOPADRE,NIVEL_CATALOGO, NOMBRE_CATALOGO, CODIGO_CATALOGO, DESCRIPCION, EDITABLE, ESTADO, TIENE_VIGENCIA, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES( (select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='TPIDENT'), 
0, 'Pasaporte', 'PSPORT', '', 0, 1, 0, getdate(), getdate(), 'admin', 'admin');


INSERT INTO Condominios.dbo.PERSONA
(ID_TIPO_IDENTIFICACION, NOMBRES_PERSONA, APELLIDOS_PERSONA, IDENTIFICACION_PERSONA, TELEFONO_PERSONA, EMAIL_PERSONA, OBSERVACION_PERSONA, FECHA_CREACION, FECHA_MODIFICACION, 
USUARIO_CREACION, USUARIO_MODIFICACION)
VALUES((select ID_CATALOGO from Catalogo_Conjunto.dbo.CATALOGO where CODIGO_CATALOGO ='CDULA'), 
'Patricio', 'C�rdova', '2200025787', '0982119036', 'patricio.cordova@outlook.com', '', getdate(), getdate(), 'admin', 'admin');


INSERT INTO Condominios_Permisos.dbo.ROL
(NOMBRE_ROL, ESTADO, FECHA_CREACION, FECHA_MODIFICACION, USUARIO_CREACION, USUARIO_MODIFICACION, ACCESO_TODOS, ROL_RESTRINGIDO)
VALUES('Administrador', 0, getdate(), getdate(), 'admin', 'admin', 1, 1);

SELECT ID_ROL from Condominios_Permisos.dbo.ROL where NOMBRE_ROL ='Administrador'
SELECT * from Condominios_Permisos.dbo.ROL

INSERT INTO Condominios_Permisos.dbo.MODULO
(ID_ROL, NOMBRE, ICONO_MODULO)
VALUES(
(SELECT ID_ROL from Condominios_Permisos.dbo.ROL where NOMBRE_ROL ='Administrador')
, 'Configuraci�n', '');

SELECT ID_MODULO  from Condominios_Permisos.dbo.MODULO where NOMBRE ='Configuraci�n'

INSERT INTO Condominios_Permisos.dbo.MENU
( ID_MODULO, NOMBRE_MENU, RUTA_MENU, DATO_ICONO)
VALUES(
(SELECT ID_MODULO  from Condominios_Permisos.dbo.MODULO where NOMBRE ='Configuraci�n'), 
'Cat�logos', '/C_Catalogo/GestionCatalogos', '');

SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBREMENU ='Cat�logos'


INSERT INTO Condominios_Permisos.dbo.PERMISOS
(ID_MENU, NOMBRE_PERMISO, CONCEDIDO, CSS_PERMISO)
values 
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Cat�logos'),'Eliminar', 1, 'btn btn-outline-danger'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Cat�logos'),'Lectura', 1, 'badge badge-primary'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Cat�logos'),'Escritura', 1, 'btn btn-outline-info'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Cat�logos'),'B�squedas', 1, 'badge badge-dark'),
((SELECT ID_MENU  from Condominios_Permisos.dbo.MENU where NOMBRE_MENU ='Cat�logos'),'Edici�n', 1, 'badge badge-success')

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
FROM Condominios_Permisos.dbo.USUARIO_CONJUNTO

SELECT *
from OrientoilRolesPermisos.dbo.USUARIO u 
where u.ID_PERSONA =(SELECT ID_PERSONA 
from OrientoilRRHH.dbo.PERSONA p 
where p.NUMERO_IDENTIFICACION ='2200025787')



SELECT ID_CONJUNTO from Condominios.dbo.CONJUNTO WHERE NOMBRE_CONJUNTO ='Condominio'

