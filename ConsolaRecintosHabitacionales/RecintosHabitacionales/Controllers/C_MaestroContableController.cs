using DTOs.CatalogoGeneral;
using DTOs.ConfiguracionCuenta;
using DTOs.MaestroContable;
using DTOs.MaestroContable.Archivo;
using DTOs.Select;
using DTOs.Usuarios;
using Newtonsoft.Json;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using XAct;

namespace RecintosHabitacionales.Controllers
{
    public class C_MaestroContableController : Controller
    {
        private const string controladorActual = "C_MaestroContable";
        private const string accionActual = "GestionarMaestro";

        private readonly IServicioConsumoAPI<MaestroContableDTOCrear> _servicioConsumoAPICrear;

        private readonly IServicioConsumoAPI<MaestroContableDTOCompleto> _servicioConsumoAPICompleto;
        private readonly IServicioConsumoAPI<MaestroContableBusqueda> _servicioConsumoAPIBusqueda;
        private readonly CargarMaestroContable _servicioMestroContable;
        public C_MaestroContableController(IServicioConsumoAPI<MaestroContableDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<MaestroContableDTOCompleto> servicioConsumoAPICompleto, IServicioConsumoAPI<MaestroContableBusqueda> servicioConsumoAPIBusqueda, CargarMaestroContable servicioMestroContable)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPICompleto = servicioConsumoAPICompleto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioMestroContable = servicioMestroContable;
        }

        public async Task<ActionResult> GestionarMaestro()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
                objBusqueda.IdConjunto = objUsuarioSesion.IdConjuntoDefault;

                List<MaestroContableDTOCompleto> listaMestroConta = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);
                
                listaMestroConta = listaMestroConta.Where(x => x.IdConMstPadre == Guid.Empty).ToList();               

                ViewData["listaCuentas"] = listaMestroConta;
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

      

        #region CRUD

        #region Crear
        [HttpGet]
        public IActionResult CrearMaestroContable()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearMaestroContable(MaestroContableDTOCrear objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI, HttpMethod.Post, objModeloVista);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public ActionResult CargarMaestroDesdeArchivo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;
                return View();
            }



            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CargarMaestroDesdeArchivo(Guid IdConjunto)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ConfiguraCuentasDTOCompleto objConfigurar = await recuperarRegistro(IdConjunto);

                List<ModeloArchivoMaestro> listaArchivoLeido = await construirMaestroArchivo(FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion));

                HttpResponseMessage respuesta = await construirDTOMaestroMayor(objUsuarioSesion, listaArchivoLeido, IdConjunto, objConfigurar);

                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        public async Task<List<ModeloArchivoMaestro>> construirMaestroArchivo(string usuarioCreacion)
        {
            IFormFileCollection archivosFormulario = Request.Form.Files;
            List<ModeloArchivoMaestro> listaArchivoLeido = new List<ModeloArchivoMaestro>();

            if (archivosFormulario != null)
            {
                foreach (var archivo in archivosFormulario)
                {
                    if (archivo.Length > 0)
                    {
                        listaArchivoLeido = await LeerArchivoMaestro.procesarArchivoExcel(archivo, usuarioCreacion);
                    }
                }
            }

            return listaArchivoLeido;
        }

        public async Task<HttpResponseMessage> construirDTOMaestroMayor(UsuarioSesionDTO objUsuarioSesion, List<ModeloArchivoMaestro> listaArchivoLeido, Guid IdConjunto, ConfiguraCuentasDTOCompleto objConfigurar)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();
            string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

            List<MaestroContableDTOCrear> lista = new List<MaestroContableDTOCrear>();

            foreach (var cuenta in listaArchivoLeido)
            {
                MaestroContableDTOCrear objCrearTemporal = new MaestroContableDTOCrear();

                string cuentaFormateda = FuncionesUtiles.FormatearCadenaCuenta(cuenta.ctacont, objConfigurar.Parametrizacion);

                if (cuentaFormateda=="2")
                {

                }

                string[] partesCuenta = cuentaFormateda.Split('.');

                objCrearTemporal.CuentaCon = cuenta.ctacont;
                objCrearTemporal.NombreCuenta = cuenta.nom_cuenta;
                objCrearTemporal.IdConjunto = IdConjunto;
                objCrearTemporal.Grupo = cuenta.grupo == "0" ? false : true;
                objCrearTemporal.UsuarioCreacion = usuarioCreacion;
                objCrearTemporal.FechaCreacion = DateTime.Now;

                int posicionAlta = partesCuenta.Length - 1;

                MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();

                try
                {
                    if (cuentaFormateda.Length == 1)
                    {
                        objBusqueda.CuentaCon = cuentaFormateda;
                    }

                    for (int i=0; i<posicionAlta;i++)
                    {
                        objBusqueda.CuentaCon += partesCuenta[i];
                    }
                }
                catch (Exception ex)
                {

                }

                objBusqueda.IdConjunto = IdConjunto;

                List<MaestroContableDTOCompleto> listaMestroConta = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);

                if (listaMestroConta.Count > 0)
                {
                    var cuentaPadre = listaMestroConta.FirstOrDefault();

                    objCrearTemporal.IdConMstPadre = cuentaPadre.IdConMst;
                }

             
                respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI, HttpMethod.Post, objCrearTemporal);
            }

            return respuesta;
        }

        #endregion

        #region Editar
        [HttpGet]
        public async Task<ActionResult> EditarMaestroContable(Guid idMaestro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + idMaestro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    MaestroContableDTOCompleto objMaestroContable = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                    MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
                    objBusqueda.IdConjunto = objMaestroContable.IdConjunto;
                    objBusqueda.Grupo = true;

                    List<MaestroContableDTOCompleto> listaMestroConta = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);

                    List<ObjetoSelectDropDown> listaSelect = new List<ObjetoSelectDropDown>();


                    listaSelect = listaMestroConta.Select(x => new ObjetoSelectDropDown { id = x.IdConMst.ToString(), texto = x.CuentaCon }).ToList();

                    SelectList objListaCuentas; 

                    if (objMaestroContable.IdConMstPadre != Guid.Empty)                        
                         objListaCuentas = new SelectList(listaSelect, "id", "texto", objMaestroContable.IdConMstPadre);
                    
                    else
                        objListaCuentas = new SelectList(Enumerable.Empty<SelectListItem>());
                    
                    

                    ViewData["listaCuentas"] = objListaCuentas;

                    return View(objMaestroContable);

                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        //public async Task<ActionResult> EditarMaestroContable(Guid IdConMst, MaestroContableDTOEditar objModeloVista)
        public async Task<ActionResult> EditarMaestroContable(MaestroContableDTOCompleto objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICompleto.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPIEditar + objModeloVista.IdConMst, HttpMethod.Post, objModeloVista);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        #endregion

        #region Eliminar
        [HttpGet]
        public async Task<ActionResult> EliminarMaestroContable(Guid idMaestro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + idMaestro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    MaestroContableDTOCompleto objMaestroContable = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                    return View(objMaestroContable);
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        public async Task<ActionResult> EliminarMaestroContable(MaestroContableDTOCompleto objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICompleto.consumoAPI(ConstantesConsumoAPI.gestionarMaestroConableAPIEliminar + objModeloVista.IdConMst, HttpMethod.Post, objModeloVista);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual, true));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        #endregion


        #endregion

        #region Consultas
        [HttpGet]
        public async Task<ActionResult> BusquedaMaestroContable(MaestroContableBusqueda objBusqueda)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<MaestroContableDTOCompleto> listaMestroConta = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);

                return View("_ListaMaestroContable", listaMestroConta);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

     

        public async Task<JsonResult> cargarListaCuentasContables(Guid idConjunto)
        {
            MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
            List<CatalogoDTODropDown> listaFinal = new List<CatalogoDTODropDown>();

            objBusqueda.IdConjunto = idConjunto;
            objBusqueda.Grupo = false;            
           
                try
                {

                List<MaestroContableDTOCompleto> listaMestroConta = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);

                  listaFinal = listaMestroConta.Select(x => new CatalogoDTODropDown
                    {
                        IdCatalogo = x.IdConMst,
                        Nombrecatalogo = x.CuentaCon + " " + x.NombreCuenta
                    }).ToList();
                }
                catch (Exception ex)
                {
                    listaFinal = new List<CatalogoDTODropDown>();
                }
            


            return new JsonResult(listaFinal);
        }

        #endregion

        private async Task<ConfiguraCuentasDTOCompleto> recuperarRegistro(Guid IdConjunto)
        {
            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

            try
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.buscarConfiguracion + IdConjunto, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                    objConfigurar = await LeerRespuestas<ConfiguraCuentasDTOCompleto>.procesarRespuestasConsultas(respuesta);
            }
            catch (Exception ex)
            {
                objConfigurar = new ConfiguraCuentasDTOCompleto();
            }

            return objConfigurar;
        }
    }
}
