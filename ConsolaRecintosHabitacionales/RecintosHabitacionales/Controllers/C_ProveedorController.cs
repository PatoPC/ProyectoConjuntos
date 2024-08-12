using AutoMapper;
using DTOs.CatalogoGeneral;
using DTOs.Proveedor;
using DTOs.Select;
using DTOs.Usuarios;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ProveedorController : Controller
    {
        private const string controladorActual = "C_Proveedor";
        private const string accionActual = "AdministrarProveedor";

        private readonly IServicioConsumoAPI<ProveedorDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<ProveedorDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<BusquedaProveedor> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IMapper _mapper;

        public C_ProveedorController(IServicioConsumoAPI<ProveedorDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<ProveedorDTOEditar> servicioConsumoAPIEditar, IServicioConsumoAPI<BusquedaProveedor> servicioConsumoAPIBusqueda, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IMapper mapper)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult AdministrarProveedor()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                BusquedaProveedor obj = new BusquedaProveedor();
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                //obj.idcon = objUsuarioSesion.IdConjuntoDefault;

                return View();
            }


            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region CRUD

        #region CrearProveedor
        public async Task<ActionResult> CrearProveedor()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                await DatosInciales();

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearProveedor(ProveedorDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                //Añadir un campo para celular y  otro para telefono
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                BusquedaProveedor objBusquedaDTO = new BusquedaProveedor();

                HttpResponseMessage respuestaDuplicados = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPI, HttpMethod.Get, objBusquedaDTO);

                var listaResultado = await LeerRespuestas<List<ProveedorDTOCompleto>>.procesarRespuestasConsultas(respuestaDuplicados);

                if (listaResultado == null)
                    listaResultado = new List<ProveedorDTOCompleto>();


                if (listaResultado.Count() == 0)
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPI, HttpMethod.Post, objDTO);

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

                return new JsonResult(MensajesRespuesta.errorMensajePersonalizado("Error, ya existe una Proveedor con el número de identificación ingresado."));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Detalle/Editar Proveedor
        public async Task<ActionResult> DetalleProveedor(Guid IdProveedor)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPI + IdProveedor, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {                    
                    ProveedorDTOCompleto objDTO = await LeerRespuestas<ProveedorDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                    await DatosInciales(objDTO.IdCiudadProveedor);
                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #endregion

        #region Editar
        public async Task<ActionResult> EditarProveedor(Guid IdProveedor)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPI + IdProveedor, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    
                    ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                    ProveedorDTOCompleto objDTO = await LeerRespuestas<ProveedorDTOCompleto>.procesarRespuestasConsultas(respuesta);
                    await DatosInciales(objDTO.IdCiudadProveedor);

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarProveedor(ProveedorDTOEditar objDTO, Guid IdProveedor)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPIEditar + IdProveedor, HttpMethod.Post, objDTO);

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

        #region Eliminar Proveedor
        public async Task<ActionResult> EliminarProveedor(Guid IdProveedor)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPI + IdProveedor, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    ProveedorDTOCompleto objDTO = await LeerRespuestas<ProveedorDTOCompleto>.procesarRespuestasConsultas(respuesta);
                    await DatosInciales(objDTO.IdCiudadProveedor);
                    ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                    return View(objDTO);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarProveedor(Guid IdProveedor, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.gestionarProveedorAPIEliminar + IdProveedor, HttpMethod.Post);

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

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaProveedor(BusquedaProveedor objBusquedaDTO)
        {
            List<ProveedorDTOCompleto> listaResultado = new List<ProveedorDTOCompleto>();
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            
            if (objUsuarioSesion != null)
            {
                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarProveedorAvanzado, HttpMethod.Get, objBusquedaDTO);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        listaResultado = await LeerRespuestas<List<ProveedorDTOCompleto>>.procesarRespuestasConsultas(respuesta);
                    }
                    else
                    {
                        MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    }
                }
                catch (Exception ex)
                {

                }

                if (listaResultado == null)
                    listaResultado = new List<ProveedorDTOCompleto>();

                return View("_ListaProveedor", listaResultado);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        public async Task DatosInciales(Guid? idCiudad=null)
        {
            CatalogoDTOResultadoBusqueda objCatalogoProvincia = new CatalogoDTOResultadoBusqueda();

            if (idCiudad!=null)
            {
                HttpResponseMessage respuestaHijosCiudad = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHermanosPorID + idCiudad, HttpMethod.Get);

                var listaCiudades = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuestaHijosCiudad);
                objCatalogoProvincia = listaCiudades.Where(x => x.IdCatalogo == idCiudad).FirstOrDefault();

                SelectList objSelectListCiudades = new SelectList(listaCiudades, "IdCatalogo", "NombreCatalogo", idCiudad);

                ViewData["listaCiudades"] = objSelectListCiudades;
            }

            HttpResponseMessage respuestaHijos = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.padrePais, HttpMethod.Get);

            var pais = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaHijos);
            var listaProvincias = pais.InverseIdCatalogopadreNavigation;

            var objSelectList = new SelectList(Enumerable.Empty<SelectListItem>());

            if (objCatalogoProvincia != null)            
                objSelectList = new SelectList(listaProvincias, "IdCatalogo", "NombreCatalogo", objCatalogoProvincia.IdCatalogopadre);            
            else            
                objSelectList = new SelectList(listaProvincias, "IdCatalogo", "NombreCatalogo");            
                

            ViewData["listaProvincias"] = objSelectList;

            ViewData["listaTipoIdentificacion"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoIdentificacion, "IdCatalogo", "Nombrecatalogo");
        }

    }

}
