using AutoMapper;
using DTOs.CatalogoGeneral;
using DTOs.Persona;
using DTOs.Select;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_PersonaController : Controller
    {
        private const string controladorActual = "C_Persona";
        private const string accionActual = "AdministrarPersona";

        private readonly IServicioConsumoAPI<PersonaDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<TipoPersonaDTO> _servicioConsumoAPICrearTipoPersona;
        private readonly IServicioConsumoAPI<ObjTipoPersonaDepartamento> _servicioConsumoAPIBusquedaTipoPersona;
        private readonly IServicioConsumoAPI<PersonaDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<ObjetoBusquedaPersona> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IMapper _mapper;

        public C_PersonaController(IServicioConsumoAPI<PersonaDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<PersonaDTOEditar> servicioConsumoAPIEditar, IServicioConsumoAPI<ObjetoBusquedaPersona> servicioConsumoAPIBusqueda, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IMapper mapper, IServicioConsumoAPI<TipoPersonaDTO> servicioConsumoAPICrearTipoPersona, IServicioConsumoAPI<ObjTipoPersonaDepartamento> servicioConsumoAPIBusquedaTipoPersona)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _mapper = mapper;
            _servicioConsumoAPICrearTipoPersona = servicioConsumoAPICrearTipoPersona;
            _servicioConsumoAPIBusquedaTipoPersona = servicioConsumoAPIBusquedaTipoPersona;
        }


        [HttpGet]
        public IActionResult AdministrarPersona()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region CRUD

        #region CrearPersona
        public async Task<ActionResult> CrearPersona()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                await DatosInciales();

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearPersona(PersonaDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                //Añadir un campo para celular y  otro para telefono
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                ObjetoBusquedaPersona objBusqueda = new ObjetoBusquedaPersona();
                objBusqueda.IdentificacionPersona = objDTO.IdentificacionPersona;

                HttpResponseMessage respuestaDuplicados = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarPersonaAvanzado, HttpMethod.Get, objBusqueda);

                var listaResultado = await LeerRespuestas<List<PersonaDTOCompleto>>.procesarRespuestasConsultas(respuestaDuplicados);

                if (listaResultado == null)
                    listaResultado = new List<PersonaDTOCompleto>();

                if (listaResultado.Count() == 0)
                {
                    HttpResponseMessage respuestaCedula = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.CodigoCatalogoCedula, HttpMethod.Get);
                    var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCedula);

                    //Recuperar catalogo seleccionado
                    bool cedulaValida = FuncionesUtiles.validacionNumeroCedula(objDTO.IdentificacionPersona, objCatalogo.NombreCatalogo);

                    if (cedulaValida)
                    {
                        HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI, HttpMethod.Post, objDTO);

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
                    else
                    {
                        return new JsonResult(MensajesRespuesta.errorCedulaIncorrecta());
                    }
                }

                return new JsonResult(MensajesRespuesta.errorMensajePersonalizado("Error, ya existe una persona con el número de identificación ingresado."));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Detalle/Editar Persona
        public async Task<ActionResult> DetallePersona(Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    await DatosInciales();
                    PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Editar
        public async Task<ActionResult> EditarPersona(Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    await DatosInciales();
                    PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarPersona(PersonaDTOEditar objDTO, Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPIEditar + IdPersona, HttpMethod.Post, objDTO);

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

        #region Eliminar Persona
        public async Task<ActionResult> EliminarPersona(Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);
                    await DatosInciales();

                    return View(objDTO);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarPersona(Guid IdPersona, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPIEliminar + IdPersona, HttpMethod.Post);

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

        #region Asignar Persona Departamento
        public async Task<ActionResult> AsignarConjuntoPersona(Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);
                    SelectList objSelectListaConjunto = new SelectList(objUsuarioSesion.ListaConjuntosAcceso, "IdConjunto", "NombreConjunto", objUsuarioSesion.IdConjuntoDefault);

                    await DatosInciales();

                    PersonaDTOConjunto objPersonaConjunto = _mapper.Map<PersonaDTOConjunto>(objDTO);

                    objPersonaConjunto.IdConjunto = objUsuarioSesion.IdConjuntoDefault;

                    ViewData["listaTipoPersonas"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoPersona, "IdCatalogo", "Nombrecatalogo");

                    ViewData["listaConjuntos"] = objSelectListaConjunto;

                    return View(objPersonaConjunto);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> AsignarConjuntoPersona(PersonaDTOConjunto objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                TipoPersonaDTO objDTOTipoPersona = _mapper.Map<TipoPersonaDTO>(objDTO);

                objDTOTipoPersona.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                objDTOTipoPersona.UsuarioModificacion = objDTOTipoPersona.UsuarioCreacion;

                HttpResponseMessage respuesta = await _servicioConsumoAPICrearTipoPersona.consumoAPI(ConstantesConsumoAPI.crearPersonaDepartamento, HttpMethod.Post, objDTOTipoPersona);

                if (respuesta.IsSuccessStatusCode)
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));

                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        //Esta consulta esta pensada para evitar que exista un departamento asignado a dos personas
        [HttpGet]
        public async Task<JsonResult> BusquedTipoPersonaDepartamento(ObjTipoPersonaDepartamento objBusquedaConjuntos)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaTipoPersona.consumoAPI(ConstantesConsumoAPI.consultaTipoPersonaDepartamento, HttpMethod.Get, objBusquedaConjuntos);
            TipoPersonaDTO resultado = new TipoPersonaDTO();

            if (respuesta.IsSuccessStatusCode)
            {
                resultado = await LeerRespuestas<TipoPersonaDTO>.procesarRespuestasConsultas(respuesta);
            }

            return new JsonResult(resultado);
        }


        #endregion

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaPersona(ObjetoBusquedaPersona objBusquedaConjuntos)
        {
            List<PersonaDTOCompleto> listaResultado = new List<PersonaDTOCompleto>();
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarPersonaAvanzado, HttpMethod.Get, objBusquedaConjuntos);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        listaResultado = await LeerRespuestas<List<PersonaDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                        foreach (var resultado in listaResultado)
                        {
                            HttpResponseMessage respuestaUsuario = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getUsuarioByIDPersona + resultado.IdPersona, HttpMethod.Get);
                            if (respuestaUsuario.IsSuccessStatusCode)
                            {
                                UsuarioDTOCompleto objDTOUsuario = await LeerRespuestas<UsuarioDTOCompleto>.procesarRespuestasConsultas(respuestaUsuario);

                                resultado.IdUsuario = objDTOUsuario.IdUsuario;
                            }

                            if (resultado.TipoPersonas != null)
                            {
                                foreach (var item in resultado.TipoPersonas)
                                {
                                    HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + item.IdTipoPersonaDepartamento, HttpMethod.Get);

                                    CatalogoDTOCompleto objCatalogo = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaCatalogo);

                                    item.TipoPersona = objCatalogo.Nombrecatalogo;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }

                if (listaResultado == null)
                    listaResultado = new List<PersonaDTOCompleto>();

                return View("_ListaPersona", listaResultado);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        public async Task DatosInciales()
        {
            ViewData["listaTipoIdentificacion"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoIdentificacion, "IdCatalogo", "Nombrecatalogo");
        }

    }
}
