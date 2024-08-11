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
using DTOs.Comprobantes;

namespace RecintosHabitacionales.Controllers
{
    public class C_EstadoCuentaController : Controller
    {
        private readonly IServicioConsumoAPI<GenerarAdeudo> _servicioConsumoBusqueda;
        private readonly IServicioConsumoAPI<AdeudoDTOEditar> _servicioConsumoAPIEditar;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;
        private readonly IServicioConsumoAPI<AdeudoDTOPagar> _servicioConsumoAPIAdeudoDTOPagar;
        private readonly IServicioConsumoAPI<ComprobantePagoDTOCompleto> _servicioConsumoAPIComprobante;

        public C_EstadoCuentaController(IServicioConsumoAPI<GenerarAdeudo> servicioConsumoBusqueda, IServicioConsumoAPI<AdeudoDTOEditar> servicioConsumoAPIEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres, IServicioConsumoAPI<AdeudoDTOPagar> servicioConsumoAPIAdeudoDTOPagar, IServicioConsumoAPI<ComprobantePagoDTOCompleto> servicioConsumoAPIComprobante)
        {
            _servicioConsumoBusqueda = servicioConsumoBusqueda;
            _servicioConsumoAPIEditar = servicioConsumoAPIEditar;
            _servicioConsumoAPIBusquedaTorres = servicioConsumoAPIBusquedaTorres;
            _servicioConsumoAPIAdeudoDTOPagar = servicioConsumoAPIAdeudoDTOPagar;
            _servicioConsumoAPIComprobante = servicioConsumoAPIComprobante;
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

            SelectList listSelecTorres = new SelectList(listaResultado, "IdTorres", "NombreTorres");

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

            listaResultado = listaResultado.OrderBy(x => x.FechaAdeudos).ToList();

            return View(listaResultado);
        }

     
            [HttpPost]
        public async Task<ActionResult> GenerarRecibo(ComprobantePagoDTOCompleto objComprobante)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            decimal valorRestantePagado = objComprobante.ValorPago;

            foreach (var objDetalle in objComprobante.DetalleComprobantePagos)
            {
                AdeudoDTOPagar objAdeudoEditar = new AdeudoDTOPagar();

                HttpResponseMessage respuestaTemp = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI + objDetalle.IdTablaDeuda, HttpMethod.Get);

                AdeudoDTOCompleto objAdeudo = await LeerRespuestas<AdeudoDTOCompleto>.procesarRespuestasConsultas(respuestaTemp);

                decimal valorDeudaActual = objAdeudo.MontoAdeudos;

                valorRestantePagado = valorRestantePagado - valorDeudaActual;

                if(valorRestantePagado < 0)
                {
                    objAdeudoEditar.SaldoPendiente = valorRestantePagado * -1;
                    objAdeudoEditar.EstadoAdeudos = false;
                }
                else
                {
                    objAdeudoEditar.EstadoAdeudos = true;
                    objAdeudoEditar.SaldoPendiente = 0;
                }

                objAdeudoEditar.FechaModificacion = DateTime.Now;
                objAdeudoEditar.UsuarioModificacion = objUsuarioSesion.NombreUsuario;
                objAdeudoEditar.IdFormapago = objComprobante.IdTipoPago;

                HttpResponseMessage respuestaEditar = await _servicioConsumoAPIAdeudoDTOPagar.consumoAPI(ConstantesConsumoAPI.gestionarEditarAdeudoPago + objAdeudo.IdAdeudos, HttpMethod.Post, objAdeudoEditar);

                if (respuestaEditar.IsSuccessStatusCode)
                {

                }
            }

            objComprobante.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);


            HttpResponseMessage respuesta = await _servicioConsumoAPIAdeudoDTOPagar.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.ingresoAdeudos, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);
            
            objComprobante.IdTipoPago = objCatalogo.IdCatalogo;
            objComprobante.UrlConsumaTablaDeuda = ConstantesConsumoAPI.gestionarAdeudoAPI;
            objComprobante.EstadoImpreso = false;
            objComprobante.SaldoPendiente = valorRestantePagado < 0 ? valorRestantePagado * -1 : valorRestantePagado;
            objComprobante.UsuarioCreacion = objUsuarioSesion.NombreUsuario;
            objComprobante.UsuarioModificacion = objUsuarioSesion.NombreUsuario;

          
            HttpResponseMessage respuestaComprobante = await _servicioConsumoAPIComprobante.consumoAPI(ConstantesConsumoAPI.ComprobantePago, HttpMethod.Post, objComprobante);

            if (respuestaComprobante.IsSuccessStatusCode)
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

                    //pago.NombreTipoPago = objCatalogo.NombreCatalogo;
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
