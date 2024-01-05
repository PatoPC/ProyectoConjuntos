using DTOs.CatalogoGeneral;
using DTOs.Comunicado;
using DTOs.Conjunto;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using System.Diagnostics;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServicioConsumoAPI<BusquedaComunicadoDTO> _servicioConsumoAPIBusqueda;

        public HomeController(ILogger<HomeController> logger, IServicioConsumoAPI<BusquedaComunicadoDTO> servicioConsumoAPI)
        {
            _logger = logger;
            _servicioConsumoAPIBusqueda = servicioConsumoAPI;
        }

        public async Task<ActionResult> Index()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                BusquedaComunicadoDTO objBusquedaComunicado = new BusquedaComunicadoDTO();

                objBusquedaComunicado.IdConjunto = objUsuarioSesion.IdConjuntoDefault;
                List<ComunicadoDTOCompleto> listaResultado = new();
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.BuscarComunicadoAvanzado, HttpMethod.Get, objBusquedaComunicado);
                if (respuesta.IsSuccessStatusCode)
                    listaResultado = await LeerRespuestas<List<ComunicadoDTOCompleto>>.procesarRespuestasConsultas(respuesta);
                var listaFinal = listaResultado.Where(x => x.DiasParaCaducar >= 0).ToList();

                listaFinal = await completarObjetoComunicado(listaFinal);                

                ViewData["listaComunicados"] = listaFinal;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        private async Task<List<ComunicadoDTOCompleto>> completarObjetoComunicado(List<ComunicadoDTOCompleto> listaResultadoDTO)
        {
            if (listaResultadoDTO != null)
            {
                foreach (ComunicadoDTOCompleto dato in listaResultadoDTO)
                {

                    await completarObjetoComunicado(dato);
                }
            }

            return listaResultadoDTO;
        }

        private async Task<ComunicadoDTOCompleto> completarObjetoComunicado(ComunicadoDTOCompleto obj)
        {
            if (obj != null)
            {
                HttpResponseMessage respuestaConjunto = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + obj.IdConjunto, HttpMethod.Get);
                if (respuestaConjunto.IsSuccessStatusCode)
                {
                    ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuestaConjunto);
                    obj.NombreConjunto = objDTO.NombreConjunto;
                }
            }

            return obj;
        }
       

    }
}