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

namespace RecintosHabitacionales.Controllers
{
    public class C_ConjuntosController : Controller
    {
        private const string controladorActual = "C_Conjuntos";
        private const string accionActual = "AdministrarConjuntos";

        private readonly IServicioConsumoAPI<ConjuntoDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<UsuarioConjuntoDTO> _servicioConsumoAPIUsuarioConjunto;
        private readonly IServicioConsumoAPI<ConjuntoDTOEditar> _servicioConsumoAPICrearEditar;
        private readonly IServicioConsumoAPI<BusquedaConjuntos> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusquedaTorres;

        private readonly IServicioConsumoAPI<DepartamentoDTOEditar> _servicioConsumoAPIDepartamentoEditar;

        private readonly IServicioConsumoAPI<DepartamentoDTOCrear> _servicioConsumoAPIDepartamento;

        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;

        public C_ConjuntosController(IServicioConsumoAPI<ConjuntoDTOCrear> servicioConsumoAPIConjunto, IServicioConsumoAPI<BusquedaConjuntos> servicioConsumoAPIBusqueda, IServicioConsumoAPI<ConjuntoDTOEditar> servicioConsumoAPIConjuntoEditar, IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusquedaTorres, IServicioConsumoAPI<DepartamentoDTOCrear> servicioConsumoAPIDepartamento, IServicioConsumoAPI<DepartamentoDTOEditar> servicioConsumoAPIDepartamentoEditar, IServicioConsumoAPI<UsuarioConjuntoDTO> servicioConsumoAPIUsuarioConjunto, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos)
        {
            _servicioConsumoAPICrear = servicioConsumoAPIConjunto;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrearEditar = servicioConsumoAPIConjuntoEditar;
            _servicioConsumoAPIBusquedaTorres = servicioConsumoAPIBusquedaTorres;
            _servicioConsumoAPIDepartamento = servicioConsumoAPIDepartamento;
            _servicioConsumoAPIDepartamentoEditar = servicioConsumoAPIDepartamentoEditar;
            _servicioConsumoAPIUsuarioConjunto = servicioConsumoAPIUsuarioConjunto;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
        }

        #region CRUD

        #region CrearConjuntos
        public IActionResult CrearConjuntos()
        {
            return View();
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

                    HttpResponseMessage respuestaConjuntoUsuairo = await _servicioConsumoAPIUsuarioConjunto.consumoAPI(ConstantesConsumoAPI.getCreateUsuarioConjunto, HttpMethod.Post, objDTOUsuario);

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

                if(listaTipoAreaDepartamentoCrear != null && IdTipoAreaCrear != null)
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

        public async Task cargaInicial()
        {
            ViewData["listaTipoAreas"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoAreas, "IdCatalogo", "Nombrecatalogo");
        }

    }
}
