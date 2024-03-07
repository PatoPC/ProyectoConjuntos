using AutoMapper;
using DTOs.Adeudo;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Contabilidad;
using DTOs.Persona;
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
        private readonly IServicioConsumoAPI<GenerarAdeudo> _servicioConsumoBusqueda;
        private readonly IServicioConsumoAPI<List<AdeudoDTOCrear>> _servicioConsumoAPICrear;
        private readonly IMapper _mapper;
        public C_AdeudoController(IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusqueda, IServicioConsumoAPI<List<AdeudoDTOCrear>> servicioConsumoAPICrear, IServicioConsumoAPI<GenerarAdeudo> servicioConsumoBusqueda, IMapper mapper)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoBusqueda = servicioConsumoBusqueda;
            _mapper = mapper;
        }

        public IActionResult GestionarAdeudo()
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
        public async Task<ActionResult> BusquedaAvanzadaAdeudo(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);


            if (objUsuarioSesion != null)
            {

                List<AdeudoDTOCompleto> listaResultado = await recuperarListaAdeudos(variable);

                return View("_ListaAdeudos", listaResultado);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
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
                List<AdeudoDTOCrear> listaAdeudos = new List<AdeudoDTOCrear>();
                DateTime fechaADeudoActual = FuncionesUtiles.ObtenerUltimoDiaDelMes(variable.mes, variable.anio);
                List<string> adeudoDuplicados = new List<string>();

                if (variable.IdConjunto != null)
                {
                    BusquedaTorres objBusquedaTorres = new BusquedaTorres();
                    objBusquedaTorres.IdConjunto = (Guid)variable.IdConjunto;

                    List<TorreDTOCompleto> listaResultado = new List<TorreDTOCompleto>();

                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

                    CatalogoDTOResultadoBusqueda objCataArrendatario = await tipoPersonaDepartmento(ConstantesAplicacion.tipoPersonaCondomino);

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
                                objAdeudo.Torre = torre.NombreTorres;
                                objAdeudo.FechaAdeudos = fechaADeudoActual;
                                objAdeudo.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                                if (departamento.TipoPersonas != null)
                                {
                                    if (departamento.TipoPersonas.Count > 0)
                                    {
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

                                        listaAdeudos.Add(objAdeudo);
                                    }

                                }
                            }
                        }

                        if (listaAdeudos.Count > 0)
                        {
                            List<AdeudoDTOCompleto> adeudosActuales = await recuperarListaAdeudos(variable);

                            List<AdeudoDTOCompleto> adeudosExistentes = new List<AdeudoDTOCompleto>();
                            List<AdeudoDTOCompleto> adeudosNuevos = new List<AdeudoDTOCompleto>();

                            foreach (AdeudoDTOCompleto adeudo in adeudosActuales)
                            {
                                AdeudoDTOCrear adeudoTemporal = listaAdeudos.Where(x => x.IdDepartamento == adeudo.IdDepartamento && x.IdPersona == adeudo.IdPersona && x.FechaAdeudos == adeudo.FechaAdeudos).FirstOrDefault();

                                if(adeudoTemporal != null)
                                    adeudosExistentes.Add(adeudo);                           
                            }

                            if(adeudosExistentes.Count > 0)
                            {
                                foreach (var adeudoExistente in adeudosExistentes)
                                {
                                    AdeudoDTOCrear adeudoTemporal = listaAdeudos.Where(x => x.IdDepartamento == adeudoExistente.IdDepartamento && x.IdPersona == adeudoExistente.IdPersona && x.FechaAdeudos == adeudoExistente.FechaAdeudos).FirstOrDefault();

                                    if (adeudoTemporal != null)
                                    {
                                        string adeudoEliminado = adeudoTemporal.NombresPersona + " " + adeudoTemporal.ApellidosPersona+" Departamento: "+adeudoTemporal.Departamento;
                                        adeudoDuplicados.Add(adeudoEliminado);
                                    }


                                    listaAdeudos.Remove(adeudoTemporal);
                                }
                            }

                            HttpResponseMessage httpCrearAdeudo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI, HttpMethod.Post, listaAdeudos);

                            if (httpCrearAdeudo.IsSuccessStatusCode)
                            {
                                List<AdeudoDTOCompleto> listaMostrar = _mapper.Map<List<AdeudoDTOCompleto>>(listaAdeudos);

                                CatalogoDTOResultadoBusqueda objCataGeneracion = await tipoPersonaDepartmento(ConstantesAplicacion.tipoTransaccion);

                                EncabezContDTOCrear objDTO = new EncabezContDTOCrear();

                                objDTO.IdConjunto = variable.IdConjunto;
                                objDTO.TipoDocNEncCont = objCataGeneracion.IdCatalogo;
                                objDTO.FechaEncCont = fechaADeudoActual;
                                objDTO.UsuarioCreacionEncCont = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                                DetalleContabilidadCrear objDetalle = new DetalleContabilidadCrear();

                                objDetalle.FechaDetCont = fechaADeudoActual;
                                objDetalle.DetalleDetCont = "Generación "+ fechaADeudoActual.ToString("dd-MMM-yyyy");

                                objDetalle.FechaCreacion = DateTime.Now;
                                objDetalle.FechaModificacion = DateTime.Now;
                                objDetalle.UsuarioCreacion = objDTO.UsuarioCreacionEncCont;
                                objDetalle.UsuarioModificacion = objDTO.UsuarioCreacionEncCont;

                                return View("_ListaAdeudos", listaMostrar);
                            }
                            else
                            {
                                TempData["adeudoDuplicados"] = adeudoDuplicados;
                                return View("_ListaAdeudos", new List<AdeudoDTOCompleto>());
                            }
                        }
                    }
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        private async Task<List<AdeudoDTOCompleto>> recuperarListaAdeudos(GenerarAdeudo variable)
        {
            List<AdeudoDTOCompleto> listaResultado = new List<AdeudoDTOCompleto>();

            DateTime fechaADeudoActual = FuncionesUtiles.ObtenerUltimoDiaDelMes(variable.mes, variable.anio);

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
        private async Task<CatalogoDTOResultadoBusqueda> tipoPersonaDepartmento(string codigoCatalogo)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + codigoCatalogo, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);

            return objCatalogo;

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
