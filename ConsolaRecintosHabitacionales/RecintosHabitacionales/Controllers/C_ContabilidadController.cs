using DTOs.CatalogoGeneral;
using DTOs.Contabilidad;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ContabilidadController : Controller
    {
        private readonly IServicioConsumoAPI<BusquedaContabilidad> _servicioConsumoAPI;

        public C_ContabilidadController(IServicioConsumoAPI<BusquedaContabilidad> servicioConsumoAPI)
        {
            _servicioConsumoAPI = servicioConsumoAPI;
        }

        public IActionResult Comprobantes()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

            return View();
        }

        public async Task<ActionResult> BusquedaAvanzadaContabilidad(BusquedaContabilidad objBusqueda)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.GetBusquedaAvanzadaContabilidad, HttpMethod.Get, objBusqueda);

            List<EncabezContDTOCompleto> listaEncabezado = await LeerRespuestas<List<EncabezContDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            return View("_ListaEncabezado", listaEncabezado);
        }
    }
}
