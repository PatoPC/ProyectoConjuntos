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

                List<ComunicadoDTOCompleto> listaComunicados = await buscarComunicados(objBusquedaComunicado);

                

                ViewData["listaComunicados"] = listaComunicados;

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

        private async Task<List<ComunicadoDTOCompleto>> buscarComunicados(BusquedaComunicadoDTO objBusquedaComunicado)
        {
            List<ComunicadoDTOCompleto> listaResultadoFinal = new List<ComunicadoDTOCompleto>();

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.BuscarComunicadoAvanzado, HttpMethod.Get, objBusquedaComunicado);

            if (respuesta.IsSuccessStatusCode)
            {
                List<ComunicadoDTOCompleto> listaResultado = await LeerRespuestas<List<ComunicadoDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                var listaConjuntos = listaResultado.GroupBy(x => x.IdConjunto).ToList();

                foreach (var conjunto in listaConjuntos)
                {
                    HttpResponseMessage respuestaConjunto = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + conjunto.Key, HttpMethod.Get);

                    ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuestaConjunto);

                    if (respuestaConjunto.IsSuccessStatusCode)
                    {
                        if (objDTO != null)
                        {
                            List<ComunicadoDTOCompleto> listaTemporal = listaResultado.Where(x => x.IdConjunto == objDTO.IdConjunto)
                                                            .Select(y => new ComunicadoDTOCompleto
                                                            {
                                                                IdComunicado = y.IdComunicado,
                                                                IdConjunto = y.IdConjunto,
                                                                NombreConjunto = objDTO.NombreConjunto,
                                                                Titulo = y.Titulo,
                                                                Descripcion = FuncionesUtiles.ResumirString(y.Descripcion, 10, 1),
                                                                FechaCreacion = y.FechaCreacion,
                                                                FechaModificacion = y.FechaModificacion,
                                                                UsuarioCreacion = y.UsuarioCreacion,
                                                                UsuarioModificacion = y.UsuarioModificacion,
                                                            })
                                                            .ToList();

                            listaResultadoFinal = listaResultadoFinal.Union(listaTemporal).ToList();

                        }
                    }

                }
            }


            if (listaResultadoFinal == null)
                listaResultadoFinal = new List<ComunicadoDTOCompleto>();

            return listaResultadoFinal;
        }

    }
}