﻿using DTOs.AreasDepartamento;
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
using DTOs.Roles;
using DTOs.AreaComunal;
using System.Net.Http;
using DTOs.Parametro;
using DTOs.MaestroContable;
using DTOs.ConfiguracionCuenta;
using XAct;
using XAct.Library.Settings;

namespace RecintosHabitacionales.Controllers
{
    public class C_ConjuntosController : Controller
    {
        private const string controladorActual = "C_Conjuntos";
        private const string accionActual = "AdministrarConjuntos";

        private readonly IServicioConsumoAPI<ConjuntoDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<UsuarioDTOCrear> UsuarioDTOCrearUsuario;
        private readonly IServicioConsumoAPI<List<ConjuntoDTOCrearArchivo>> _servicioConsumoAPICrearLista;
        private readonly IServicioConsumoAPI<UsuarioConjuntoDTO> _servicioConsumoAPIUsuarioConjunto;
        private readonly IServicioConsumoAPI<List<UsuarioConjuntoDTO>> _servicioConsumoAPIUsuarioConjuntoLista;
        private readonly IServicioConsumoAPI<ConjuntoDTOEditar> _servicioConsumoAPICrearEditar;

        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;
        private readonly IServicioConsumoAPI<BusquedaAreaComunal> _servicioBusqueAreaComunal;

        private readonly IServicioConsumoAPI<ObjetoBusquedaPersona> _servicioConsumoAPIBusquedaPersona;
        private readonly IServicioConsumoAPI<PersonaDTOCrear> _servicioConsumoAPICrearPersona;
        private readonly IServicioConsumoAPI<TipoPersonaDTO> _servicioConsumoAPICrearTipoPersona;
        private readonly IServicioConsumoAPI<BusquedaConjuntos> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<DepartamentoDTOEditar> _servicioConsumoAPIDepartamentoEditar;

        private readonly IServicioConsumoAPI<DepartamentoDTOCrear> _servicioConsumoAPIDepartamento;

        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IServicioConsumoAPI<CatalogoDTOCrear> _servicioConsumoAPICrearCatalogos;

        private readonly CargarMaestroContable _servicioMestroContable;

        private readonly IServicioConsumoAPI<MaestroContableBusqueda> _servicioConsumoAPIBusquedaMaestro;
        private readonly IMapper _mapper;
        public C_ConjuntosController(IServicioConsumoAPI<ConjuntoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<ConjuntoDTOEditar> servicioConsumoAPIConjuntoEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres, IServicioConsumoAPI<DepartamentoDTOCrear> servicioConsumoAPIDepartamento, IServicioConsumoAPI<DepartamentoDTOEditar> servicioConsumoAPIDepartamentoEditar, IServicioConsumoAPI<UsuarioConjuntoDTO> servicioConsumoAPIUsuarioConjunto, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IServicioConsumoAPI<List<ConjuntoDTOCrearArchivo>> servicioConsumoAPICrearLista, IServicioConsumoAPI<List<UsuarioConjuntoDTO>> servicioConsumoAPIUsuarioConjuntoLista, IServicioConsumoAPI<ObjetoBusquedaPersona> servicioConsumoAPIBusquedaPersona, IServicioConsumoAPI<PersonaDTOCrear> servicioConsumoAPICrearPersona, IServicioConsumoAPI<TipoPersonaDTO> servicioConsumoAPICrearTipoPersona, IMapper mapper, IServicioConsumoAPI<CatalogoDTOCrear> servicioConsumoAPICrearCatalogos, IServicioConsumoAPI<UsuarioDTOCrear> usuarioDTOCrearUsuario, IServicioConsumoAPI<BusquedaAreaComunal> servicioBusqueAreaComunal, IServicioConsumoAPI<MaestroContableBusqueda> servicioConsumoAPIBusquedaMaestro, CargarMaestroContable servicioMestroContable, IServicioConsumoAPI<BusquedaConjuntos> servicioConsumoAPIBusqueda)
        {
            _servicioConsumoAPICrear = servicioConsumoAPIConjunto;
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
            _servicioConsumoAPICrearCatalogos = servicioConsumoAPICrearCatalogos;
            UsuarioDTOCrearUsuario = usuarioDTOCrearUsuario;
            _servicioBusqueAreaComunal = servicioBusqueAreaComunal;
            _servicioConsumoAPIBusquedaMaestro = servicioConsumoAPIBusquedaMaestro;
            _servicioMestroContable = servicioMestroContable;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
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
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.crearConjunto, HttpMethod.Post, objDTO);

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
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaPersona.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + idConjuntos, HttpMethod.Get);

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
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaPersona.consumoAPI(ConstantesConsumoAPI.buscarConjuntosPorID + idConjuntos, HttpMethod.Get);

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
        public ActionResult CargarConjuntoDesdeArchivo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CargarConjuntoDesdeArchivo(string produccion)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuestaRol = await _servicioConsumoAPIBusquedaPersona.consumoAPI(ConstantesConsumoAPI.getRolPorNombreExacto + ConstantesAplicacion.nombreRolCondominos, HttpMethod.Get);

            var objRol = await LeerRespuestas<RolDTOBusqueda>.procesarRespuestasConsultas(respuestaRol);

            if (objRol != null)
            {
                List<ModeloArchivoConjunto> listaArchivoLeido = await procesarExcelConjuntos(FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion));

                List<ModeloArchivoConjunto> cedulasInvalidasCondominos = listaArchivoLeido
                    .Where(x => x.Numero_Identificacion_Condomino.Contains(ConstantesAplicacion.CedulaNoValida)).ToList();

                List<ModeloArchivoConjunto> cedulasInvalidasPropietarios = listaArchivoLeido
                       .Where(x => x.Numero_Identificacion_Propietario.Contains(ConstantesAplicacion.CedulaNoValida)).ToList();

                if (cedulasInvalidasCondominos.Count() > 0 || cedulasInvalidasPropietarios.Count() > 0)
                {
                    string mensajeTemporal = "";

                    foreach (ModeloArchivoConjunto item in cedulasInvalidasCondominos)
                    {
                        mensajeTemporal += " <b>Identificación incorrecta de Condomino:</b> <br/>" + item.Nombre_Condomino + " "
                            + item.Apellido_Condomino + "<br/>";
                    }

                    foreach (ModeloArchivoConjunto item in cedulasInvalidasPropietarios)
                    {
                        mensajeTemporal += " <b>Identificación incorrecta de Propietarios :</b> <br/>"
                            + item.Nombre_Propietario + " " + item.Apellido_Propietario + "<br/>";
                    }



                    return Json(new { success = true, message = mensajeTemporal, IsSuccess = false, state = "error" });
                }

                List<ConjuntoDTOCrearArchivo> listaConjuntos = construirConjuntos(objUsuarioSesion, listaArchivoLeido);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrearLista.consumoAPI(ConstantesConsumoAPI.crearListaConjuntos, HttpMethod.Post, listaConjuntos);

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

                    //Crear usuarios
                    foreach (var objPersona in listaPersonas)
                    {
                        UsuarioDTOCrear objDTO = new UsuarioDTOCrear();
                        objDTO.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                        objDTO.Contrasena = FuncionesContrasena.encriptarContrasena(objPersona.IdentificacionPersona);

                        //Encontrar conjunto del usuario 
                        var listaPersonaCnjunto = listaArchivoLeido
                            .Where(x =>
                            x.Numero_Identificacion_Condomino.Trim() == objPersona.IdentificacionPersona.Trim()
                            || x.Numero_Identificacion_Propietario.Trim() == objPersona.IdentificacionPersona.Trim()
                            ).ToList();

                        List<UsuarioConjuntoDTO> UsuarioConjuntos = new List<UsuarioConjuntoDTO>();

                        foreach (var datos in listaPersonaCnjunto)
                        {
                            ConjuntoDTOCompleto conjuntoTemporal = ListaObjDTDOCreado.Where(x => x.RucConjunto.Trim() == datos.RUC).FirstOrDefault();

                            UsuarioConjuntoDTO objConjuntoUsuario = new UsuarioConjuntoDTO();

                            objConjuntoUsuario.IdConjunto = conjuntoTemporal.IdConjunto;
                            objDTO.IdConjuntoDefault = conjuntoTemporal.IdConjunto;

                            UsuarioConjuntos.Add(objConjuntoUsuario);
                        }

                        if (objRol != null)
                        {
                            objDTO.IdRol = objRol.IdRol;

                            HttpResponseMessage respuestaPersona = await UsuarioDTOCrearUsuario.consumoAPI(ConstantesConsumoAPI.getCreateUsuario, HttpMethod.Post, objDTO);

                            if (respuestaPersona.IsSuccessStatusCode)
                            {

                            }
                        }
                    }

                    return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuestaConjuntoUsuarios, controladorActual, accionActual));
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }
            }
            else
                return new JsonResult(MensajesRespuesta.errorNoExisteRol());
        }



        #endregion

        #endregion

        #region CrearDepartamento
        [HttpPost]
        public async Task<ActionResult> CrearDepartamento(DepartamentoDTOCrear objDTO, List<decimal> listaTipoAreaDepartamentoCrear, List<Guid> IdTipoAreaCrear, Guid idPersonaPropietario, Guid idPersonaCondomino)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                //string nuevaCuentaAdeudo = "000";
                string nuevaCuentaAdeudo = "0";
                int cuentaAdeudo = 0;
                MaestroContableDTOCrear objCuentaAdeudo = new MaestroContableDTOCrear();

                string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
                objDTO.UsuarioCreacion = usuarioCreacion;

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
                //Recupera los modulos contables para adeudo
                HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPIBusquedaTorres.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + ConstantesAplicacion.adeudoModulosContables, HttpMethod.Get);

                if (respuestaCatalogo.IsSuccessStatusCode)
                {
                    objDTO.TipoPersonas = await cargarPersonasDepartamento(idPersonaPropietario, idPersonaCondomino, usuarioCreacion);

                    string nombreNuevaCuenta = await identificacionNuevaCuenta(idPersonaPropietario, idPersonaCondomino);

                    CatalogoDTOResultadoBusqueda objAdeudo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuestaCatalogo);

                    HttpResponseMessage respuestaParametro = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.obtenerParametroPorCatalogo + objAdeudo.IdCatalogo, HttpMethod.Get);

                    if (respuestaParametro.IsSuccessStatusCode)
                    {
                        ParametroCompletoDTO objMaestroContable = await LeerRespuestas<ParametroCompletoDTO>.procesarRespuestasConsultas(respuestaParametro);

                        HttpResponseMessage respuestaContable = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + objMaestroContable.CtaCont1, HttpMethod.Get);

                        if (respuestaContable.IsSuccessStatusCode)
                        {
                            MaestroContableDTOCompleto dtoMaestroCompleto = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuestaContable);

                            objCuentaAdeudo.Grupo = false;
                            objCuentaAdeudo.IdConjunto = objMaestroContable.IdConjunto;

                            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

                            HttpResponseMessage respuestaConfigurar = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.buscarConfiguracion + objMaestroContable.IdConjunto, HttpMethod.Get);

                            if (respuestaConfigurar.IsSuccessStatusCode)
                                objConfigurar = await LeerRespuestas<ConfiguraCuentasDTOCompleto>.procesarRespuestasConsultas(respuestaConfigurar);

                            if (objConfigurar != null)
                            {
                                MaestroContableBusqueda objBusqueda = new MaestroContableBusqueda();
                                objBusqueda.IdConjunto = objUsuarioSesion.IdConjuntoDefault;
                                objBusqueda.CuentaCon = dtoMaestroCompleto.CuentaCon;

                                //Se comenta porque no se va a guardar formateado y separado por puntos
                                string cuentaActual = FuncionesUtiles.FormatearCadenaCuenta(dtoMaestroCompleto.CuentaCon, objConfigurar.Parametrizacion);

                                List<MaestroContableDTOCompleto> ListaCuentasPadre = await _servicioMestroContable.recuperarMaestroContable(objBusqueda);

                                MaestroContableDTOCompleto objCuentaPadre = new MaestroContableDTOCompleto();

                                objCuentaPadre = ListaCuentasPadre.FirstOrDefault();

                                //if (cuentaActual.Length == objConfigurar.Parametrizacion.Length)
                                //{
                                string[] configuracion = cuentaActual.Split('.');

                                if (objCuentaPadre != null)
                                {
                                    objCuentaAdeudo.IdConMstPadre = objCuentaPadre.IdConMst;

                                    if (objCuentaPadre.InverseIdConMstPadreNavigation.Count > 0)
                                    {
                                        var cuentasHijas = objCuentaPadre.InverseIdConMstPadreNavigation.Count;

                                        cuentaAdeudo = cuentasHijas + 1;

                                        if (cuentaAdeudo < 10)
                                            nuevaCuentaAdeudo = dtoMaestroCompleto.CuentaCon + "0" + cuentaAdeudo.ToString();
                                        else
                                            nuevaCuentaAdeudo = dtoMaestroCompleto.CuentaCon + cuentaAdeudo.ToString();
                                    }
                                    else
                                    {
                                        nuevaCuentaAdeudo = dtoMaestroCompleto.CuentaCon + "01";
                                    }

                                    //nuevaCuentaAdeudo = FuncionesUtiles.TruncarString(nuevaCuentaAdeudo, 4);
                                    objCuentaAdeudo.CuentaCon = nuevaCuentaAdeudo;
                                    objCuentaAdeudo.NombreCuenta = nombreNuevaCuenta;

                                    objCuentaAdeudo.UsuarioCreacion = usuarioCreacion;
                                    objCuentaAdeudo.UsuarioModificacion = usuarioCreacion;
                                    objCuentaAdeudo.FechaCreacion = DateTime.Now;
                                    objCuentaAdeudo.FechaModificacion = DateTime.Now;
                                }

                                //}
                            }

                        }

                    }
                    else
                    {
                        MensajesRespuesta objMensajeRespuesta = await respuestaParametro.ExceptionResponse();
                        return new JsonResult(objMensajeRespuesta);
                    }
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuestaCatalogo.ExceptionResponse();
                    return new JsonResult(objMensajeRespuesta);
                }

                objDTO.IdConMstNavigation = objCuentaAdeudo;


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

        #region Varios Departamento
        private async Task<string> identificacionNuevaCuenta(Guid idPersonaPropietario, Guid idPersonaCondomino)
        {
            Guid idPersonaBuscar = Guid.Empty;
            string numeroIdentificacion = "";

            if (idPersonaCondomino != Guid.Empty)
                idPersonaBuscar = idPersonaCondomino;
            else
                idPersonaBuscar = idPersonaPropietario;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaPersona.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + idPersonaBuscar, HttpMethod.Get);

            if (respuesta.IsSuccessStatusCode)
            {
                PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuesta);

                numeroIdentificacion = objDTO.IdentificacionPersona;
            }

            return numeroIdentificacion;
        }

        private async Task<List<TipoPersonaDTO>> cargarPersonasDepartamento(Guid idPersonaPropietario, Guid idPersonaCondomino, string usuarioCreacion)
        {
            List<TipoPersonaDTO> tipoPersonas = new List<TipoPersonaDTO>();
            //Inicio Personas Departamento
            List<CatalogoDTODropDown> listaTipoPersona = await DropDownsCatalogos<CatalogoDTODropDown>.procesarRespuestasConsultaCatlogoObjeto(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoPersona);

            CatalogoDTODropDown objPropietario = listaTipoPersona.Where(x => x.CodigoCatalogo == ConstantesAplicacion.tipoPersonaPropietario).FirstOrDefault();

            TipoPersonaDTO objTipoPropietario = new TipoPersonaDTO();
            objTipoPropietario.IdTipoPersonaDepartamento = objPropietario.IdCatalogo;
            objTipoPropietario.IdPersona = idPersonaPropietario;

            objTipoPropietario.UsuarioCreacion = usuarioCreacion;
            objTipoPropietario.UsuarioModificacion = usuarioCreacion;
            objTipoPropietario.FechaCreacion = DateTime.Now;
            objTipoPropietario.FechaModificacion = DateTime.Now;

            tipoPersonas.Add(objTipoPropietario);

            if (idPersonaCondomino != Guid.Empty)
            {
                CatalogoDTODropDown objCondomino = listaTipoPersona.Where(x => x.CodigoCatalogo == ConstantesAplicacion.tipoPersonaCondomino).FirstOrDefault();

                TipoPersonaDTO objTipoCondomino = new TipoPersonaDTO();
                objTipoCondomino.IdTipoPersonaDepartamento = objCondomino.IdCatalogo;
                objTipoCondomino.IdPersona = idPersonaCondomino;
                objTipoCondomino.UsuarioCreacion = usuarioCreacion;
                objTipoCondomino.UsuarioModificacion = usuarioCreacion;
                objTipoCondomino.FechaCreacion = DateTime.Now;
                objTipoCondomino.FechaModificacion = DateTime.Now;

                tipoPersonas.Add(objTipoCondomino);
            }
            //Fin Personas departamento

            return tipoPersonas;
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

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            List<ResultadoBusquedaConjuntos> listaResultado = new List<ResultadoBusquedaConjuntos>();

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


        public async Task<IActionResult> RecuperarListaTorresPorConjuntoID(Guid idConjunto)
        {
            List<TorreDTOCompleto> listaResultado = await cargarTorresIdConjunto(idConjunto);


            if (listaResultado != null)
            {
                return View("Torre/_ListaTorres", listaResultado);
            }

            return View("Torre/_ListaTorres", new List<TorreDTOCompleto>());
        }


        public async Task<JsonResult> cargarSelectTorres(Guid idConjunto)
        {
            List<CatalogoDTODropDown> listaFinal = new List<CatalogoDTODropDown>();

            try
            {
                List<TorreDTOCompleto> listaTorres = await cargarTorresIdConjunto(idConjunto);                

                listaFinal = listaTorres.Select(x => new CatalogoDTODropDown
                {
                    IdCatalogo = x.IdTorres,
                    Nombrecatalogo = x.NombreTorres
                }).ToList();
            }
            catch (Exception ex)
            {
                listaFinal = new List<CatalogoDTODropDown>();
            }

            return new JsonResult(listaFinal);
        }

        private async Task<List<TorreDTOCompleto>> cargarTorresIdConjunto(Guid idConjunto)
        {
            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = idConjunto;

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusquedaTorres.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);

            List<TorreDTOCompleto> listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<TorreDTOCompleto>();

            return listaResultado;
        }



        public async Task<IActionResult> RecuperarListaAreaComunalPorConjuntoID(Guid idConjunto)
        {
            BusquedaAreaComunal objBusqueda = new BusquedaAreaComunal();

            objBusqueda.IdConjunto = idConjunto;

            HttpResponseMessage respuesta = await _servicioBusqueAreaComunal.consumoAPI(ConstantesConsumoAPI.BuscarAreaComunalAvanzado, HttpMethod.Get, objBusqueda);

            List<AreaComunalDTOCompleto> listaResultado = await LeerRespuestas<List<AreaComunalDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            if (listaResultado == null)
                listaResultado = new List<AreaComunalDTOCompleto>();

            if (listaResultado != null)
            {
                return View("AreaComunal/_ListaAreaComunal", listaResultado);
            }

            return View("AreaComunal/_ListaAreaComunal", new List<AreaComunalDTOCompleto>());
        }

        public async Task<IActionResult> RecuperarListaAreaComunicadoPorConjuntoID(Guid idConjunto)
        {
            BusquedaTorres objBusquedaTorres = new BusquedaTorres();

            objBusquedaTorres.IdConjunto = idConjunto;

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

        public async Task<List<ModeloArchivoConjunto>> procesarExcelConjuntos(string usuarioCreacion)
        {
            IFormFileCollection archivosFormulario = Request.Form.Files;
            List<ModeloArchivoConjunto> listaArchivoLeido = new List<ModeloArchivoConjunto>();

            if (archivosFormulario != null)
            {

                foreach (var archivo in archivosFormulario)
                {
                    if (archivo.Length > 0)
                    {
                        listaArchivoLeido = await LeerArchivoConjunto.procesarArchivoExcel(archivo, _servicioConsumoAPICrearCatalogos, usuarioCreacion);
                    }
                }
            }

            return listaArchivoLeido;
        }

        public List<ConjuntoDTOCrearArchivo> construirConjuntos(UsuarioSesionDTO objUsuarioSesion, List<ModeloArchivoConjunto> listaArchivoLeido)
        {
            string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);
            List<ConjuntoDTOCrearArchivo> listaConjuntos = new List<ConjuntoDTOCrearArchivo>();

            listaConjuntos = listaArchivoLeido.GroupBy(x => x.Nombre_Conjunto, (key, group) => group.First()).
                          Select(x => new ConjuntoDTOCrearArchivo
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
                List<TorreDTOCrearArchivo> listaTorres = new List<TorreDTOCrearArchivo>();

                var listaTorresTemporales = listaArchivoLeido.Where(x => x.Nombre_Conjunto == conjunto.NombreConjunto).ToList();

                listaTorres = listaTorresTemporales.GroupBy(x => x.Torre, (key, group) => group.First()).
                    Select(x => new TorreDTOCrearArchivo
                    {
                        NombreTorres = x.Torre,
                        UsuarioCreacion = usuarioCreacion,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }).ToList();

                conjunto.Torres = listaTorres;

                foreach (var torre in listaTorres)
                {
                    List<DepartamentoDTOCrearArchivo> listaDepartamentos = new List<DepartamentoDTOCrearArchivo>();

                    listaDepartamentos = listaArchivoLeido.Where(y => y.Torre == torre.NombreTorres).GroupBy(x => new { x.Nombre_Condomino, x.Torre }, (key, group) => group.First()).
                        Select(x => new DepartamentoDTOCrearArchivo
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

                    foreach (var departamento in listaDepartamentos)
                    {
                        List<AreasDepartamentoDTO> listaAreas = new List<AreasDepartamentoDTO>();

                        try
                        {
                            var areasTemporal = listaArchivoLeido
                                                .Where(y =>
                                                y.Torre == torre.NombreTorres
                                                && y.Departamento == departamento.CodigoDepartamento
                                                && y.listaAreasDepartamentos != null)
                                                .GroupBy(x =>
                                                        new
                                                        { x.Nombre_Condomino, x.Torre, x.Departamento }, (key, group) => group.First()).
                                                        Select(x =>
                                                            x.listaAreasDepartamentos
                                                        );

                            foreach (var item in areasTemporal)
                            {
                                var datos = item.Split(';');
                                foreach (var area in datos)
                                {
                                    var datosFinal = area.Split(',');
                                    AreasDepartamentoDTO objAreas = new AreasDepartamentoDTO();

                                    objAreas.IdTipoArea = Guid.Parse(datosFinal[0]);
                                    objAreas.MetrosCuadrados = Convert.ToDecimal(datosFinal[1]);

                                    listaAreas.Add(objAreas);
                                }
                            }

                        }
                        catch (Exception exx)
                        {

                        }
                        if (listaAreas != null)
                        {
                            if (listaAreas.Count > 0)
                            {
                                departamento.AreasDepartamentos = listaAreas;
                            }
                        }
                    }
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
            var listaFiltrada = listaArchivoLeido.Select(x => x.Tipo_Identificacion_Condomino)
                                  .Concat(listaArchivoLeido.Select(x => x.Tipo_Identificacion_Propietario))
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
                             IdTipoIdentificacion = listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificacion_Condomino.Trim().ToUpper()).FirstOrDefault() != null ? listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificacion_Condomino.Trim().ToUpper()).FirstOrDefault().IdCatalogo : null,
                             NombresPersona = x.Nombre_Condomino,
                             ApellidosPersona = x.Apellido_Condomino,
                             Torre = x.Torre,
                             IdentificacionPersona = x.Numero_Identificacion_Condomino,
                             TelefonoPersona = x.Telefono_Condomino,
                             CelularPersona = x.Celular_Condomino,
                             EmailPersona = x.Correo_Condomino,
                             RUC = x.RUC,
                             Departamento = x.Departamento,
                             UsuarioCreacion = usuarioCreacion,
                             FechaCreacion = DateTime.Now,
                         }).Where(x => !string.IsNullOrEmpty(x.CelularPersona)).ToList();

            List<PersonaDTOCrear> listaPersonasPropietarios = listaArchivoLeido.
                         Select(x => new PersonaDTOCrear
                         {
                             IdTipoIdentificacion = listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificacion_Propietario.Trim().ToUpper()).FirstOrDefault() != null ? listaCatalogos.Where(y => y.Nombrecatalogo.Trim().ToUpper() == x.Tipo_Identificacion_Propietario.Trim().ToUpper()).FirstOrDefault().IdCatalogo : null,
                             RUC = x.RUC,
                             Departamento = x.Departamento,
                             Torre = x.Torre,
                             NombresPersona = x.Nombre_Propietario,
                             ApellidosPersona = x.Apellido_Propietario,
                             IdentificacionPersona = x.Numero_Identificacion_Propietario,
                             TelefonoPersona = x.Telefono_Propietario,
                             CelularPersona = x.Celular_Propietario,
                             EmailPersona = x.Correo_Propietario,
                             UsuarioCreacion = usuarioCreacion,
                             FechaCreacion = DateTime.Now,
                         }).Where(x => !string.IsNullOrEmpty(x.CelularPersona)).ToList();

            listaPersonasCrear = listaPersonasCondominos.Union(listaPersonasPropietarios).ToList();

            listaPersonasCrear = listaPersonasCrear.Distinct().ToList();

            //listaPersonasCrear = listaPersonasCrear.DistinctBy(x => x.CelularPersona).ToList();

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
                PersonaDTOCompleto objPersonaTemporal = listaPersonasCompleta.Where(x => x.IdentificacionPersona == persona.IdentificacionPersona).FirstOrDefault();

                if (objPersonaTemporal != null)
                {
                    PersonaDTOConjunto objPersonaConjunto = _mapper.Map<PersonaDTOConjunto>(objPersonaTemporal);

                    var departamentoActual = listaConjuntos.Where(x => x.RucConjunto == persona.RUC).FirstOrDefault().Torres.Where(x => x.NombreTorres == persona.Torre).FirstOrDefault().Departamentos.Where(w => w.CodigoDepartamento == persona.Departamento).FirstOrDefault();

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
                PersonaDTOCompleto objPersonaTemporal = listaPersonasCompleta.Where(x => x.IdentificacionPersona == persona.IdentificacionPersona).FirstOrDefault();

                if (objPersonaTemporal != null)
                {
                    PersonaDTOConjunto objPersonaConjunto = _mapper.Map<PersonaDTOConjunto>(objPersonaTemporal);

                    var departamentoActual = listaConjuntos.Where(x => x.RucConjunto == persona.RUC).FirstOrDefault().Torres.Where(x => x.NombreTorres == persona.Torre).FirstOrDefault().Departamentos.Where(w => w.CodigoDepartamento == persona.Departamento).FirstOrDefault();

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
