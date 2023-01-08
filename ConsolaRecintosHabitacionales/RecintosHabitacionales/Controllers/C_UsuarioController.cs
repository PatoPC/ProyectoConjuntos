using DTOs.Persona;
using DTOs.Roles;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_UsuarioController : Controller
    {
        private readonly IServicioConsumoAPI<ObjetoBusquedaUsuarios> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<UsuarioDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<UsuarioDTOEditar> _servicioConsumoAPIEditar;
        string controladorActual = "C_Usuario";
        string accionActual = "GestionUsuarios";

        public C_UsuarioController(IServicioConsumoAPI<ObjetoBusquedaUsuarios> servicioConsumoAPIBusqueda, IServicioConsumoAPI<UsuarioDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<UsuarioDTOEditar> servicioConsumoAPIEditar)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
        }

        public IActionResult GestionUsuarios()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                return View();

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region CRUD 

        #region Crear
        public async Task<ActionResult> CrearUsuarios(Guid IdPersona)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if(objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + IdPersona, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    await cargaInicial(objUsuarioSesion);
                    PersonaDTOCompleto objDTOPersona = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    UsuarioDTOCrear objUsuario = new UsuarioDTOCrear();

                    objUsuario.IdPersona = objDTOPersona.IdPersona;
                    objUsuario.IdentificacionPersona = objDTOPersona.IdentificacionPersona;
                    objUsuario.NombresCompletos = objDTOPersona.NombresPersona+" "+objDTOPersona.ApellidosPersona;
                    objUsuario.CorreoElectronico = objDTOPersona.EmailPersona;

                    return View(objUsuario);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearUsuarios(UsuarioDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if(objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                objDTO.Contrasena = FuncionesContrasena.encriptarContrasena(objDTO.IdentificacionPersona);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCreateUsuario, HttpMethod.Post, objDTO);

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

        #endregion Crear

        #region Editar Usuario

        public async Task<ActionResult> EditarUsuario(Guid IdUsuario)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getUsuarioByID + IdUsuario, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    
                    UsuarioDTOCompleto objDTO = await LeerRespuestas<UsuarioDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    HttpResponseMessage respuestaPersona = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + objDTO.IdPersona, HttpMethod.Get);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await cargaInicial(objUsuarioSesion, objDTO);
                        PersonaDTOCompleto objDTOPersona = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuestaPersona);

                        objDTO.IdPersona = objDTOPersona.IdPersona;
                        objDTO.IdentificacionPersona = objDTOPersona.IdentificacionPersona;
                        objDTO.NombresCompletos = objDTOPersona.NombresPersona + " " + objDTOPersona.ApellidosPersona;
                        objDTO.CorreoElectronico = objDTOPersona.EmailPersona;

                        return View(objDTO);
                    }
                }
            }
            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        public async Task<ActionResult> EditarUsuario(Guid IdUsuario, UsuarioDTOEditar objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.getEditUsuario+ IdUsuario, HttpMethod.Post, objDTO);

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


        #region Editar Usuario

        public async Task<ActionResult> EliminarUsuario(Guid IdUsuario)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getUsuarioByID + IdUsuario, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {

                    UsuarioDTOCompleto objDTO = await LeerRespuestas<UsuarioDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    HttpResponseMessage respuestaPersona = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + objDTO.IdPersona, HttpMethod.Get);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        await cargaInicial(objUsuarioSesion, objDTO);
                        PersonaDTOCompleto objDTOPersona = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuestaPersona);

                        objDTO.IdPersona = objDTOPersona.IdPersona;
                        objDTO.IdentificacionPersona = objDTOPersona.IdentificacionPersona;
                        objDTO.NombresCompletos = objDTOPersona.NombresPersona + " " + objDTOPersona.ApellidosPersona;
                        objDTO.CorreoElectronico = objDTOPersona.EmailPersona;

                        return View(objDTO);
                    }
                }
            }
            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        public async Task<ActionResult> EliminarUsuario(Guid IdUsuario, UsuarioDTOEditar objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.eliminarUsuario + IdUsuario, HttpMethod.Post, objDTO);

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


        #endregion CRUD

        public async Task<ActionResult> BusquedaAvanzadaPersona(ObjetoBusquedaUsuarios objDTO)
        {
            List<UsuarioResultadoBusquedaDTO> listaResultado = new List<UsuarioResultadoBusquedaDTO>();
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getAdvancedSearch, HttpMethod.Get, objDTO);

                    if (respuesta.IsSuccessStatusCode)
                        listaResultado = await LeerRespuestas<List<UsuarioResultadoBusquedaDTO>>.procesarRespuestasConsultas(respuesta);
                }
                catch (Exception ex)
                {

                }

                if (listaResultado == null)
                    listaResultado = new List<UsuarioResultadoBusquedaDTO>();

                return View("_ListaUsuarios", listaResultado);
            }

            return RedirectToAction(accionActual, controladorActual);
        }

        public async Task cargaInicial(UsuarioSesionDTO objUsuarioSesion, UsuarioDTOCompleto? usuarioEdicion=null)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GetAllRolsByConjunto, HttpMethod.Get);
            var listaRoles = await LeerRespuestas<List<RolDTOBusqueda>>.procesarRespuestasConsultas(respuesta);

            var listaRolesRestringidos = listaRoles.Where(x => x.RolRestringido && x.NombreRol != objUsuarioSesion.NombreRol).ToList();

            foreach (var rolRestringido in listaRolesRestringidos)
            {
                listaRoles.Remove(rolRestringido);
            }

            SelectList objSelectListRoles = new SelectList(listaRoles, "IdRol", "NombreRol");

            ViewData["listaRoles"] = objSelectListRoles;

            SelectList objSelectListaConjunto = new SelectList(objUsuarioSesion.ListaConjuntosAcceso, "IdConjunto", "NombreConjunto", objUsuarioSesion.IdConjuntoDefault);

            ViewData["listaConjuntos"] = objSelectListaConjunto;

            if (usuarioEdicion != null)
            {
                SelectList listaTemporal = new SelectList(usuarioEdicion.UsuarioConjuntos, "IdConjunto", "NombreConjunto", usuarioEdicion.IdConjuntoDefault);

                ViewData["listaConjuntosSeleccionados"] = listaTemporal;
            }
            else
            {
                SelectList listaTemporal = new SelectList(new List<SelectListItem>());
                ViewData["listaConjuntosSeleccionados"] = listaTemporal;
            }

        }

    }
}
