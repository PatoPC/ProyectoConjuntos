using DTOs.Logs;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio.Interface;
using RecintosHabitacionales.Servicio;
using Utilitarios;
using DTOs.Persona;

namespace RecintosHabitacionales.Controllers
{
    public class C_LogsController : Controller
    {
        private readonly IServicioConsumoAPI<LogBusqueda> _servicioConsumoAPIBusqueda;

        public C_LogsController(IServicioConsumoAPI<LogBusqueda> servicioConsumoAPIBusqueda)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
        }

        [HttpGet]
        public IActionResult IndexLog()
        {
            return View();
        }


        #region Busquedas
        public async Task<IActionResult> BusquedaAvanzada(LogBusqueda objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<ResultadoBusquedaLogDTO> listaRespuesta = new List<ResultadoBusquedaLogDTO>();

                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.busquedaLogs, HttpMethod.Get, objDTO);

                listaRespuesta = await LeerRespuestas<List<ResultadoBusquedaLogDTO>>.procesarRespuestasConsultas(respuesta);

                if (listaRespuesta == null)
                    listaRespuesta = new List<ResultadoBusquedaLogDTO>();

                return View("_ListaLogs", listaRespuesta);
            }

            return RedirectToAction("Ingresar", "C_Ingreso", "sesionCaducada=" + ConstantesAplicacion.mensajeSesionCaducada);
        }

        #endregion

        [HttpGet]
        public async Task<ActionResult> DetalleLogs(Guid IdLogExcepciones)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.busquedaLogsPorID + IdLogExcepciones, HttpMethod.Get);

                LogErrorCompleto objDTO = await LeerRespuestas<LogErrorCompleto>.procesarRespuestasConsultas(respuesta);


                if (objDTO != null)
                {
                    if (objDTO.IdUsuario != null && objDTO.IdUsuario != ConstantesAplicacion.guidNulo)
                    {
                        string urlConsumo = ConstantesConsumoAPI.gestionarPersonaAPI + objDTO.IdUsuario;
                        HttpResponseMessage respuestaPersona = await _servicioConsumoAPIBusqueda.consumoAPI(urlConsumo, HttpMethod.Get);


                        if (respuesta.IsSuccessStatusCode)
                        {
                            PersonaDTOConjunto objDTOPersona = await LeerRespuestas<PersonaDTOConjunto>.procesarRespuestasConsultas(respuestaPersona);
                        }
                    }

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso", "sesionCaducada=" + ConstantesAplicacion.mensajeSesionCaducada);

        }
    }
}
