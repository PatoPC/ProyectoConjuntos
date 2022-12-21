using DTOs.CatalogoGeneral;
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

        #region Editar
        public async Task<ActionResult> EditarDepartamento(Guid idConjuntos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI + idConjuntos, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    DepartamentoDTOCompleto objDTO = await LeerRespuestas<DepartamentoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region EditarDepartamento
        [HttpPost]
        public async Task<ActionResult> EditarDepartamento(DepartamentoDTOEditar objDTO, Guid IdDepartamento)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                if (IdDepartamento == ConstantesAplicacion.guidNulo)
                    IdDepartamento = objDTO.IdDeptoEditar;


                HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamentoEditar.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPIEditar + IdDepartamento, HttpMethod.Post, objDTO);

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

        #endregion


        #region EliminarDepartamento      
        [HttpPost]
        public async Task<ActionResult> EliminarDepartamento(Guid IdDeptoEditar, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamentoEditar.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPIEliminar + IdDeptoEditar, HttpMethod.Post);

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
        public IActionResult AdministrarDepartamento()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaDepartamento(DepartamentoBusquedaDTO objBusquedaDepartamento)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<DepartamentoDTOCompleto> listaResultado = new List<DepartamentoDTOCompleto>();

                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarDepartamentoAvanzado, HttpMethod.Get, objBusquedaDepartamento);

                if (respuesta.IsSuccessStatusCode)                
                    listaResultado = await LeerRespuestas<List<DepartamentoDTOCompleto>>.procesarRespuestasConsultas(respuesta);
                

                if (listaResultado == null)
                    listaResultado = new List<DepartamentoDTOCompleto>();

                return View("_ListaDepartamento", listaResultado);

            }
            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpGet]
        public async Task<JsonResult> BusquedaPorDepartamentoID(Guid IdDepartamento)
        {
            DepartamentoDTOCompleto objResultado = new DepartamentoDTOCompleto();

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI + IdDepartamento, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                objResultado = await LeerRespuestas<DepartamentoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                if (objResultado!=null)
                {
                    if (objResultado.TipoPersonas!=null)
                    {
                        foreach (var item in objResultado.TipoPersonas)
                        {
                            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + item.IdTipoPersonaDepartamento, HttpMethod.Get);

                            CatalogoDTOCompleto objCatalogo = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaCatalogo);

                            item.TipoPersona = objCatalogo.Nombrecatalogo;
                        }  
                    }
                }

            }
                

            if (objResultado == null)
                objResultado = new DepartamentoDTOCompleto();

            return new JsonResult(objResultado);
        }

        [HttpGet]
        public async Task<JsonResult> BusquedaPorDepartamentoPorIDTorre(Guid IdTorre)
        {
            List<ObjetoSelectDropDown> listaSelect = new List<ObjetoSelectDropDown>();

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarDepartamentosPorIDTorre + IdTorre, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                var listaRespuesta = await LeerRespuestas<List<DepartamentoDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                listaSelect = listaRespuesta.Select(x => new ObjetoSelectDropDown { id = x.IdDepartamento.ToString(), texto = x.CodigoDepartamento }).ToList();
            }

            if (listaSelect == null)
                listaSelect = new List<ObjetoSelectDropDown>();

            return new JsonResult(listaSelect);
        }

    }
}
