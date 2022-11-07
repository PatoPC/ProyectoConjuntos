using DTOs.Conjunto;
using DTOs.Departamento;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ConjuntosController : Controller
    {
        private const string controladorActual = "C_Conjuntos";
        private const string accionActual = "AdministrarConjuntos";

        private readonly IServicioConsumoAPI<ConjuntoDTOCrear> _servicioConsumoAPIConjunto;
        private readonly IServicioConsumoAPI<ConjuntoDTOEditar> _servicioConsumoAPIConjuntoEditar;
        private readonly IServicioConsumoAPI<BusquedaConjuntos> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;

        private readonly IServicioConsumoAPI<DepartamentoDTOEditar> _servicioConsumoAPIDepartamentoEditar;

        private readonly IServicioConsumoAPI<DepartamentoDTOCrear> _servicioConsumoAPIDepartamento;

        public C_ConjuntosController(IServicioConsumoAPI<ConjuntoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<BusquedaConjuntos> servicioConsumoAPIBusqueda, IServicioConsumoAPI<ConjuntoDTOEditar> servicioConsumoAPIConjuntoEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres, IServicioConsumoAPI<DepartamentoDTOCrear> servicioConsumoAPIDepartamento, IServicioConsumoAPI<DepartamentoDTOEditar> servicioConsumoAPIDepartamentoEditar)
        {
            _servicioConsumoAPIConjunto = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPIConjuntoEditar = servicioConsumoAPIConjuntoEditar;
            _servicioConsumoAPIBusquedaTorres = servicioConsumoAPIBusquedaTorres;
            _servicioConsumoAPIDepartamento = servicioConsumoAPIDepartamento;
            _servicioConsumoAPIDepartamentoEditar = servicioConsumoAPIDepartamentoEditar;
        }

        #region CRUD

        #region CrearConjuntos
        public IActionResult CrearConjuntos()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CrearConjuntos(ConjuntoDTOCrear objDTO)
        {
            objDTO.UsuarioCreacion = "prueba";

            HttpResponseMessage respuesta = await _servicioConsumoAPIConjunto.consumoAPI(ConstantesConsumoAPI.crearConjuto, HttpMethod.Post, objDTO);

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
        
        #region EditarConjuntos
        public async Task<ActionResult> EditarConjuntos(Guid idConjutos)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID+ idConjutos, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                return View(objDTO);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditarConjuntos(ConjuntoDTOEditar objDTO, Guid IdConjunto)
        {
            objDTO.UsuarioModificacion = "prueba";

            HttpResponseMessage respuesta = await _servicioConsumoAPIConjuntoEditar.consumoAPI(ConstantesConsumoAPI.EditarConjuntosPorID+ IdConjunto, HttpMethod.Post, objDTO);

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

         
        #region EditarConjuntos
        public async Task<ActionResult> EliminarConjuntos(Guid idConjutos)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID+ idConjutos, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                return View(objDTO);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EliminarConjuntos(Guid IdConjunto, bool eliminar)
        {
           
            HttpResponseMessage respuesta = await _servicioConsumoAPIConjuntoEditar.consumoAPI(ConstantesConsumoAPI.EditarConjuntosEliminar + IdConjunto, HttpMethod.Post);

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

        #region CrearDepartamento
        [HttpPost]
        public async Task<ActionResult> CrearDepartamento(DepartamentoDTOCrear objDTO)
        {
            objDTO.UsuarioCreacion = "prueba";

            HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamento.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI, HttpMethod.Post, objDTO);

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

     
        [HttpGet]
        public IActionResult AdministrarConjuntos()
        {
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{
              

                return View();
            //}

            //return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaConjuntos(BusquedaConjuntos objBusquedaConjuntos)
        {
            List<ResultadoBusquedaConjuntos> listaResultado = new List<ResultadoBusquedaConjuntos>();
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{

            try
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosAvanzado, HttpMethod.Get, objBusquedaConjuntos);

                if (respuesta.IsSuccessStatusCode)
                    listaResultado = await LeerRespuestas<List<ResultadoBusquedaConjuntos>>.procesarRespuestasConsultas(respuesta);
            }
            catch (Exception ex)
            {

            }

            if (listaResultado == null)
                listaResultado = new List<ResultadoBusquedaConjuntos>();

            return View("_ListaConjutos", listaResultado);
        }

        public async Task<IActionResult> RecuperarListaTorresPorConjutoID(Guid idConjuto)
        {
            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = idConjuto;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaTorres.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            List<TorreDTOCompleto> listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<TorreDTOCompleto>();

            if (listaResultado != null)
            {
                return View("Torre/_ListaTorres", listaResultado);
            }

            return View("Torre/_ListaTorres", new List<TorreDTOCompleto>());
        }

    }
}
