using AutoMapper;
using DTOs.AreaComunal;
using DTOs.CatalogoGeneral;
using DTOs.ReservaArea;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ReservaAreas : Controller
    {
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IMapper _mapper;
        private readonly IServicioConsumoAPI<ReservaAreaDTOEditar> _servicioConsumoAPIEditar;
        public C_ReservaAreas(IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IMapper mapper, IServicioConsumoAPI<ReservaAreaDTOEditar> servicioConsumoAPIEditar)
        {
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _mapper = mapper;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
        }

        // GET: C_ReservaAreas
        public async Task<ActionResult> ReservaAreasComunales()
        {

            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            
            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            if(objUsuarioSesion==null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarAreasComunalesPorIdConjunto + objUsuarioSesion.IdConjuntoDefault, HttpMethod.Get);

            List<AreaComunalDTOCompleto> listAreaComunal = new();

            if (respuesta.IsSuccessStatusCode)
                listAreaComunal = await LeerRespuestas<List<AreaComunalDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if(listAreaComunal.Count() > 0) 
            {
                ViewData["listaAreasComunales"] = new SelectList(listAreaComunal, "IdAreaComunal", "NombreArea");
            }
            else 
            {
                
                ViewData["listaAreasComunales"] = "";
            }
            ViewData["ListaConjustosAcceso"] = objUsuarioSesion.ConjutosAccesoSelect;


            return View();
        }

        public async Task<ActionResult> ReservaAreasComunales1()
        {

            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarAreasComunalesPorIdConjunto + objUsuarioSesion.IdConjuntoDefault, HttpMethod.Get);

            List<AreaComunalDTOCompleto> listAreaComunal = new();

            if (respuesta.IsSuccessStatusCode)
                listAreaComunal = await LeerRespuestas<List<AreaComunalDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listAreaComunal.Count() > 0)
            {
                ViewData["listaAreasComunales"] = new SelectList(listAreaComunal, "IdAreaComunal", "NombreArea");
            }
            else
            {

                ViewData["listaAreasComunales"] = "";
            }
            ViewData["ListaConjustosAcceso"] = objUsuarioSesion.ConjutosAccesoSelect;


            return View();
        }



        public async Task<JsonResult> recuperarAreasComunalesPorIdConjunto(Guid idConjunto)
        {
            List<AreaComunalDTOCompleto> listaResultadoDTO = new();
            HttpResponseMessage respuestaAreaComunal = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarAreasComunalesPorIdConjunto + idConjunto, HttpMethod.Get);

            listaResultadoDTO = await LeerRespuestas<List<AreaComunalDTOCompleto>>.procesarRespuestasConsultas(respuestaAreaComunal);
            return new JsonResult(listaResultadoDTO);
        }

        public async Task<JsonResult> obtenerReserva(Guid idAreaComunal)
        {
            try
            {
                List<ReservaAreaDTOCompleto> listaResultadoDTO = new();
                HttpResponseMessage respuestaAreaComunal = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarReservaAreaPorIdComunal + idAreaComunal, HttpMethod.Get);
                if (respuestaAreaComunal.IsSuccessStatusCode)
                    listaResultadoDTO = await LeerRespuestas<List<ReservaAreaDTOCompleto>>.procesarRespuestasConsultas(respuestaAreaComunal);


                // return new JsonResult(listaResultadoDTO);

                var json = Json(new
                {
                    ListTasksCalendar = listaResultadoDTO,
                    IsSuccessful = true
                });
                return json;
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }

        public async Task<JsonResult> obtenerReservaPorId(Guid id)
        {
            try
            {
                ReservaAreaDTOCompleto listaResultadoDTO = new();
                HttpResponseMessage respuestaAreaComunal = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.BuscarReservaAreaPorID + id, HttpMethod.Get);
                if (respuestaAreaComunal.IsSuccessStatusCode)
                    listaResultadoDTO = await LeerRespuestas<ReservaAreaDTOCompleto>.procesarRespuestasConsultas(respuestaAreaComunal);


                // return new JsonResult(listaResultadoDTO);

                var json = Json(new
                {
                    Task = listaResultadoDTO,
                    IsSuccessful = true
                });
                return json;
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    IsSuccessful = false,
                    Errors = new List<string> { ex.Message }
                });
            }
        }




    }
}
