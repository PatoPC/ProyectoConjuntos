using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitarios
{
    public class ConstantesConsumoAPI
    {

        #region Conjuntos
        public const string crearConjunto = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/";
        public const string crearListaConjuntos = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/CrearListaConjuntos";
        public const string buscarConjuntosAvanzado = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/ObtenerConjutosAvanzado";
        public const string buscarConjuntosPorID = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/";
        public const string EditarConjuntosPorID = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/Editar?id=";
        public const string EditarConjuntosEliminar = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/Eliminar?id=";
        public const string TodosConjuntos = ConstantesAplicacion.pathAPI + "/api/API_Conjuntos/ObtenerTodosConjuntos";
        #endregion

        #region Torre
        public const string GestionarTorres = ConstantesAplicacion.pathAPI + "/api/API_Torre/";
        public const string buscarTorresAvanzado = ConstantesAplicacion.pathAPI + "/api/API_Torre/ObtenerTorresAvanzado";
        public const string TorresPorIDEditar = ConstantesAplicacion.pathAPI + "/api/API_Torre/Editar?id=";
        public const string TorresPorIDEliminar = ConstantesAplicacion.pathAPI + "/api/API_Torre/Eliminar?id=";
        public const string buscarTorresPorConjunto = ConstantesAplicacion.pathAPI + "/api/API_Torre/ObtenerTorresPorIDConjunto?idConjunto=";

        #endregion

        #region Departamento
        public const string gestionarDepartamentoAPI = ConstantesAplicacion.pathAPI + "/api/API_Departamento/";
        public const string gestionarDepartamentoAPIEditar = ConstantesAplicacion.pathAPI + "/api/API_Departamento/Editar?id=";
        public const string gestionarDepartamentoAPIEliminar = ConstantesAplicacion.pathAPI + "/api/API_Departamento/Eliminar?id=";
        public const string buscarDepartamentoAvanzado = ConstantesAplicacion.pathAPI + "/api/ API_Departamento/ObtenerTorresAvanzado";
        public const string buscarDepartamentosPorIDTorre = ConstantesAplicacion.pathAPI + "/api/API_Departamento/ObtenerDepartamentoPorIDTorre?idTorre=";

        #endregion

        #region Persona
        public const string gestionarPersonaAPI = ConstantesAplicacion.pathAPI + "/api/persona/";
        public const string gestionarPersonaAPIEditar = ConstantesAplicacion.pathAPI + "/api/persona/Editar?id=";
        public const string gestionarPersonaAPIEliminar = ConstantesAplicacion.pathAPI + "/api/persona/Eliminar?id=";
        public const string buscarPersonaAvanzado = ConstantesAplicacion.pathAPI + "/api/persona/ObtenerPersonaAvanzado";
        public const string crearPersonaDepartamento = ConstantesAplicacion.pathAPI + "/api/persona/CrearPersonaDepartamento";
        public const string consultaTipoPersonaDepartamento = ConstantesAplicacion.pathAPI + "/api/persona/ConsultaPersonaDepartamento";

        #endregion

        #region Catalogo        
        public const string getGetCatalogosHijosPorCodigoPadre = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsChildsIDConjunto?codigoPadreCatalgo=";

        public const string getGetCatalogosHijosPorIDCatalogoPadre = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsChildsIDConjuntoIDCatalogoPadre?idCatalogoPadre=";
        public const string getGetCatalogosHijosPorNombre = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsChildsByName_ConjuntoRol?nombrePadre=";
        public const string getGetCatalogosPorHijosPorIDPadre = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsChildsByIDFather?idCodigoPadreCatalgo=";
        public const string getGetCatalogosPadreTios = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsUpLevel_Conjunto?idCatalogo=";
        public const string getGetCatalogosHermanosPorID = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogsSameLevelByID_Conjunto?idCatalogoHermano=";
        public const string getGetCatalogosPorIdCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/";
        public const string getGetCatalogosCreate = ConstantesAplicacion.pathAPI + "/api/Catalogo/Create";

        public const string getEditCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/Edit?idCatalogo=";
        public const string getNombreCatalogoIdConjuntos = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogoByNameIdConjunto?nameCatalogo=";
        public const string getNombreCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogoByName?nameCatalogo=";
        public const string getNombreExactoCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogoByNameExact?nameCatalogo=";
        public const string getCodigoCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetCatalogoByCodeIDConjunto?codigoCatalgo=";
        public const string obtenerCatalogoPorIDConjuntos = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetAllCatalogosByIDConjunto?idEmpresa=";
        public const string obtenerTodosLosCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/GetAllCatalogosWithOutConjunto";
        public const string getDeleteCatalogo = ConstantesAplicacion.pathAPI + "/api/Catalogo/DeleteCatalogo?idCatalogo=";

        #endregion

        #region Usuario
        public const string getLogin = ConstantesAplicacion.pathAPI + "/api/Usuario/LoginUsuario?";
        public const string getCreateUsuario = ConstantesAplicacion.pathAPI + "/api/Usuario/Create";
        public const string getCreateUsuarioConjunto = ConstantesAplicacion.pathAPI + "/api/Usuario/CreateUsuarioConjunto";
        public const string getCreateUsuarioConjuntoLista = ConstantesAplicacion.pathAPI + "/api/Usuario/CreateUsuarioConjuntoLista";
        public const string getEditUsuario = ConstantesAplicacion.pathAPI + "/api/Usuario/Edit?IdUsuario=";
        public const string getEditarUsuarioReingreso = ConstantesAplicacion.pathAPI + "/api/Usuario/EditarRecontratar?IdUsuario=";
        public const string getUsuarioByID = ConstantesAplicacion.pathAPI + "/api/Usuario/";
        public const string getAdvancedSearch = ConstantesAplicacion.pathAPI + "/api/Usuario/GetUsuariosAdvanced";
        public const string getUsuarioByIDPersona = ConstantesAplicacion.pathAPI + "/api/Usuario/GetUsuariosPorIDPersona?idPersona=";
        public const string eliminarUsuario = ConstantesAplicacion.pathAPI + "/api/Usuario/Delete?IdUsuario=";
        public const string crearUsuarioEmpleado = ConstantesAplicacion.pathAPI + "/api/Usuario/CreateUser";
        public const string apiOlvidoContrasena = ConstantesAplicacion.pathAPI + "/api/EnvioCorreo/EnviarCorreoOlvidoConstrasena?correoOlvido=";
        public const string apiUsuarioCambioContrasena = ConstantesAplicacion.pathAPI + "/api/Usuario/UpdateCambioContrasenia?idUsuario=";
        #endregion

        #region ROL
        public const string GetAllRolsByConjunto = ConstantesAplicacion.pathAPI + "/api/Rol/GetAllRolsByConjunto";
        public const string getGetRolCreate = ConstantesAplicacion.pathAPI + "/api/Rol/Create";
        public const string getGetRolEditar = ConstantesAplicacion.pathAPI + "/api/Rol/Edit?idRol=";
        public const string getGetRolEliminar = ConstantesAplicacion.pathAPI + "/api/Rol/Delete?idRol=";
        public const string endPointRolByID = ConstantesAplicacion.pathAPI + "/api/Rol/";
        public const string getRolPorNombre = ConstantesAplicacion.pathAPI + "/api/Rol/GetRolPorNombre?nombreRol=";
        public const string getRolPorNombreExacto = ConstantesAplicacion.pathAPI + "/api/Rol/GetRolPorNombreExacto?nombreRolExacto=";
        #endregion

        #region Proveedor
        public const string gestionarProveedorAPI = ConstantesAplicacion.pathAPI + "/api/proveeedor/";
        public const string gestionarProveedorAPIEditar = ConstantesAplicacion.pathAPI + "/api/proveeedor/Editar?id=";
        public const string gestionarProveedorAPIEliminar = ConstantesAplicacion.pathAPI + "/api/proveeedor/Eliminar?id=";
        public const string buscarProveedorAvanzado = ConstantesAplicacion.pathAPI + "/api/proveeedor/BusquedaAvanzadaProveedor";
        #endregion

        #region Maestro Contable
        public const string gestionarMaestroContableAPI = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/";
        public const string apiCrearListaMaestro = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/CrearListaMaestro";
        public const string gestionarMaestroContableAPIEditar = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/Editar?id=";
        public const string gestionarMaestroConableAPIEliminar = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/Eliminar?id=";
        public const string buscarMaestroContableTodos = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/ObtenerMaestroContableTodos";
        public const string buscarMaestroContableAvanzado = ConstantesAplicacion.pathAPI + "/api/API_MaestroContable/MaestroContableBusquedaAvanzada";
        #endregion

        #region Adeudo
        public const string gestionarAdeudoAPI = ConstantesAplicacion.pathAPI + "/api/adeudo/";
        //public const string gestionarPersonaAPIEditar = ConstantesAplicacion.pathAPI + "/api/persona/Editar?id=";
        //public const string gestionarPersonaAPIEliminar = ConstantesAplicacion.pathAPI + "/api/persona/Eliminar?id=";
        public const string buscarAdeudoAvanzado = ConstantesAplicacion.pathAPI + "/api/adeudo/BusquedaAvanzadaAdeudo";
        //public const string crearPersonaDepartamento = ConstantesAplicacion.pathAPI + "/api/persona/CrearPersonaDepartamento";
        //public const string consultaTipoPersonaDepartamento = ConstantesAplicacion.pathAPI + "/api/persona/ConsultaPersonaDepartamento";

        #endregion

        #region Logs
        public const string crearLogs = ConstantesAplicacion.pathAPI + "/api/Logs/Create";
        public const string busquedaLogs = ConstantesAplicacion.pathAPI + "/api/Logs/BusquedaAvanzada";
        public const string busquedaLogsPorID = ConstantesAplicacion.pathAPI + "/api/Logs/";
        #endregion

        #region Comunicado
        public const string CrearComunicado = ConstantesAplicacion.pathAPI + "/api/Comunicado/";
        public const string BuscarComunicadoAvanzado = ConstantesAplicacion.pathAPI + "/api/Comunicado/BusquedaAvanzadaComunicado";
        public const string BuscarComunicadoPorID = ConstantesAplicacion.pathAPI + "/api/Comunicado/";
        public const string EditarComunicadoID = ConstantesAplicacion.pathAPI + "/api/Comunicado/Editar?id=";
        public const string EditarComunicadoEliminar = ConstantesAplicacion.pathAPI + "/api/Comunicado/Eliminar?id=";
        #endregion

        #region AreaComunal
        public const string CrearAreaComunal = ConstantesAplicacion.pathAPI + "/api/AreaComunal/";
        public const string BuscarAreaComunalAvanzado = ConstantesAplicacion.pathAPI + "/api/AreaComunal/BusquedaAvanzadaAreaComu";
        public const string BuscarAreaComunalPorID = ConstantesAplicacion.pathAPI + "/api/AreaComunal/";
        public const string EditarAreaComunal = ConstantesAplicacion.pathAPI + "/api/AreaComunal/Editar?id=";
        public const string EliminarAreaComunal = ConstantesAplicacion.pathAPI + "/api/AreaComunal/Eliminar?id=";
        public const string BuscarAreasComunalesPorIdConjunto = ConstantesAplicacion.pathAPI + "/api/AreaComunal/BuscarAreasComunalesPorIdConjunto?IdConjunto=";
        #endregion

        
        #region Parametro
        public const string CrearParametro = ConstantesAplicacion.pathAPI + "/api/Parametro/";
        public const string BuscarParamtroAvanzado = ConstantesAplicacion.pathAPI + "/api/Parametro/BusquedaAvanzaParametro";
        public const string BuscarParametroPorID = ConstantesAplicacion.pathAPI + "/api/Parametro/";
        public const string EditarParametro = ConstantesAplicacion.pathAPI + "/api/Parametro/Editar?id=";
        public const string EliminarParametro = ConstantesAplicacion.pathAPI + "/api/Parametro/Eliminar?id=";
        public const string obtenerParametroPorCatalogo = ConstantesAplicacion.pathAPI + "/api/Parametro/RecuperarParametroModulo?idModuloCatalogo=";
        //public const string BuscarParametroPorIdConjunto = ConstantesAplicacion.pathAPI + "/api/Parametro/BuscarAreasComunalesPorIdConjunto?IdConjunto=";
        #endregion


    }
}
