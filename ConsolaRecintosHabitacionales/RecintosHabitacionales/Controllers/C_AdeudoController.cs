
using AutoMapper;
using DTOs.Adeudo;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Contabilidad;
using DTOs.Departamento;
using DTOs.MaestroContable;
using DTOs.Parametro;
using DTOs.Persona;
using DTOs.Proveedor;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_AdeudoController : Controller
    {
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<GenerarAdeudo> _servicioConsumoBusqueda;
        private readonly IServicioConsumoAPI<EncabezContDTOCrear> _servicioConsumoEncabezado;
        private readonly IServicioConsumoAPI<List<AdeudoDTOCrear>> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<BusquedaParametro> _servicioConsumoAPIParametro;
        private readonly IServicioConsumoAPI<DepartamentoBusquedaDTO> _servicioConsumoAPIDepartamento;

        private readonly IMapper _mapper;
        public C_AdeudoController(IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusqueda, IServicioConsumoAPI<List<AdeudoDTOCrear>> servicioConsumoAPICrear, IServicioConsumoAPI<GenerarAdeudo> servicioConsumoBusqueda, IMapper mapper, IServicioConsumoAPI<EncabezContDTOCrear> servicioConsumoEncabezado, IServicioConsumoAPI<BusquedaParametro> servicioConsumoAPIParametro, IServicioConsumoAPI<DepartamentoBusquedaDTO> servicioConsumoAPIDepartamento)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoBusqueda = servicioConsumoBusqueda;
            _mapper = mapper;
            _servicioConsumoEncabezado = servicioConsumoEncabezado;
            _servicioConsumoAPIParametro = servicioConsumoAPIParametro;
            _servicioConsumoAPIDepartamento = servicioConsumoAPIDepartamento;
        }

        public async Task<ActionResult> GestionarAdeudo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<int> listaAnios = FuncionesUtiles.obtenerAnios().ToList();

                ViewData["listaAnios"] = listaAnios;

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                List<TorreDTOCompleto> listaTorres = await recuperarListaTorres(objUsuarioSesion.IdConjuntoDefault);

                var idTorre = listaTorres.FirstOrDefault().IdTorres;

                SelectList listSelecTorres = new SelectList(listaTorres, "IdTorres", "NombreTorres");

                ViewData["listaTorres"] = listSelecTorres;

                List<DepartamentoDTOCompleto> listaDepartamentosTodos = new List<DepartamentoDTOCompleto>();

                DepartamentoDTOCompleto objTemporalDepar = new DepartamentoDTOCompleto();
                objTemporalDepar.CodigoDepartamento = "Todos";

                listaDepartamentosTodos.Add(objTemporalDepar); 

                SelectList listSelecDepartamento = new SelectList(listaDepartamentosTodos, "IdDepartamento", "CodigoDepartamento");

                ViewData["listaDepartamento"] = listSelecDepartamento;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> BusquedaAvanzadaAdeudo(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            List<AdeudoDTOCompleto> listaResultado = await recuperarListaAdeudos(variable);

            listaResultado = listaResultado
                .OrderBy(x => x.Torre)
                .ThenBy(x => x.Departamento)
                .ToList();

            return View("_ListaAdeudos", listaResultado);
        }

        [HttpGet]
        public async Task<ActionResult> GenearAdeudo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<int> listaAnios = FuncionesUtiles.obtenerAnios().ToList();

                ViewData["listaAnios"] = listaAnios;

                ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

                List<TorreDTOCompleto> listaTorres = await recuperarListaTorres(objUsuarioSesion.IdConjuntoDefault);

                var idTorre = listaTorres.FirstOrDefault().IdTorres;

                SelectList listSelecTorres = new SelectList(listaTorres, "IdTorres", "NombreTorres", idTorre);

                ViewData["listaTorres"] = listSelecTorres;

                DepartamentoBusquedaDTO objBusquedaDepartamento = new DepartamentoBusquedaDTO(idTorre);

                List<DepartamentoDTOCompleto> listaResultadoDepartamento = new List<DepartamentoDTOCompleto>();

                HttpResponseMessage respuestaDepartamento = await _servicioConsumoAPIDepartamento.consumoAPI(ConstantesConsumoAPI.buscarDepartamentoAvanzado, HttpMethod.Get, objBusquedaDepartamento);

                if (respuestaDepartamento.IsSuccessStatusCode)
                {
                    listaResultadoDepartamento = await LeerRespuestas<List<DepartamentoDTOCompleto>>.procesarRespuestasConsultas(respuestaDepartamento);

                    listaResultadoDepartamento = listaResultadoDepartamento.OrderBy(x => x.CodigoDepartamento).ToList();

                    SelectList listSelecDepartamento = new SelectList(listaResultadoDepartamento, "IdDepartamento", "CodigoDepartamento");

                    ViewData["listaDepartamento"] = listSelecDepartamento;

                }
                else
                {
                    SelectList listSelecDepartamento = new SelectList(Enumerable.Empty<object>(), "IdDepartamento", "CodigoDepartamento");

                    ViewData["listaDepartamento"] = listSelecDepartamento;
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> GenearAdeudo(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            List<DetalleContabilidadCrear> listaDetallesHaber = new List<DetalleContabilidadCrear>();
            List<AdeudoDTOCrear> listaAdeudos = new List<AdeudoDTOCrear>();
            DateTime fechaADeudoActual = FuncionesUtiles.ObtenerPrimerDiaDelMes(variable.mes, variable.anio);
            List<string> adeudoDuplicados = new List<string>();

            if (variable.IdConjunto != null)
            {
                //Generación de todos los adeudos por mes
                if (variable.tipoGeneracion == 0)
                {
                    BusquedaTorres objBusquedaTorres = new BusquedaTorres();
                    objBusquedaTorres.IdConjunto = (Guid)variable.IdConjunto;

                    List<TorreDTOCompleto> listaResultado = new List<TorreDTOCompleto>();

                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

                    

                    if (respuesta.IsSuccessStatusCode)
                    {
                        listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                        foreach (TorreDTOCompleto torre in listaResultado)
                        {
                            foreach (var departamento in torre.Departamentos)
                            {
                                AdeudoDTOCrear objAdeudo = new AdeudoDTOCrear();

                                objAdeudo.IdDepartamento = departamento.IdDepartamento;
                                objAdeudo.Departamento = departamento.CodigoDepartamento;
                                objAdeudo.NombreConjunto = torre.NombreConjunto;
                                objAdeudo.MontoAdeudos = departamento.AliqDepartamento;
                                objAdeudo.EstadoAdeudos = false;
                                objAdeudo.Departamento = departamento.CodigoDepartamento;
                                objAdeudo.Torre = torre.NombreTorres;
                                objAdeudo.FechaAdeudos = fechaADeudoActual;
                                objAdeudo.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                                objAdeudo = await recuperarTipoPersonaDepartamento(departamento, objAdeudo);

                                listaAdeudos.Add(objAdeudo);
                            }
                        }

                    }
                }
                else
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI + variable.IdDepartamento, HttpMethod.Get);

                    DepartamentoDTOCompleto objDTODepartamento = await LeerRespuestas<DepartamentoDTOCompleto>.procesarRespuestasConsultas(respuesta);
                    //Generación individual
                    AdeudoDTOCrear objAdeudo = new AdeudoDTOCrear();

                    objAdeudo.IdDepartamento = objDTODepartamento.IdDepartamento;
                    objAdeudo.Departamento = objDTODepartamento.CodigoDepartamento;

                    objAdeudo.MontoAdeudos = objDTODepartamento.AliqDepartamento;
                    objAdeudo.EstadoAdeudos = false;
                    objAdeudo.Departamento = objDTODepartamento.CodigoDepartamento;
                    objAdeudo.FechaCreacion = DateTime.Now;

                    HttpResponseMessage respuestaTorre = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GestionarTorres + variable.IdTorre, HttpMethod.Get);

                    TorreDTOCompleto objDTOTorre = await LeerRespuestas<TorreDTOCompleto>.procesarRespuestasConsultas(respuestaTorre);

                    objAdeudo.NombreConjunto = objDTOTorre.NombreConjunto;
                    objAdeudo.Torre = objDTOTorre.NombreTorres;
                    objAdeudo.FechaAdeudos = fechaADeudoActual;
                    objAdeudo.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                    objAdeudo = await recuperarTipoPersonaDepartamento(objDTODepartamento, objAdeudo);

                    listaAdeudos.Add(objAdeudo);
                }

                //Carga de Adeudos
                if (listaAdeudos.Count > 0)
                {
                    List<AdeudoDTOCompleto> adeudosCreadosAntes = await recuperarListaAdeudos(variable);

                    List<AdeudoDTOCompleto> adeudosExistentes = new List<AdeudoDTOCompleto>();

                    //Gestión para evitar duplicados
                    foreach (AdeudoDTOCompleto adeudo in adeudosCreadosAntes)
                    {
                        AdeudoDTOCrear adeudoTemporal = listaAdeudos.Where(x => x.IdDepartamento == adeudo.IdDepartamento && x.IdPersona == adeudo.IdPersona && x.FechaAdeudos == adeudo.FechaAdeudos).FirstOrDefault();

                        if (adeudoTemporal != null)
                            adeudosExistentes.Add(adeudo);
                    }

                    if (adeudosExistentes.Count > 0)
                    {
                        foreach (var adeudoExistente in adeudosExistentes)
                        {
                            AdeudoDTOCrear adeudoTemporal = listaAdeudos.Where(x => x.IdDepartamento == adeudoExistente.IdDepartamento && x.IdPersona == adeudoExistente.IdPersona && x.FechaAdeudos == adeudoExistente.FechaAdeudos).FirstOrDefault();

                            if (adeudoTemporal != null)
                            {
                                string adeudoEliminado = adeudoTemporal.NombresPersona + " " + adeudoTemporal.ApellidosPersona + " Departamento: " + adeudoTemporal.Departamento;
                                adeudoDuplicados.Add(adeudoEliminado);
                            }


                            listaAdeudos.Remove(adeudoTemporal);
                        }
                    }
                    //Fin Gestión Duplicados

                    //Detalle Contabilidad 
                    HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.adeudoModulosContables, HttpMethod.Get);
                    CatalogoDTOResultadoBusqueda objCatalogoCuentaAdeudo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);

                    /*Cuando se Parametriza se configura por Modulo, aqui se recupera la parametrización para Adeudos*/
                    HttpResponseMessage respuestaParametro = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.obtenerParametroPorCatalogo + objCatalogoCuentaAdeudo.IdCatalogo, HttpMethod.Get);

                    ParametroCompletoDTO objMaestroContable = await LeerRespuestas<ParametroCompletoDTO>.procesarRespuestasConsultas(respuestaParametro);


                    HttpResponseMessage httpCuentaContableCon1 = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + objMaestroContable.CtaCont1, HttpMethod.Get);

                    MaestroContableDTOCompleto objCuentaCont1 = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(httpCuentaContableCon1);

                    foreach (var adeudo in listaAdeudos)
                    {
                        HttpResponseMessage httpPersona = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + adeudo.IdPersona, HttpMethod.Get);

                        PersonaDTOCompleto objDTOPersona = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(httpPersona);

                        //Adeudos Por cobrar que deben ir al DEBE

                        DetalleContabilidadCrear objDetalleTemporal = new DetalleContabilidadCrear();

                        objDetalleTemporal.FechaDetCont = fechaADeudoActual;
                        objDetalleTemporal.DetalleDetCont = "Generación " + fechaADeudoActual.ToString("dd-MMM-yyyy");

                        objDetalleTemporal.FechaCreacion = DateTime.Now;
                        objDetalleTemporal.FechaModificacion = DateTime.Now;
                        objDetalleTemporal.UsuarioCreacion = adeudo.UsuarioCreacion;
                        objDetalleTemporal.UsuarioModificacion = adeudo.UsuarioCreacion;
                        objDetalleTemporal.DebitoDetCont = adeudo.MontoAdeudos;

                        //Debe
                        objDetalleTemporal.IdCuentaContable = objMaestroContable.CtaCont1;

                        objDetalleTemporal.NroDepartmentoCont = adeudo.Departamento;

                        objDetalleTemporal.DetalleDetCont = "GENERACIÓN DE " + fechaADeudoActual.ToString("MMMM").ToUpper() + " " + objCuentaCont1.NombreCuenta;
                        listaDetallesHaber.Add(objDetalleTemporal);
                    }

                    HttpResponseMessage httpCrearAdeudo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI, HttpMethod.Post, listaAdeudos);

                    if (httpCrearAdeudo.IsSuccessStatusCode)
                    {
                        List<AdeudoDTOCompleto> listaMostrar = _mapper.Map<List<AdeudoDTOCompleto>>(listaAdeudos);

                        CatalogoDTOResultadoBusqueda objCataGeneracion = await recuperarCatalogoPorCodigo(ConstantesAplicacion.tipoTransaccion);

                        //Recuperar Secuencial
                        HttpResponseMessage respuestaSecuencial = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.SecuencialContabilidad, HttpMethod.Get);

                        var secuencialMaximo = await LeerRespuestas<string>.procesarRespuestasConsultas(respuestaSecuencial);
                        int nuevoSecuencial = Convert.ToInt32(secuencialMaximo) + 1;

                        SecuencialCabeceraContDTO objSecuencial = new SecuencialCabeceraContDTO();
                        objSecuencial.Secuencial = nuevoSecuencial;

                        //Recuperar Cuenta contable


                        EncabezContDTOCrear objDTOCabecera = new EncabezContDTOCrear();

                        objDTOCabecera.SecuencialCabeceraConts = new List<SecuencialCabeceraContDTO>();
                        objDTOCabecera.SecuencialCabeceraConts.Add(objSecuencial);

                        objDTOCabecera.ConceptoEncCont = "Generación Adeudos " + fechaADeudoActual.ToString("MMMM");
                        objDTOCabecera.IdConjunto = (Guid)variable.IdConjunto;
                        objDTOCabecera.TipoDocNEncCont = objCataGeneracion.IdCatalogo;
                        objDTOCabecera.Mes = variable.mes;
                        objDTOCabecera.FechaEncCont = fechaADeudoActual;
                        objDTOCabecera.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                        //Esta es la cuenta contable del HABER
                        DetalleContabilidadCrear objDetalle = new DetalleContabilidadCrear();

                        objDetalle.FechaDetCont = fechaADeudoActual;
                        objDetalle.DetalleDetCont = "Generación " + fechaADeudoActual.ToString("dd-MMM-yyyy");

                        objDetalle.FechaCreacion = DateTime.Now;
                        objDetalle.FechaModificacion = DateTime.Now;
                        objDetalle.UsuarioCreacion = objDTOCabecera.UsuarioCreacion;
                        objDetalle.UsuarioModificacion = objDTOCabecera.UsuarioCreacion;
                        objDetalle.CreditoDetCont = listaAdeudos.Sum(x => x.MontoAdeudos);

                        HttpResponseMessage httpCuentaContable = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + objMaestroContable.CtaCont2, HttpMethod.Get);

                        MaestroContableDTOCompleto objCuenta = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(httpCuentaContable);

                        //Esta es la cuenta contable del HABER
                        if (objMaestroContable.CtaCont2 != null)
                        {
                            objDetalle.IdCuentaContable = (Guid)objMaestroContable.CtaCont2;
                            objDetalle.DetalleDetCont = "Generación " + fechaADeudoActual.ToString("dd-MMM-yyyy") + " " + objCuenta.NombreCuenta;
                        }
                        else
                        {
                            objDetalle.DetalleDetCont = "No exite cuenta en el haber.";
                        }

                        objDTOCabecera.DetalleContabilidads = new List<DetalleContabilidadCrear>();
                        listaDetallesHaber.Add(objDetalle);
                        objDTOCabecera.DetalleContabilidads = listaDetallesHaber;

                        HttpResponseMessage httpCrearCabecera = await _servicioConsumoEncabezado.consumoAPI(ConstantesConsumoAPI.CabeceraContabilidad, HttpMethod.Post, objDTOCabecera);

                        return View("_ListaAdeudos", listaMostrar);
                    }
                    else
                    {
                        TempData["adeudoDuplicados"] = adeudoDuplicados;
                        return View("_ListaAdeudos", new List<AdeudoDTOCompleto>());
                    }
                }

            }

            return View();
        }

        private async Task<List<TorreDTOCompleto>> recuperarListaTorres(Guid idConjuntoDefault)
        {
            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = idConjuntoDefault;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            List<TorreDTOCompleto> listaTorres = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaTorres == null)
                listaTorres = new List<TorreDTOCompleto>();
            else
            {
                TorreDTOCompleto torreTodos = new TorreDTOCompleto();
                torreTodos.IdTorres = Guid.Empty;
                torreTodos.NombreTorres = "Todas";               

                listaTorres = listaTorres.OrderBy(x => x.NombreTorres).ToList();

                listaTorres.Insert(0, torreTodos);
            }
                

            return listaTorres;
        }

        private async Task<AdeudoDTOCrear> recuperarTipoPersonaDepartamento(DepartamentoDTOCompleto departamento, AdeudoDTOCrear objAdeudo)
        {
            if (departamento.TipoPersonas != null)
            {
                if (departamento.TipoPersonas.Count > 0)
                {
                    CatalogoDTOResultadoBusqueda objCataArrendatario = await recuperarCatalogoPorCodigo(ConstantesAplicacion.tipoPersonaCondomino);

                    var personaArrend = departamento.TipoPersonas.Where(x => x.IdTipoPersonaDepartamento == objCataArrendatario.IdCatalogo).FirstOrDefault();
                    objAdeudo.FechaCreacion = DateTime.Now;
                    if (personaArrend != null)
                    {
                        objAdeudo.IdPersona = (Guid)personaArrend.IdPersona;
                        objAdeudo.NombresPersona = personaArrend.NombrePersona;
                    }
                    else
                    {
                        objAdeudo.IdPersona = (Guid)departamento.TipoPersonas.FirstOrDefault().IdPersona;
                        objAdeudo.NombresPersona = departamento.TipoPersonas.FirstOrDefault().NombrePersona;
                    }
                }
            }

            return objAdeudo;
        }

        private async Task<List<AdeudoDTOCompleto>> recuperarListaAdeudos(GenerarAdeudo variable)
        {
            List<AdeudoDTOCompleto> listaResultado = new List<AdeudoDTOCompleto>();

            DateTime fechaADeudoActual = FuncionesUtiles.ObtenerPrimerDiaDelMes(variable.mes, variable.anio);

            variable.fechaADeudoActual = fechaADeudoActual;

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


        /// <summary>
        /// Recpera los catalogos de acuerdo al codigo catalogo, para saber si es un arrendatario o un dueño de departamento
        /// </summary>
        /// <param name="codigoPersona"></param>
        private async Task<CatalogoDTOResultadoBusqueda> recuperarCatalogoPorCodigo(string codigoCatalogo)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + codigoCatalogo, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);

            return objCatalogo;

        }



    }
}
