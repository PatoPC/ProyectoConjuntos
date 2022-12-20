using DTOs.CatalogoGeneral;
using DTOs.Persona;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_PersonaController : Controller
    {
        private const string controladorActual = "C_Persona";
        private const string accionActual = "AdministrarPersona";

        private readonly IServicioConsumoAPI<PersonaDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<PersonaDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<ObjetoBusquedaPersona> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;

        public C_PersonaController(IServicioConsumoAPI<PersonaDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<PersonaDTOEditar> servicioConsumoAPIEditar, IServicioConsumoAPI<ObjetoBusquedaPersona> servicioConsumoAPIBusqueda, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
        }

        #region CRUD

        #region CrearPersona
        public async Task<ActionResult> CrearPersona()
        {
            await DatosInciales();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CrearPersona(PersonaDTOCrear objDTO)
        {
            objDTO.UsuarioCreacion = "prueba";

            ObjetoBusquedaPersona objBusquedaConjuntos = new ObjetoBusquedaPersona();
            objBusquedaConjuntos.IdentificacionPersona = objDTO.IdentificacionPersona;

            HttpResponseMessage respuestaDuplicados = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarPersonaoAvanzado, HttpMethod.Get, objBusquedaConjuntos);

            var listaResultado = await LeerRespuestas<List<PersonaDTOCompleto>>.procesarRespuestasConsultas(respuestaDuplicados);

            if (listaResultado == null)
                listaResultado = new List<PersonaDTOCompleto>();


            if (listaResultado.Count() == 0)
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

            return new JsonResult(MensajesRespuesta.errorMensajePersonalizado("Error, ya existe una persona con el número de identificación ingresado."));
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
            objDTO.UsuarioModificacion = "prueba";

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

            return View();
        }
        #endregion

        #region Eliminar Persona
        public async Task<ActionResult> EliminarPersona(Guid IdPersona)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);
                await DatosInciales();

                return View(objDTO);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EliminarPersona(Guid IdPersona, bool eliminar)
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

            return View();
        }
        #endregion

        #endregion

        [HttpGet]
        public IActionResult AdministrarPersona()
        {
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{


            return View();
            //}

            //return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaPersona(ObjetoBusquedaPersona objBusquedaConjuntos)
        {
            List<PersonaDTOCompleto> listaResultado = new List<PersonaDTOCompleto>();
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarPersonaoAvanzado, HttpMethod.Get, objBusquedaConjuntos);

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

        //public async Task<IActionResult> RecuperarListaTorresPorConjutoID(Guid idConjuto)
        //{
        //    BusquedaTorres objBusquedaTorres = new BusquedaTorres();

        //    objBusquedaTorres.IdPersona = idConjuto;

        //    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

        //    List<TorreDTOCompleto> listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

        //    if (listaResultado == null)
        //        listaResultado = new List<TorreDTOCompleto>();

        //    if (listaResultado != null)
        //    {
        //        return View("Torre/_ListaTorres", listaResultado);
        //    }

        //    return View("Torre/_ListaTorres", new List<TorreDTOCompleto>());
        //}

        public async Task DatosInciales()
        {
            ViewData["listaTipoIdentificacion"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoIdentificacion, "IdCatalogo", "Nombrecatalogo");
        }

    }
}
