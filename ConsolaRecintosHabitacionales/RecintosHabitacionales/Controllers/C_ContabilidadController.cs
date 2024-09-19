using DTOs.CatalogoGeneral;
using DTOs.ConfiguracionCuenta;
using DTOs.Contabilidad;
using DTOs.MaestroContable;
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

        public async Task<JsonResult> DetalleComprobantesCabecera(Guid IdEncCont)
		{
			var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

			HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.CabeceraContabilidad+ IdEncCont, HttpMethod.Get);

			EncabezContDTOCompleto objCabecera = await LeerRespuestas<EncabezContDTOCompleto>.procesarRespuestasConsultas(respuesta);

            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

            HttpResponseMessage respuestaConfigurar = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.buscarConfiguracion + objCabecera.IdConjunto, HttpMethod.Get);

            if (respuestaConfigurar.IsSuccessStatusCode)
                objConfigurar = await LeerRespuestas<ConfiguraCuentasDTOCompleto>.procesarRespuestasConsultas(respuestaConfigurar);


            foreach (var detalle in objCabecera.DetalleContabilidads)
            {
                HttpResponseMessage respuestaCuentaContableAdeudos = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + detalle.IdCuentaContable, HttpMethod.Get);

                MaestroContableDTOCompleto objCuentaAdeudo = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuestaCuentaContableAdeudos);

                detalle.CuentaContable = objCuentaAdeudo.CuentaCon;

                //Se comenta porque no se va a guardar formateado y separado por puntos
                string cuentaActual = FuncionesUtiles.FormatearCadenaCuenta(objCuentaAdeudo.CuentaCon, objConfigurar.Parametrizacion);

                detalle.CuentaContable = cuentaActual;
                detalle.NombreCuentaContable = objCuentaAdeudo.NombreCuenta;
            }

            objCabecera.DetalleContabilidads = objCabecera.DetalleContabilidads.OrderBy(x => x.DetalleDetCont).ToList();

            return new JsonResult(objCabecera);
        }

	}
}
