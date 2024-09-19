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
using DTOs.Contabilidad;
using DTOs.Departamento;

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
            else
            {
                listaResultado = listaResultado.OrderBy(x => x.NombreTorres).ToList();

                TorreDTOCompleto torreTodos = new TorreDTOCompleto();
                torreTodos.IdTorres = Guid.Empty;
                torreTodos.NombreTorres = "Todas";

                listaResultado.Insert(0, torreTodos);
            }

            SelectList listSelecTorres = new SelectList(listaResultado, "IdTorres", "NombreTorres");

            ViewData["listaTorres"] = listSelecTorres;

            List<DepartamentoDTOCompleto> listaDepartamentosTodos = new List<DepartamentoDTOCompleto>();

            DepartamentoDTOCompleto objTemporalDepar = new DepartamentoDTOCompleto();
            objTemporalDepar.CodigoDepartamento = "Todos";

            listaDepartamentosTodos.Add(objTemporalDepar);

            SelectList listSelecDepartamento = new SelectList(listaDepartamentosTodos, "IdDepartamento", "CodigoDepartamento");

            ViewData["listaDepartamento"] = listSelecDepartamento;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BuscarEstadoCuenta(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            List<AdeudoDTOCompleto> listaResultado = await recuperarListaAdeudos(variable);

            listaResultado = listaResultado.OrderBy(x => x.FechaAdeudos)
                .ThenBy(x => x.Torre)
                .ThenBy(x => x.Departamento)
                .ToList();

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

            HttpResponseMessage tipoPagoBanco = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.tipoBancoFormaPago, HttpMethod.Get);

            var listaTipoBanco = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(tipoPagoBanco);

            listaTipoBanco = listaTipoBanco.OrderBy(x => x.NombreCatalogo).ToList();

            var selectTipoPago = new SelectList(listaTipoBanco, "IdCatalogo", "NombreCatalogo");

            ViewData["listaTipoPagoBanco"] = selectTipoPago;

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

                decimal valorDeudaActual = Convert.ToDecimal("0.00");

                if (objAdeudo.SaldoPendiente == 0)
                    valorDeudaActual = objAdeudo.MontoAdeudos;
                else
                    valorDeudaActual = objAdeudo.SaldoPendiente;

                valorRestantePagado = valorRestantePagado - valorDeudaActual;

                if (valorRestantePagado < 0)
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


                HttpResponseMessage respuestaEditar = await _servicioConsumoAPIAdeudoDTOPagar.consumoAPI(ConstantesConsumoAPI.gestionarEditarAdeudoPago + objAdeudo.IdAdeudos, HttpMethod.Post, objAdeudoEditar);

                if (respuestaEditar.IsSuccessStatusCode)
                {

                }
            }

            objComprobante.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);


            objComprobante.UrlConsumaTablaDeuda = ConstantesConsumoAPI.gestionarAdeudoAPI;
            objComprobante.EstadoImpreso = false;
            objComprobante.IdBancoComprobante = objComprobante.IdBancoComprobante;
            objComprobante.SaldoPendiente = valorRestantePagado < 0 ? valorRestantePagado * -1 : valorRestantePagado;
            objComprobante.UsuarioCreacion = objUsuarioSesion.NombreUsuario;
            objComprobante.UsuarioModificacion = objUsuarioSesion.NombreUsuario;

            //Recuperar Secuencial
            HttpResponseMessage respuestaSecuencial = await _servicioConsumoAPIAdeudoDTOPagar.consumoAPI(ConstantesConsumoAPI.GetSecuencialComprobantePago, HttpMethod.Get);

            var secuencialMaximo = await LeerRespuestas<string>.procesarRespuestasConsultas(respuestaSecuencial);
            int nuevoSecuencial = Convert.ToInt32(secuencialMaximo) + 1;

            SecuencialComprobantePagoDTO objSecuencial = new SecuencialComprobantePagoDTO();
            objSecuencial.SecuencialComprobante = nuevoSecuencial;

            objComprobante.SecuencialComprobantePagos = new List<SecuencialComprobantePagoDTO>();
            objComprobante.SecuencialComprobantePagos.Add(objSecuencial);

            HttpResponseMessage respuestaComprobante = await _servicioConsumoAPIComprobante.consumoAPI(ConstantesConsumoAPI.ComprobantePago, HttpMethod.Post, objComprobante);

            if (respuestaComprobante.IsSuccessStatusCode)
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuestaComprobante, controladorActual, accionActual));
            else
            {
                MensajesRespuesta objMensajeRespuesta = await respuestaComprobante.ExceptionResponse();
                return new JsonResult(objMensajeRespuesta);
            }
        }

        public async Task<IActionResult> PDF_ReciboAdeudo(Guid IdAdeudos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuesta = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.GetComprobanteByIDDetalle + IdAdeudos, HttpMethod.Get);

            ComprobantePagoDTOCompleto objComprobante = await LeerRespuestas<ComprobantePagoDTOCompleto>.procesarRespuestasConsultas(respuesta);

            //Recuperar texto Forma Pago
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + objComprobante.IdTipoPago, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);

            objComprobante.TipoPago = objCatalogo.NombreCatalogo;

            //Recuperar texto Banco, en donde se va a colocar el dinero 
            HttpResponseMessage respuestaBancoPago = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + objComprobante.IdBancoComprobante, HttpMethod.Get);

            var objCatalogoBancoPago = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaBancoPago);

            objComprobante.NombreBancoComprobante = objCatalogoBancoPago.NombreCatalogo;

            //Recuperar datos Adeudo

            foreach (var detalle in objComprobante.DetalleComprobantePagos)
            {
                HttpResponseMessage respuestaAdeudo = await _servicioConsumoBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI + detalle.IdTablaDeuda, HttpMethod.Get);

                AdeudoDTOCompleto objAdeudo = await LeerRespuestas<AdeudoDTOCompleto>.procesarRespuestasConsultas(respuestaAdeudo);

                detalle.NombrePersona = objAdeudo.Nombre + " " + objAdeudo.Apellido;

                objComprobante.Conjunto = objAdeudo.NombreConjunto;
                objComprobante.Torre = objAdeudo.Torre;
                objComprobante.Departamento = objAdeudo.Departamento;
            }

            return new ViewAsPdf("PDF_ReciboAdeudo", objComprobante)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal,
            };
        }

    }
}
