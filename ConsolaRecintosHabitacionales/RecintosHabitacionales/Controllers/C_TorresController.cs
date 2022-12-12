using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_TorresController : Controller
    {
        private const string controladorActual = "C_Torres";
        private const string accionActual = "AdministrarTorres";

        private readonly IServicioConsumoAPI<TorreDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<TorreDTOEditar> _servicioConsumoAPICrearEditar;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusqueda;

        public C_TorresController(IServicioConsumoAPI<TorreDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusqueda, IServicioConsumoAPI<TorreDTOEditar> servicioConsumoAPIConjuntoEditar)
        {
            _servicioConsumoAPICrear = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrearEditar = servicioConsumoAPIConjuntoEditar;
        }

        #region CRUD

        #region CrearTorres
        public IActionResult CrearTorres()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CrearTorres(TorreDTOCrear objDTO)
        {
            objDTO.UsuarioCreacion = "prueba";

            HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.GestionarTorres, HttpMethod.Post, objDTO);

            if (respuesta.IsSuccessStatusCode)
            {
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
            }
            else
            {
                MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                return new JsonResult(objMensajeRespuesta);
            }

            return View();
        }
        #endregion
        
        #region EditarTorres
        public async Task<ActionResult> EditarTorres(Guid idConjutos)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GestionarTorres + idConjutos, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                TorreDTOCompleto objDTO = await LeerRespuestas<TorreDTOCompleto>.procesarRespuestasConsultas(respuesta);

                return View(objDTO);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditarTorres(TorreDTOEditar objDTO, Guid IdTorre)
        {
            objDTO.UsuarioModificacion = "prueba";

            if (IdTorre == ConstantesAplicacion.guidNulo)
            {
                IdTorre = objDTO.IdTorresEditar;
            }

            HttpResponseMessage respuesta = await _servicioConsumoAPICrearEditar.consumoAPI(ConstantesConsumoAPI.TorresPorIDEditar + IdTorre, HttpMethod.Post, objDTO);

            if (respuesta.IsSuccessStatusCode)
            {
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
            }
            else
            {
                MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                return new JsonResult(objMensajeRespuesta);
            }

            return View();
        }
        #endregion


        #region EliminarTorres
       
        [HttpPost]
        public async Task<ActionResult> EliminarTorres(Guid IdTorresEditar, bool eliminar)
        {
           
            HttpResponseMessage respuesta = await _servicioConsumoAPICrearEditar.consumoAPI(ConstantesConsumoAPI.TorresPorIDEliminar+ IdTorresEditar, HttpMethod.Post);

            if (respuesta.IsSuccessStatusCode)
            {
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta,"","", true));
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
        public IActionResult AdministrarTorres()
        {
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{
              

                return View();
            //}

            //return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaTorres(BusquedaTorres objBusquedaTorres)
        {
            List<TorreDTOCompleto> listaResultado = new List<TorreDTOCompleto>();
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            if (respuesta.IsSuccessStatusCode)            
                listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<TorreDTOCompleto>();


            return View("_ListaTorres", listaResultado);
        }

        
        [HttpGet]
        public async Task<JsonResult> BusquedaPorTorresID(Guid IdTorres)
        {
            TorreDTOCompleto objTorre = new TorreDTOCompleto();
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GestionarTorres + IdTorres, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)            
                objTorre = await LeerRespuestas<TorreDTOCompleto>.procesarRespuestasConsultas(respuesta);

            if (objTorre == null)
                objTorre = new TorreDTOCompleto();

            return new JsonResult(objTorre);
        }


        

    }
}
