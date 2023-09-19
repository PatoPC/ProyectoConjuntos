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

        public C_ParametroController(IServicioConsumoAPI<ParametroCrearDTO> servicioConsumoAPICrear, IServicioConsumoAPI<MaestroContableBusqueda> servicioConsumoAPIBusqueda)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
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

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.BuscarParametroPorID + IdParametro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    ParametroCompletoDTO objMaestroContable = await LeerRespuestas<ParametroCompletoDTO>.procesarRespuestasConsultas(respuesta);

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

                


                

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarParametro(ParametroCompletoDTO objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                objModeloVista.Estado = true;
                //HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.CrearParametro, HttpMethod.Post, objModeloVista);

                //if (respuesta.IsSuccessStatusCode)
                //{
                //    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
                //}
                //else
                //{
                //    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                //    return new JsonResult(objMensajeRespuesta);
                //}
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion


        [HttpGet]
        public async Task<ActionResult> BusquedaParametros()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<CatalogoDTOResultadoBusqueda> listaCatalogo = new List<CatalogoDTOResultadoBusqueda>();

                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.nombrePadreParam, HttpMethod.Get);

                    listaCatalogo = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuesta);
                }
                catch (Exception ex)
                {

                }

                if (listaCatalogo == null)
                    listaCatalogo = new List<CatalogoDTOResultadoBusqueda>();

                return View("_ListaParametros", listaCatalogo);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
    }
}
