using DTOs.AreaComunal;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Implementar;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_AreaComunalController : Controller
    {
        private readonly IServicioConsumoAPI<AreaComunalDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<AreaComunalDTOEditar> _servicioConsumoAPIEditar;

        public C_AreaComunalController(IServicioConsumoAPI<AreaComunalDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<AreaComunalDTOEditar> servicioConsumoAPIEditar)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
        }

        [HttpPost]
        public async Task<ActionResult> CrearAreaComunal(AreaComunalDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.CrearAreaComunal, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        public async Task<ActionResult> EditarAreaComunal(AreaComunalDTOEditar objDTO, Guid IdAreaComunalEditar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.EditarAreaComunal + IdAreaComunalEditar, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        
        [HttpPost]
        public async Task<ActionResult> EliminarAreaComunal(AreaComunalDTOEditar objDTO, Guid IdAreaComunalEditar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.EliminarAreaComunal + IdAreaComunalEditar, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta,"","",true));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        public async Task<ActionResult> DetalleAreaComunal(Guid idAreaComunal)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarAreaComunalPorID + idAreaComunal, HttpMethod.Get);

                AreaComunalDTOCompleto objDTO = await LeerRespuestas<AreaComunalDTOCompleto>.procesarRespuestasConsultas(respuesta);

                return View(objDTO);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpGet]
        public async Task<JsonResult> BusquedaPorAreaComunalID(Guid IdAreaComunal)
        {
            AreaComunalDTOCompleto objDTO = new AreaComunalDTOCompleto();

            HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.CrearAreaComunal + IdAreaComunal, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
                objDTO = await LeerRespuestas<AreaComunalDTOCompleto>.procesarRespuestasConsultas(respuesta);

            if (objDTO == null)
                objDTO = new AreaComunalDTOCompleto();

            return new JsonResult(objDTO);
        }


    }
}
