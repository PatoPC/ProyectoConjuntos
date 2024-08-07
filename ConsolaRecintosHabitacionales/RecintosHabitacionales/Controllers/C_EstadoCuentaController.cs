using DocumentFormat.OpenXml.Drawing;
using DTOs.Adeudo;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Rotativa.AspNetCore;
using Rotativa;
using Utilitarios;
using DTOs.Torre;

namespace RecintosHabitacionales.Controllers
{
    public class C_EstadoCuentaController : Controller
    {
        private readonly IServicioConsumoAPI<GenerarAdeudo> _servicioConsumoBusqueda;
        private readonly IServicioConsumoAPI<AdeudoDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;

        public C_EstadoCuentaController(IServicioConsumoAPI<GenerarAdeudo> servicioConsumoBusqueda, IServicioConsumoAPI<AdeudoDTOEditar> servicioConsumoAPIEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres)
        {
            _servicioConsumoBusqueda = servicioConsumoBusqueda;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
            _servicioConsumoAPIBusquedaTorres = servicioConsumoAPIBusquedaTorres;
        }

        string controladorActual = "C_EstadoCuenta";
        string accionActual = "AdministracionEstadoCuenta";

        public async Task<ActionResult> AdministracionEstadoCuenta()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            List<int> listaAnios = FuncionesUtiles.obtenerAnios().ToList();

            ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;
            ViewData["listaAnios"] = listaAnios;


            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = objUsuarioSesion.IdConjuntoDefault;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaTorres.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            List<TorreDTOCompleto> listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<TorreDTOCompleto>();

            SelectList listSelecTorres = new SelectList(listaResultado, "IdTorres", "NombreConjunto");

            ViewData["listaTorres"] = listSelecTorres;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BuscarEstadoCuenta(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");
            
            List<AdeudoDTOCompleto> listaResultado = await recuperarListaAdeudos(variable);

            listaResultado = listaResultado.OrderBy(x => x.FechaAdeudos).ToList();

            return View("_ListaEstadoCuenta", listaResultado);
        }

        private async Task<List<AdeudoDTOCompleto>> recuperarListaAdeudos(GenerarAdeudo variable)
        {
            List<AdeudoDTOCompleto> listaResultado = new List<AdeudoDTOCompleto>();

            try
            {
                DateTime fechaADeudoActual = FuncionesUtiles.ObtenerPrimerDiaDelMes(variable.mes, variable.anio);

                variable.fechaADeudoActual = fechaADeudoActual;
            }
            catch (Exception ex)
            {
                variable.fechaADeudoActual = DateTime.MinValue;
            }

            try
            {
                HttpResponseMessage respuesta = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.buscarAdeudoAvanzado, HttpMethod.Get, variable);

                if (respuesta.IsSuccessStatusCode)
                    listaResultado = await LeerRespuestas<List<AdeudoDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            }
            catch (Exception ex)
            {

            }

            if (listaResultado == null)
                listaResultado = new List<AdeudoDTOCompleto>();

            return listaResultado;
        }

        public async Task<ActionResult> GenerarRecibo(Guid IdAdeudos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");


            HttpResponseMessage respuestaCatalogo = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.tipoFormaPago, HttpMethod.Get);

            var listaCatalogos = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuestaCatalogo);

            var selectFormasPago = new SelectList(listaCatalogos, "IdCatalogo", "NombreCatalogo");

            ViewData["listaFormasPago"] = selectFormasPago;


            HttpResponseMessage respuesta = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI + IdAdeudos, HttpMethod.Get);

            AdeudoDTOCompleto objAdeudo = await LeerRespuestas<AdeudoDTOCompleto>.procesarRespuestasConsultas(respuesta);

            GenerarAdeudo objBusquedaAdeudos = new GenerarAdeudo();

            objBusquedaAdeudos.IdDepartamento = objAdeudo.IdDepartamento;
            objBusquedaAdeudos.tipoGeneracion = 2;

            List<AdeudoDTOCompleto> listaResultado = await recuperarListaAdeudos(objBusquedaAdeudos);


            return View(objAdeudo);
        }

        [HttpPost]
        public async Task<ActionResult> GenerarRecibo(Guid IdAdeudos, AdeudoDTOEditar objEstadoCuenta)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            objEstadoCuenta.FechaPago = DateTime.Now;
            objEstadoCuenta.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

            var deudaTotal = objEstadoCuenta.MontoAdeudos + objEstadoCuenta.SaldoPendiente;

            var nuevoSaldoPendiente = deudaTotal - objEstadoCuenta.valorPagar;

            objEstadoCuenta.SaldoPendiente = nuevoSaldoPendiente;

            PagoAdeudoDTOCompleto objPagoAdeudo = new PagoAdeudoDTOCompleto();

            objPagoAdeudo.FechaCreacion = objEstadoCuenta.FechaPago;
            objPagoAdeudo.FechaModificacion = objEstadoCuenta.FechaPago;
            objPagoAdeudo.UsuarioCreacion = objEstadoCuenta.UsuarioModificacion;
            objPagoAdeudo.FechaPago = objEstadoCuenta.FechaPago;
            objPagoAdeudo.IdTipoPago = objEstadoCuenta.IdFormapago;
            objPagoAdeudo.UsuarioModificacion = objEstadoCuenta.UsuarioModificacion;
            objPagoAdeudo.ValorPago = objEstadoCuenta.valorPagar;
            objPagoAdeudo.SaldoPendiente = objEstadoCuenta.SaldoPendiente;
            objPagoAdeudo.Observacion = objEstadoCuenta.Observacion;
            objPagoAdeudo.EstadoImpreso = false;

            objEstadoCuenta.PagoAdeudos = new List<PagoAdeudoDTOCompleto>();

            objEstadoCuenta.PagoAdeudos.Add(objPagoAdeudo);

            if (objPagoAdeudo.SaldoPendiente == 0)
                objEstadoCuenta.EstadoAdeudos = true;

            HttpResponseMessage respuesta = await _servicioConsumoAPIEditar.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoEditar + IdAdeudos, HttpMethod.Post, objEstadoCuenta);

            if (respuesta.IsSuccessStatusCode)
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));            
            else
            {
                MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                return new JsonResult(objMensajeRespuesta);
            }
        }

        public async Task<IActionResult> PDF_ReciboAdeudo(Guid IdAdeudos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                HttpResponseMessage respuesta = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI + IdAdeudos, HttpMethod.Get);

                AdeudoDTOCompleto objAdeudo = await LeerRespuestas<AdeudoDTOCompleto>.procesarRespuestasConsultas(respuesta);
                
                foreach (var pago in objAdeudo.PagoAdeudos)
                {
                    HttpResponseMessage respuestaCatalogo = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + pago.IdTipoPago, HttpMethod.Get);

                    var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);

                    pago.NombreTipoPago = objCatalogo.NombreCatalogo;
                }

                return new ViewAsPdf("PDF_ReciboAdeudo", objAdeudo)
                {
                    PageSize = Rotativa.AspNetCore.Options.Size.Legal,
                };
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

    }
}
