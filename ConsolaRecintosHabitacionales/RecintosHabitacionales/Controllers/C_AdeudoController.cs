using DTOs.Adeudo;
using DTOs.Proveedor;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_AdeudoController : Controller
    {
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusqueda;

        public C_AdeudoController(IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusqueda)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
        }

        public IActionResult GestionarAdeudo()
        {
            return View();
        }

        public IActionResult GenearAdeudo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<int> listaAnios = obtenerAnios().ToList();

                ViewData["listaAnios"] = listaAnios;
              
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;            

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        [HttpPost]
        public async Task<ActionResult> GenearAdeudo(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                if (variable.IdConjunto!=null)
                {

                    BusquedaTorres objBusquedaTorres = new BusquedaTorres();
                    objBusquedaTorres.IdConjunto = (Guid) variable.IdConjunto;

                    List<TorreDTOCompleto> listaResultado = new List<TorreDTOCompleto>();

                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                        foreach (TorreDTOCompleto torre in listaResultado)
                        {
                            foreach (var departamento in torre.Departamentos)
                            {
                                AdeudoDTOCrear objAduedo = new AdeudoDTOCrear();

                                objAduedo.IdDepartamento = departamento.IdDepartamento;
                                objAduedo.MontoAdeudos = departamento.AliqDepartamento;
                                //objAduedo.FechaAdeudos = departamento.IdFecha;

                            }
                        }
                    }
                       
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        public static IEnumerable<int> obtenerAnios()
        {
            int currentYear = DateTime.Now.Year;
            while (currentYear >= 2023)
            {
                yield return currentYear;
                currentYear--;
            }
        }

    }
}
