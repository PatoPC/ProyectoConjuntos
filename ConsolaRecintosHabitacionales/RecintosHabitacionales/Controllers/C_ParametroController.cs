using DTOs.CatalogoGeneral;
using DTOs.MaestroContable;
using DTOs.Parametro;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IServicioConsumoAPI<ParametroCrearDTO> _servicioConsumoAPICrear;

        public C_ParametroController(IServicioConsumoAPI<ParametroCrearDTO> servicioConsumoAPICrear)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
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

                if (objModeloVista.IdCatalogopadre != null && objModeloVista.IdCatalogopadre != ConstantesAplicacion.guidNulo)
                {
                    HttpResponseMessage respuestaCatlogoPadre = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + objModeloVista.IdCatalogopadre, HttpMethod.Get);

                    CatalogoDTOCompleto objCatalogoPadre = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaCatlogoPadre);

                    if (objCatalogoPadre != null)
                    {
                        objModeloVista.NivelCatalogo = objCatalogoPadre.NivelCatalogo + 1;
                    }
                }
                else
                {

                    HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.nombrePadreParam, HttpMethod.Get);

                    CatalogoDTOResultadoBusqueda objCatalogoParam = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);
                    objModeloVista.IdCatalogopadre = objCatalogoParam.IdCatalogo;
                    objModeloVista.NivelCatalogo = 0;
                }
              
                objModeloVista.CodigoCatalogo = FuncionesUtiles.GenerarCadena();
                objModeloVista.Estado = true;
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getGetCatalogosCreate, HttpMethod.Post, objModeloVista);

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

                return View("_ListaMaestroContable", listaCatalogo);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
    }
}
