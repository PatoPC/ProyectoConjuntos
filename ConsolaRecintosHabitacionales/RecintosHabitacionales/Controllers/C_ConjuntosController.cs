using DTOs.AreasDepartamento;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Departamento;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;
using Microsoft.AspNetCore.Http;
using SpreadsheetLight;
using DTOs.Persona;
using System.Collections.Generic;
using RecintosHabitacionales.Servicio.Implementar;
using AutoMapper;

namespace RecintosHabitacionales.Controllers
{
    public class C_ConjuntosController : Controller
    {
        private const string controladorActual = "C_Conjuntos";
        private const string accionActual = "AdministrarConjuntos";

        private readonly IServicioConsumoAPI<ConjuntoDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<List<ConjuntoDTOCrear>> _servicioConsumoAPICrearLista;
        private readonly IServicioConsumoAPI<UsuarioConjuntoDTO> _servicioConsumoAPIUsuarioConjunto;
        private readonly IServicioConsumoAPI<List<UsuarioConjuntoDTO>> _servicioConsumoAPIUsuarioConjuntoLista;
        private readonly IServicioConsumoAPI<ConjuntoDTOEditar> _servicioConsumoAPICrearEditar;
        private readonly IServicioConsumoAPI<BusquedaConjuntos> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;

        private readonly IServicioConsumoAPI<ObjetoBusquedaPersona> _servicioConsumoAPIBusquedaPersona;
        private readonly IServicioConsumoAPI<PersonaDTOCrear> _servicioConsumoAPICrearPersona;
        private readonly IServicioConsumoAPI<TipoPersonaDTO> _servicioConsumoAPICrearTipoPersona;

        private readonly IServicioConsumoAPI<DepartamentoDTOEditar> _servicioConsumoAPIDepartamentoEditar;

        private readonly IServicioConsumoAPI<DepartamentoDTOCrear> _servicioConsumoAPIDepartamento;

        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IMapper _mapper;
        public C_ConjuntosController(IServicioConsumoAPI<ConjuntoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<BusquedaConjuntos> servicioConsumoAPIBusqueda, IServicioConsumoAPI<ConjuntoDTOEditar> servicioConsumoAPIConjuntoEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres, IServicioConsumoAPI<DepartamentoDTOCrear> servicioConsumoAPIDepartamento, IServicioConsumoAPI<DepartamentoDTOEditar> servicioConsumoAPIDepartamentoEditar, IServicioConsumoAPI<UsuarioConjuntoDTO> servicioConsumoAPIUsuarioConjunto, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IServicioConsumoAPI<List<ConjuntoDTOCrear>> servicioConsumoAPICrearLista, IServicioConsumoAPI<List<UsuarioConjuntoDTO>> servicioConsumoAPIUsuarioConjuntoLista, IServicioConsumoAPI<ObjetoBusquedaPersona> servicioConsumoAPIBusquedaPersona, IServicioConsumoAPI<PersonaDTOCrear> servicioConsumoAPICrearPersona, IServicioConsumoAPI<TipoPersonaDTO> servicioConsumoAPICrearTipoPersona, IMapper mapper)
        {
            _servicioConsumoAPICrear = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrearEditar = servicioConsumoAPIConjuntoEditar;
            _servicioConsumoAPIBusquedaTorres = servicioConsumoAPIBusquedaTorres;
            _servicioConsumoAPIDepartamento = servicioConsumoAPIDepartamento;
            _servicioConsumoAPIDepartamentoEditar = servicioConsumoAPIDepartamentoEditar;
            _servicioConsumoAPIUsuarioConjunto = servicioConsumoAPIUsuarioConjunto;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _servicioConsumoAPICrearLista = servicioConsumoAPICrearLista;
            _servicioConsumoAPIUsuarioConjuntoLista = servicioConsumoAPIUsuarioConjuntoLista;
            _servicioConsumoAPIBusquedaPersona = servicioConsumoAPIBusquedaPersona;
            _servicioConsumoAPICrearPersona = servicioConsumoAPICrearPersona;
            _servicioConsumoAPICrearTipoPersona = servicioConsumoAPICrearTipoPersona;
            _mapper = mapper;
        }

        #region CRUD

        #region CrearConjuntos
        public IActionResult CrearConjuntos()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearConjuntos(ConjuntoDTOCrear objDTO)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.crearConjuto, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    ConjuntoDTOCompleto objDTDOCreado = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    UsuarioConjuntoDTO objDTOUsuario = new UsuarioConjuntoDTO();
                    objDTOUsuario.IdUsuario = objUsuarioSesion.IdUsuario;
                    objDTOUsuario.IdConjunto = objDTDOCreado.IdConjunto;

                    HttpResponseMessage respuestaConjuntoUsuario = await _servicioConsumoAPIUsuarioConjunto.consumoAPI(ConstantesConsumoAPI.getCreateUsuarioConjunto, HttpMethod.Post, objDTOUsuario);

                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuestaConjuntoUsuario, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region EditarConjuntos
        public async Task<ActionResult> EditarConjuntos(Guid idConjuntos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + idConjuntos, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    await cargaInicial();
                    ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EditarConjuntos(ConjuntoDTOEditar objDTO, Guid IdConjunto)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrearEditar.consumoAPI(ConstantesConsumoAPI.EditarConjuntosPorID + IdConjunto, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    await cargaInicial();
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion


        #region Eliminar Conjuntos
        public async Task<ActionResult> EliminarConjuntos(Guid idConjuntos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + idConjuntos, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    ConjuntoDTOCompleto objDTO = await LeerRespuestas<ConjuntoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objDTO);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> EliminarConjuntos(Guid IdConjunto, bool eliminar)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrearEditar.consumoAPI(ConstantesConsumoAPI.EditarConjuntosEliminar + IdConjunto, HttpMethod.Post);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual, true));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Crear Desde Archivo
        public async Task<ActionResult> CargarConjuntoDesdeArchivo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CargarConjuntoDesdeArchivo(string produccion)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<ModeloArchivoConjunto> listaArchivoLeido = await procesarExcelConjuntos();

                List<ConjuntoDTOCrear> listaConjuntos = construirConjuntos(objUsuarioSesion, listaArchivoLeido);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrearLista.consumoAPI(ConstantesConsumoAPI.crearListaConjutos, HttpMethod.Post, listaConjuntos);

                if (respuesta.IsSuccessStatusCode)
                {
                    List<ConjuntoDTOCompleto> ListaObjDTDOCreado = await LeerRespuestas<List<ConjuntoDTOCompleto>>.procesarRespuestasConsultas(respuesta);


                    var listaUsuariosConjuntos = ListaObjDTDOCreado.
                            Select(x => new UsuarioConjuntoDTO
                            {
                                IdUsuario = objUsuarioSesion.IdUsuario,
                                IdConjunto = x.IdConjunto,
                            }).ToList();

                    HttpResponseMessage respuestaConjuntoUsuarios = await _servicioConsumoAPIUsuarioConjuntoLista.consumoAPI(ConstantesConsumoAPI.getCreateUsuarioConjuntoLista, HttpMethod.Post, listaUsuariosConjuntos);


                    List<PersonaDTOCompleto> listaPersonas = await construirPersonaConjuntos(objUsuarioSesion, listaArchivoLeido, ListaObjDTDOCreado);



                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuestaConjuntoUsuarios, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }



        #endregion

        #endregion

        #region CrearDepartamento
        [HttpPost]
        public async Task<ActionResult> CrearDepartamento(DepartamentoDTOCrear objDTO, List<decimal> listaTipoAreaDepartamentoCrear, List<Guid> IdTipoAreaCrear)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                List<AreasDepartamentoDTO> listaAreasDepartamentos = new List<AreasDepartamentoDTO>();

                if (listaTipoAreaDepartamentoCrear != null && IdTipoAreaCrear != null)
                {
                    for (int i = 0; i < IdTipoAreaCrear.Count(); i++)
                    {
                        AreasDepartamentoDTO objTemporal = new AreasDepartamentoDTO(IdTipoAreaCrear[i], listaTipoAreaDepartamentoCrear[i]);

                        listaAreasDepartamentos.Add(objTemporal);
                    }

                    objDTO.AreasDepartamentos = listaAreasDepartamentos;
                }

                HttpResponseMessage respuesta = await _servicioConsumoAPIDepartamento.consumoAPI(ConstantesConsumoAPI.gestionarDepartamentoAPI, HttpMethod.Post, objDTO);

                if (respuesta.IsSuccessStatusCode)
                {
                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Consultas

        [HttpGet]
        public IActionResult AdministrarConjuntos()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpGet]
        public async Task<ActionResult> BusquedaAvanzadaConjuntos(BusquedaConjuntos objBusquedaConjuntos)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {

                List<ResultadoBusquedaConjuntos> listaResultado = new List<ResultadoBusquedaConjuntos>();
                //var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

                //if (objUsuarioSesion != null)
                //{

                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarConjuntosAvanzado, HttpMethod.Get, objBusquedaConjuntos);

                    if (respuesta.IsSuccessStatusCode)
                        listaResultado = await LeerRespuestas<List<ResultadoBusquedaConjuntos>>.procesarRespuestasConsultas(respuesta);
                }
                catch (Exception ex)
                {

                }

                if (listaResultado == null)
                    listaResultado = new List<ResultadoBusquedaConjuntos>();

                return View("_ListaConjutos", listaResultado);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        public async Task<IActionResult> RecuperarListaTorresPorConjutoID(Guid idConjuto)
        {
            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = idConjuto;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaTorres.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            List<TorreDTOCompleto> listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<TorreDTOCompleto>();

            if (listaResultado != null)
            {
                return View("Torre/_ListaTorres", listaResultado);
            }

            return View("Torre/_ListaTorres", new List<TorreDTOCompleto>());
        }

        #endregion

        #region ProcesarArchivo

        public async Task<List<ModeloArchivoConjunto>> procesarExcelConjuntos()
        {
            IFormFileCollection archivosFormulario = Request.Form.Files;
            List<ModeloArchivoConjunto> listaArchivoLeido = new List<ModeloArchivoConjunto>();

            if (archivosFormulario != null)
            {

                foreach (var archivo in archivosFormulario)
                {
                    if (archivo.Length > 0)
                    {
                        listaArchivoLeido = await LeerArchivoConjunto.procesarArchivoExcel(archivo);
                    }
                }
            }

            return listaArchivoLeido;
        }

        public List<ConjuntoDTOCrear> construirConjuntos(UsuarioSesionDTO objUsuarioSesion, List<ModeloArchivoConjunto> listaArchivoLeido)
        {
            string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
            List<ConjuntoDTOCrear> listaConjuntos = new List<ConjuntoDTOCrear>();

            listaConjuntos = listaArchivoLeido.GroupBy(x => x.Nombre_Conjunto, (key, group) => group.First()).
                          Select(x => new ConjuntoDTOCrear
                          {
                              NombreConjunto = x.Nombre_Conjunto,
                              RucConjunto = x.RUC,
                              TelefonoConjunto = x.Telefono,
                              MailConjunto = x.Correo_Conjunto,
                              DireccionConjunto = x.Dirección,
                              UsuarioCreacion = usuarioCreacion,
                              FechaCreacion = DateTime.Now,
                          }).ToList();

            foreach (var conjunto in listaConjuntos)
            {
                List<TorreDTOCrear> listaTorres = new List<TorreDTOCrear>();

                var listaTorresTemporales = listaArchivoLeido.Where(x => x.Nombre_Conjunto == conjunto.NombreConjunto).ToList();

                listaTorres = listaTorresTemporales.GroupBy(x => x.Torre, (key, group) => group.First()).
                    Select(x => new TorreDTOCrear
                    {
                        NombreTorres = x.Torre,
                        UsuarioCreacion = usuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }).ToList();

                conjunto.Torres = listaTorres;

                foreach (var torre in listaTorres)
                {
                    List<DepartamentoDTOCrear> listaDepartamentos = new List<DepartamentoDTOCrear>();

                    listaDepartamentos = listaArchivoLeido.Where(y => y.Torre == torre.NombreTorres).GroupBy(x => new { x.Nombre_Condomino, x.Torre }, (key, group) => group.First()).
                        Select(x => new DepartamentoDTOCrear
                        {
                            CodigoDepartamento = x.Departamento,
                            MetrosDepartamento = x.Metros_Cuadrados,
                            AliqDepartamento = x.Valor_Alicuota,
                            SaldoInicialAnual = x.Saldo_Inicial,
                            UsuarioCreacion = usuarioCreacion,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();

                    torre.Departamentos = listaDepartamentos;
                }
            }

            return listaConjuntos;
        }

        public async Task<List<PersonaDTOCompleto>> construirPersonaConjuntos(UsuarioSesionDTO objUsuarioSesion, List<ModeloArchivoConjunto> listaArchivoLeido, List<ConjuntoDTOCompleto> listaConjuntos)
        {
            string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
            List<PersonaDTOCrear> listaPersonasCrear = new List<PersonaDTOCrear>();
            List<PersonaDTOCompleto> listaPersonasCompleta = new List<PersonaDTOCompleto>();

            //Se filtran todas las opciones de tipo identificacion disponibles en el documento
            var listaFiltrada = listaArchivoLeido.Select(x => x.Tipo_Identificación_Condomino)
                                  .Concat(listaArchivoLeido.Select(x => x.Tipo_Identificación_Propietario))
                                  .Distinct().Where(x => !string.IsNullOrEmpty(x))
                                  .ToList();

            List<CatalogoDTOCompleto> listaCatalogos = new List<CatalogoDTOCompleto>();
            foreach (var tipoIdentificacion in listaFiltrada)
            {
                HttpResponseMessage respuestaTipoIdentificacion = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getNombreExactoCatalogo + tipoIdentificacion, HttpMethod.Get);

                CatalogoDTOCompleto objCatalogo = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaTipoIdentificacion);

                listaCatalogos.Add(objCatalogo);
            }


            List<PersonaDTOCrear> listaPersonasCondominos = listaArchivoLeido.
                         Select(x => new PersonaDTOCrear
                         {
                             IdTipoIdentificacion = listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificación_Condomino.Trim().ToUpper()).FirstOrDefault() != null ? listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificación_Condomino.Trim().ToUpper()).FirstOrDefault().IdCatalogo : null,
                             NombresPersona = x.Nombre_Condomino,
                             ApellidosPersona = x.Apellido_Condomino,
                             Torre = x.Torre,
                             IdentificacionPersona = x.Numero_Identificación_Condomino,
                             TelefonoPersona = x.Telefono_Condomino,
                             CelularPersona = x.Numero_Identificación_Condomino,
                             EmailPersona = x.Correo_Condomino,
                             RUC = x.RUC,
                             Departamento = x.Departamento,
                             UsuarioCreacion = usuarioCreacion,
                             FechaCreacion = DateTime.Now,
                         }).Where(x => !string.IsNullOrEmpty(x.CelularPersona)).ToList();

            List<PersonaDTOCrear> listaPersonasPropietarios = listaArchivoLeido.
                         Select(x => new PersonaDTOCrear
                         {
                             IdTipoIdentificacion = listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificación_Propietario.Trim().ToUpper()).FirstOrDefault() != null ? listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificación_Propietario.Trim().ToUpper()).FirstOrDefault().IdCatalogo : null,
                             RUC = x.RUC,
                             Departamento = x.Departamento,
                             Torre = x.Torre,
                             NombresPersona = x.Nombre_Propietario,
                             ApellidosPersona = x.Apellido_Propietario,
                             IdentificacionPersona = x.Numero_Identificación_Propietario,
                             TelefonoPersona = x.Telefono_Propietario,
                             CelularPersona = x.Numero_Identificación_Propietario,
                             EmailPersona = x.Correo_Propietario,
                             UsuarioCreacion = usuarioCreacion,
                             FechaCreacion = DateTime.Now,
                         }).Where(x => !string.IsNullOrEmpty(x.CelularPersona)).ToList();

            listaPersonasCrear = listaPersonasCondominos.Union(listaPersonasPropietarios).ToList();

            listaPersonasCrear = listaPersonasCrear.Distinct().ToList();

            listaPersonasCrear = listaPersonasCrear.DistinctBy(x => x.CelularPersona).ToList();

            foreach (var personaCrear in listaPersonasCrear)
            {
                ObjetoBusquedaPersona objBusqueda = new ObjetoBusquedaPersona();
                objBusqueda.IdentificacionPersona = personaCrear.IdentificacionPersona;

                HttpResponseMessage respuestaDuplicados = await _servicioConsumoAPIBusquedaPersona.consumoAPI(ConstantesConsumoAPI.buscarPersonaAvanzado, HttpMethod.Get, objBusqueda);

                var listaResultado = await LeerRespuestas<List<PersonaDTOCompleto>>.procesarRespuestasConsultas(respuestaDuplicados);

                if (listaResultado == null)
                    listaResultado = new List<PersonaDTOCompleto>();
                if (listaResultado.Count() == 0)
                {
                    HttpResponseMessage respuestaCrearPersona = await _servicioConsumoAPICrearPersona.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI, HttpMethod.Post, personaCrear);

                    if (respuestaCrearPersona.IsSuccessStatusCode)
                    {
                        PersonaDTOCompleto personaCreada = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuestaCrearPersona);
                        if (personaCreada != null)
                            listaPersonasCompleta.Add(personaCreada);
                        else
                        {

                        }
                    }
                }
                else
                {
                    string duplicado = "";
                }
            }

            //Asignar Persona departamento
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.tipoPersonaPropietario, HttpMethod.Get);

            CatalogoDTOResultadoBusqueda objCatalogoPropietario = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);

            HttpResponseMessage respuestaCatalogoCondomino = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.tipoPersonaCondomino, HttpMethod.Get);

            CatalogoDTOResultadoBusqueda objCatalogoCondominio = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogoCondomino);

            foreach (var persona in listaPersonasCondominos)
            {
                PersonaDTOCompleto objPersonaTemporal = listaPersonasCompleta.Where(x => x.IdentificacionPersona == persona.CelularPersona).FirstOrDefault();

                if (objPersonaTemporal!=null)
                {
                    PersonaDTOConjunto objPersonaConjunto = _mapper.Map<PersonaDTOConjunto>(objPersonaTemporal);
                    
                    var departamentoActual = listaConjuntos.Where(x => x.RucConjunto == persona.RUC).FirstOrDefault().Torres.Where(x => x.NombreTorres==persona.Torre).FirstOrDefault().Departamentos.Where(w => w.CodigoDepartamento==persona.Departamento).FirstOrDefault();

                    objPersonaConjunto.IdDepartamento = departamentoActual.IdDepartamento;

                    TipoPersonaDTO objDTOTipoPersona = _mapper.Map<TipoPersonaDTO>(objPersonaConjunto);

                    objDTOTipoPersona.IdTipoPersonaDepartamento = objCatalogoCondominio.IdCatalogo;

                    objDTOTipoPersona.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                    objDTOTipoPersona.UsuarioModificacion = objDTOTipoPersona.UsuarioCreacion;

                    HttpResponseMessage respuesta = await _servicioConsumoAPICrearTipoPersona.consumoAPI(ConstantesConsumoAPI.crearPersonaDepartamento, HttpMethod.Post, objDTOTipoPersona);

                    if (respuesta.IsSuccessStatusCode)
                    {

                    }
                    else
                    {

                    } 
                }
            }

             foreach (var persona in listaPersonasPropietarios)
            {
                PersonaDTOCompleto objPersonaTemporal = listaPersonasCompleta.Where(x => x.IdentificacionPersona == persona.CelularPersona).FirstOrDefault();

                if (objPersonaTemporal!=null)
                {
                    PersonaDTOConjunto objPersonaConjunto = _mapper.Map<PersonaDTOConjunto>(objPersonaTemporal);
                    
                    var departamentoActual = listaConjuntos.Where(x => x.RucConjunto == persona.RUC).FirstOrDefault().Torres.Where(x => x.NombreTorres==persona.Torre).FirstOrDefault().Departamentos.Where(w => w.CodigoDepartamento==persona.Departamento).FirstOrDefault();

                    objPersonaConjunto.IdDepartamento = departamentoActual.IdDepartamento;

                    TipoPersonaDTO objDTOTipoPersona = _mapper.Map<TipoPersonaDTO>(objPersonaConjunto);

                    objDTOTipoPersona.IdTipoPersonaDepartamento = objCatalogoPropietario.IdCatalogo;

                    objDTOTipoPersona.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                    objDTOTipoPersona.UsuarioModificacion = objDTOTipoPersona.UsuarioCreacion;

                    HttpResponseMessage respuesta = await _servicioConsumoAPICrearTipoPersona.consumoAPI(ConstantesConsumoAPI.crearPersonaDepartamento, HttpMethod.Post, objDTOTipoPersona);

                    if (respuesta.IsSuccessStatusCode)
                    {

                    }
                    else
                    {

                    } 
                }
            }




            return listaPersonasCompleta;
        }
        #endregion

        public async Task cargaInicial()
        {
            ViewData["listaTipoAreas"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoAreas, "IdCatalogo", "Nombrecatalogo");
        }

    }
}
