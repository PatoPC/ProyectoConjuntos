using DTOs.Departamento;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_DepartamentoController : Controller
    {
        private const string controladorActual = "C_Departamento";
        private const string accionActual = "AdministrarDepartamento";

        private readonly IServicioConsumoAPI<DepartamentoDTOCrear> _servicioConsumoAPIDepartamento;
        private readonly IServicioConsumoAPI<DepartamentoDTOEditar> _servicioConsumoAPIDepartamentoEditar;
        private readonly IServicioConsumoAPI<DepartamentoBusquedaDTO> _servicioConsumoAPIBusqueda;

        public C_DepartamentoController(IServicioConsumoAPI<DepartamentoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<DepartamentoBusquedaDTO> servicioConsumoAPIBusqueda, IServicioConsumoAPI<DepartamentoDTOEditar> servicioConsumoAPIConjuntoEditar)
        {
            _servicioConsumoAPIDepartamento = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPIDepartamentoEditar = servicioConsumoAPIConjuntoEditar;
        }

        #region CRUD

        
        
        #region EditarDepartamento
        public async Task<ActionResult> EditarDepartamento(Guid idConjutos)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI + idConjutos, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                DepartamentoDTOCompleto objDTO = await LeerRespuestas<DepartamentoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                return View(objDTO);
            }

            return View();
        }

        #region EditarDepartamento
        [HttpPost]
        public async Task<ActionResult> EditarDepartamento(DepartamentoDTOEditar objDTO, Guid IdDepartamento)
        {
            objDTO.UsuarioModificacion = "prueba";

            if (IdDepartamento == ConstantesAplicacion.guidNulo)
            {
                IdDepartamento = objDTO.IdDeptoEditar;
            }

            HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamentoEditar.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPIEditar + IdDepartamento, HttpMethod.Post, objDTO);

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

        #endregion


        #region EliminarDepartamento      
        [HttpPost]
        public async Task<ActionResult> EliminarDepartamento(Guid IdDeptoEditar, bool eliminar)
        {
           
            HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamentoEditar.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPIEliminar + IdDeptoEditar, HttpMethod.Post);

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
        public IActionResult AdministrarDepartamento()
        {
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{
                return View();
            //}

            //return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaDepartamento(DepartamentoBusquedaDTO objBusquedaDepartamento)
        {
            List<DepartamentoDTOCompleto> listaResultado = new List<DepartamentoDTOCompleto>();
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarDepartamentoAvanzado, HttpMethod.Get, objBusquedaDepartamento);

            if (respuesta.IsSuccessStatusCode)
            {
                listaResultado = await LeerRespuestas<List<DepartamentoDTOCompleto>>.procesarRespuestasConsultas(respuesta);
            }

            if (listaResultado == null)
                listaResultado = new List<DepartamentoDTOCompleto>();

            return View("_ListaDepartamento", listaResultado);
        }

        
        [HttpGet]
        public async Task<JsonResult> BusquedaPorDepartamentoID(Guid IdDepartamento)
        {
            DepartamentoDTOCompleto objResultado = new DepartamentoDTOCompleto();
            //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            //if (objUsuarioSesion != null)
            //{

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI + IdDepartamento, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
                objResultado = await LeerRespuestas<DepartamentoDTOCompleto>.procesarRespuestasConsultas(respuesta);

            if (objResultado == null)
                objResultado = new DepartamentoDTOCompleto();

            return new JsonResult(objResultado);
        }
    }
}
