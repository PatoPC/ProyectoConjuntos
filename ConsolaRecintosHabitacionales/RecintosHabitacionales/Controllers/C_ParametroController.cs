using DTOs.CatalogoGeneral;
using DTOs.MaestroContable;
using DTOs.Parametro;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers

{
    public class C_ParametroController : Controller
    {
        private const string controladorActual = "C_Parametro";
        private const string accionActual = "AdministrarParametros";
        private readonly IServicioConsumoAPI<MaestroContableBusqueda> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<ParametroCrearDTO> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<BusquedaParametro> _servicioConsumoAPIParametro;
        private readonly IServicioConsumoAPI<ParametroEditarDTO> _servicioConsumoCompleto;

        public C_ParametroController(IServicioConsumoAPI<ParametroCrearDTO> servicioConsumoAPICrear, IServicioConsumoAPI<MaestroContableBusqueda> servicioConsumoAPIBusqueda, IServicioConsumoAPI<BusquedaParametro> servicioConsumoAPIParametro, IServicioConsumoAPI<ParametroEditarDTO> servicioConsumoCompleto)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPIParametro = servicioConsumoAPIParametro;
            _servicioConsumoCompleto = servicioConsumoCompleto;
        }

        public IActionResult AdministrarParametros()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");

        }

        #region Crear
        [HttpGet]
        public IActionResult CrearParametro()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<MaestroContableDTOCompleto> listaResultado = new List<MaestroContableDTOCompleto>();

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearParametro(ParametroCrearDTO objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                objModeloVista.Estado = true;
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.CrearParametro, HttpMethod.Post, objModeloVista);

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


        #region Editar
        [HttpGet]
        public async Task<ActionResult> EditarParametro(Guid IdParametro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            ParametroCompletoDTO objMaestroContable = new ParametroCompletoDTO();


            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.BuscarParametroPorID + IdParametro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    objMaestroContable = await LeerRespuestas<ParametroCompletoDTO>.procesarRespuestasConsultas(respuesta);

                    MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
                    List<CatalogoDTODropDown> listaFinal = new List<CatalogoDTODropDown>();

                    objBusqueda.IdConjunto = objMaestroContable.IdConjunto;
                    objBusqueda.Grupo = false;
                    HttpResponseMessage respuestaCuentas = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarMaestroContableAvanzado, HttpMethod.Get, objBusqueda);

                    var listaCuentas = await LeerRespuestas<List<MaestroContableDTOCompleto>>.procesarRespuestasConsultas(respuestaCuentas);

                    var listaCuentas1 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont1);

                    ViewData["listaCuentas1"] = listaCuentas1;

                    var listaCuentas2 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont2);

                    ViewData["listaCuentas2"] = listaCuentas2;

                    var listaCuentas3 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont3);

                    ViewData["listaCuentas3"] = listaCuentas3;

                    var listaCuentas4 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont4);

                    ViewData["listaCuentas4"] = listaCuentas4;
                }

                List<MaestroContableDTOCompleto> listaResultado = new List<MaestroContableDTOCompleto>();

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                return View(objMaestroContable);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarParametro(Guid IdParametro, ParametroEditarDTO objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoCompleto.consumoAPI(ConstantesConsumoAPI.EditarParametro+ IdParametro, HttpMethod.Post, objModeloVista);

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
        public async Task<ActionResult> EliminarParametro(Guid IdParametro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            ParametroCompletoDTO objMaestroContable = new ParametroCompletoDTO();


            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.BuscarParametroPorID + IdParametro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    objMaestroContable = await LeerRespuestas<ParametroCompletoDTO>.procesarRespuestasConsultas(respuesta);

                    MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
                    List<CatalogoDTODropDown> listaFinal = new List<CatalogoDTODropDown>();

                    objBusqueda.IdConjunto = objMaestroContable.IdConjunto;
                    objBusqueda.Grupo = false;
                    HttpResponseMessage respuestaCuentas = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarMaestroContableAvanzado, HttpMethod.Get, objBusqueda);

                    var listaCuentas = await LeerRespuestas<List<MaestroContableDTOCompleto>>.procesarRespuestasConsultas(respuestaCuentas);

                    var listaCuentas1 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont1);

                    ViewData["listaCuentas1"] = listaCuentas1;

                    var listaCuentas2 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont2);

                    ViewData["listaCuentas2"] = listaCuentas2;

                    var listaCuentas3 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont3);

                    ViewData["listaCuentas3"] = listaCuentas3;

                    var listaCuentas4 = new SelectList(listaCuentas, "IdConMst", "NombreCuenta", objMaestroContable.CtaCont4);

                    ViewData["listaCuentas4"] = listaCuentas4;
                }

                List<MaestroContableDTOCompleto> listaResultado = new List<MaestroContableDTOCompleto>();

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                return View(objMaestroContable);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarParametro(Guid IdParametro, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {             
                HttpResponseMessage respuesta = await _servicioConsumoCompleto.consumoAPI(ConstantesConsumoAPI.EliminarParametro + IdParametro, HttpMethod.Post);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual,true));
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


        [HttpGet]
        public async Task<ActionResult> BusquedaParametros(BusquedaParametro objBusqueda)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<ParametroCompletoDTO> listaParametros = new List<ParametroCompletoDTO>();

                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIParametro.consumoAPI(ConstantesConsumoAPI.BuscarParamtroAvanzado, HttpMethod.Get, objBusqueda);

                    listaParametros = await LeerRespuestas<List<ParametroCompletoDTO>>.procesarRespuestasConsultas(respuesta);

                    if (listaParametros == null)
                        listaParametros = new List<ParametroCompletoDTO>();

                    foreach (var parametro in listaParametros)
                    {
                        MaestroContableDTOCompleto objCuenta1 = await recuperarNombreCuenta(parametro.CtaCont1);

                        parametro.Cuenta1 = objCuenta1.NombreCuenta;

                        if (parametro.CtaCont2 != Guid.Empty && parametro.CtaCont2!=null)
                        {
                            MaestroContableDTOCompleto objCuenta2 = await recuperarNombreCuenta((Guid)parametro.CtaCont2);

                            parametro.Cuenta2 = objCuenta2.NombreCuenta;
                        }

                        if (parametro.CtaCont3 != Guid.Empty && parametro.CtaCont3 != null)
                        {
                            MaestroContableDTOCompleto objCuenta3 = await recuperarNombreCuenta((Guid)parametro.CtaCont3);

                            parametro.Cuenta3 = objCuenta3.NombreCuenta;
                        }

                        if (parametro.CtaCont4 != Guid.Empty && parametro.CtaCont4 != null)
                        {
                            MaestroContableDTOCompleto objCuenta = await recuperarNombreCuenta((Guid)parametro.CtaCont4);

                            parametro.Cuenta4 = objCuenta.NombreCuenta;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

              

                return View("_ListaParametros", listaParametros);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        private async Task<MaestroContableDTOCompleto> recuperarNombreCuenta(Guid CtaCont)
        {
            HttpResponseMessage respuestaMaestro = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + CtaCont, HttpMethod.Get);

            MaestroContableDTOCompleto objMaestroContable = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuestaMaestro);

            return objMaestroContable;
        }
    }
}
