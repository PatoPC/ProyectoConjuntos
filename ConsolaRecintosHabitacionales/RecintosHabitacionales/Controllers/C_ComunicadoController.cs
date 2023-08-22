using DTOs.AreasDepartamento;
using DTOs.CatalogoGeneral;
using DTOs.Comunicado;
using DTOs.Conjunto;
using DTOs.Departamento;
using DTOs.Select;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;
using XAct;

namespace RecintosHabitacionales.Controllers
{
    public class C_ComunicadoController : Controller
    {
        private const string controladorActual = "C_Comunicado";
        private const string accionActual = "GestionarComunicado";

        private readonly IServicioConsumoAPI<ComunicadoDTOCrear> _servicioConsumoAPI;
        private readonly IServicioConsumoAPI<ComunicadoDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<BusquedaComunicadoDTO> _servicioConsumoAPIBusqueda;

        public C_ComunicadoController(IServicioConsumoAPI<ComunicadoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<BusquedaComunicadoDTO> servicioConsumoAPIBusqueda, IServicioConsumoAPI<ComunicadoDTOEditar> servicioConsumoAPIConjuntoEditar)
        {
            _servicioConsumoAPI = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPIEditar = servicioConsumoAPIConjuntoEditar;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult> GestionarComunicado()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);


            if (objUsuarioSesion != null)
            {
                BusquedaComunicadoDTO objBusquedaComunicado = new BusquedaComunicadoDTO();

                objBusquedaComunicado.IdConjunto = objUsuarioSesion.IdConjuntoDefault;

                List<ComunicadoDTOCompleto> listaResultado = await buscarComunicados(objBusquedaComunicado);

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                ViewData["listaComunicados"] = listaResultado;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        #region Crear
        public IActionResult CrearComunicado()
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
        public async Task<ActionResult> CrearComunicado(ComunicadoDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                objDTO.Estado = true;

                HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.CrearComunicado, HttpMethod.Post, objDTO);

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

        public async Task<ActionResult> EditarComunicado(Guid IdComunicado)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                HttpResponseMessage respuestaConjunto = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.BuscarComunicadoPorID + IdComunicado, HttpMethod.Get);

                ComunicadoDTOCompleto objDTO = await LeerRespuestas<ComunicadoDTOCompleto>.procesarRespuestasConsultas(respuestaConjunto);

                return View(objDTO);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarComunicado(Guid IdComunicado, ComunicadoDTOEditar objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.EditarComunicadoID+ IdComunicado, HttpMethod.Post, objDTO);

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


        #region Eliminar Comunicado

        public async Task<ActionResult> EliminarComunicado(Guid IdComunicado)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;

                HttpResponseMessage respuestaConjunto = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.BuscarComunicadoPorID + IdComunicado, HttpMethod.Get);

                ComunicadoDTOCompleto objDTO = await LeerRespuestas<ComunicadoDTOCompleto>.procesarRespuestasConsultas(respuestaConjunto);

                return View(objDTO);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarComunicado(Guid IdComunicado, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.EditarComunicadoEliminar + IdComunicado, HttpMethod.Post);

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



        #endregion

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaComunicado(BusquedaComunicadoDTO objBusquedaComunicado)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                List<ComunicadoDTOCompleto> listaResultado = await buscarComunicados(objBusquedaComunicado);

                return View("_ListaDepartamento", listaResultado);

            }
            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        private async Task<List<ComunicadoDTOCompleto>> buscarComunicados(BusquedaComunicadoDTO objBusquedaComunicado)
        {
            List<ComunicadoDTOCompleto> listaResultadoFinal = new List<ComunicadoDTOCompleto>();

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.BuscarComunicadoAvanzado, HttpMethod.Get, objBusquedaComunicado);

            if (respuesta.IsSuccessStatusCode)
            {
                List<ComunicadoDTOCompleto> listaResultado = await LeerRespuestas<List<ComunicadoDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                var listaConjuntos = listaResultado.GroupBy(x => x.IdConjunto).ToList();

                foreach (var conjunto in listaConjuntos)
                {
                    HttpResponseMessage respuestaConjunto = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + conjunto.Key, HttpMethod.Get);

                    ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuestaConjunto);

                    if (respuestaConjunto.IsSuccessStatusCode)
                    {
                        if (objDTO != null)
                        {
                            List<ComunicadoDTOCompleto> listaTemporal = listaResultado.Where(x => x.IdConjunto == objDTO.IdConjunto)
                                                            .Select(y => new ComunicadoDTOCompleto
                                                            {
                                                                IdComunicado = y.IdComunicado,
                                                                IdConjunto = y.IdConjunto,
                                                                NombreConjunto = objDTO.NombreConjunto,
                                                                Titulo = y.Titulo,
                                                                Descripcion = FuncionesUtiles.ResumirString(y.Descripcion, 10, 1),
                                                                FechaCreacion = y.FechaCreacion,
                                                                FechaModificacion = y.FechaModificacion,
                                                                UsuarioCreacion = y.UsuarioCreacion,
                                                                UsuarioModificacion = y.UsuarioModificacion,
                                                            })
                                                            .ToList();

                            listaResultadoFinal = listaResultadoFinal.Union(listaTemporal).ToList();

                        }
                    }

                }
            }


            if (listaResultadoFinal == null)
                listaResultadoFinal = new List<ComunicadoDTOCompleto>();

            return listaResultadoFinal;
        }

    }
}
