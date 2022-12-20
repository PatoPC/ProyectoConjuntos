using DTOs.Select;
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
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearTorres(TorreDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.GestionarTorres, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));

                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }
            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region EditarTorres
        public async Task<ActionResult> EditarTorres(Guid idConjuntos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GestionarTorres + idConjuntos, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    TorreDTOCompleto objDTO = await LeerRespuestas<TorreDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarTorres(TorreDTOEditar objDTO, Guid IdTorre)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                if (IdTorre == ConstantesAplicacion.guidNulo)
                    IdTorre = objDTO.IdTorresEditar;

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
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion


        #region EliminarTorres

        [HttpPost]
        public async Task<ActionResult> EliminarTorres(Guid IdTorresEditar, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrearEditar.consumoAPI(ConstantesConsumoAPI.TorresPorIDEliminar + IdTorresEditar, HttpMethod.Post);

                if (respuesta.IsSuccessStatusCode)

                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "", "", true));
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
        public IActionResult AdministrarTorres()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
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

        [HttpGet]
        public async Task<JsonResult> BusquedaPorTorresIDConjunto(Guid IdConjunto)
        {
            List<ObjetoSelectDropDown> listaSelect = new List<ObjetoSelectDropDown>();

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresPorConjunto + IdConjunto, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
               var listaRespuesta = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                listaSelect = listaRespuesta.Select(x => new ObjetoSelectDropDown { id = x.IdTorres.ToString(), texto = x.NombreTorres }).ToList();
            }                

            if (listaSelect == null)
                listaSelect = new List<ObjetoSelectDropDown>();

            return new JsonResult(listaSelect);
        }




    }
}
