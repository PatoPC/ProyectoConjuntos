using DTOs.AreasDepartamento;
using DTOs.Conjunto;
using DTOs.Departamento;
using DTOs.MaestroContable;
using DTOs.MaestroContable.Archivo;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_MaestroContableController : Controller
    {
        private const string controladorActual = "C_MaestroContable";
        private const string accionActual = "GestionarMaestro";

        private readonly IServicioConsumoAPI<MaestroContableDTOCrear> _servicioConsumoAPICrear;
        private readonly IServicioConsumoAPI<List<MaestroContableDTOCrear>> _servicioConsumoAPICrearList;
        private readonly IServicioConsumoAPI<MaestroContableDTOCompleto> _servicioConsumoAPICompleto;

        public C_MaestroContableController(IServicioConsumoAPI<MaestroContableDTOCrear> servicioConsumoAPICrear, IServicioConsumoAPI<MaestroContableDTOCompleto> servicioConsumoAPICompleto, IServicioConsumoAPI<List<MaestroContableDTOCrear>> servicioConsumoAPICrearList)
        {
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
            _servicioConsumoAPICompleto = servicioConsumoAPICompleto;
            _servicioConsumoAPICrearList = servicioConsumoAPICrearList;
        }

        public IActionResult GestionarMaestro()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region CRUD

        #region Crear
        [HttpGet]
        public IActionResult CrearMaestroContable()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CrearMaestroContable(MaestroContableDTOCrear objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI, HttpMethod.Post, objModeloVista);

                if (respuesta.IsSuccessStatusCode)
                {
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

        [HttpGet]
        public ActionResult CargarMaestroDesdeArchivo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
                return View();


            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        public async Task<ActionResult> CargarMaestroDesdeArchivo(string produccion)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<ModeloArchivoMaestro> listaArchivoLeido = await construirMaestroArchivo(FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion));

                List<MaestroContableDTOCrear> listaMaestro = construirDTOMaestroMayor(objUsuarioSesion, listaArchivoLeido);

                HttpResponseMessage respuesta = await _servicioConsumoAPICrearList.consumoAPI(ConstantesConsumoAPI.apiCrearListaMaestro, HttpMethod.Post, listaMaestro);

                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, controladorActual, accionActual));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        public async Task<List<ModeloArchivoMaestro>> construirMaestroArchivo(string usuarioCreacion)
        {
            IFormFileCollection archivosFormulario = Request.Form.Files;
            List<ModeloArchivoMaestro> listaArchivoLeido = new List<ModeloArchivoMaestro>();

            if (archivosFormulario != null)
            {
                foreach (var archivo in archivosFormulario)
                {
                    if (archivo.Length > 0)
                    {
                        listaArchivoLeido = await LeerArchivoMaestro.procesarArchivoExcel(archivo, usuarioCreacion);
                    }
                }
            }

            return listaArchivoLeido;
        }

        public List<MaestroContableDTOCrear> construirDTOMaestroMayor(UsuarioSesionDTO objUsuarioSesion, List<ModeloArchivoMaestro> listaArchivoLeido)
        {
            string usuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

            List<MaestroContableDTOCrear> lista = new List<MaestroContableDTOCrear>();

            lista = listaArchivoLeido.GroupBy(x => x.ctacont, (key, group) => group.First()).
                          Select(x => new MaestroContableDTOCrear
                          {
                              CuentaCon = x.ctacont,
                              NombreCuenta = x.nom_cuenta,
                              Grupo = x.grupo=="0" ? false : true,                              
                              UsuarioCreacion = usuarioCreacion,
                              FechaCreacion = DateTime.Now
                          }).ToList();
           
            return lista;
        }

        #endregion

        #region Editar
        [HttpGet]
        public async Task<ActionResult> EditarMaestroContable(Guid idMaestro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + idMaestro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    MaestroContableDTOCompleto objMaestroContable = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objMaestroContable);

                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        //public async Task<ActionResult> EditarMaestroContable(Guid IdConMst, MaestroContableDTOEditar objModeloVista)
        public async Task<ActionResult> EditarMaestroContable(MaestroContableDTOCompleto objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICompleto.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPIEditar + objModeloVista.IdConMst, HttpMethod.Post, objModeloVista);

                if (respuesta.IsSuccessStatusCode)
                {
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

        #region Eliminar
        [HttpGet]
        public async Task<ActionResult> EliminarMaestroContable(Guid idMaestro)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarMaestroContableAPI + idMaestro, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    MaestroContableDTOCompleto objMaestroContable = await LeerRespuestas<MaestroContableDTOCompleto>.procesarRespuestasConsultas(respuesta);

                    return View(objMaestroContable);
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }


        [HttpPost]
        public async Task<ActionResult> EliminarMaestroContable(MaestroContableDTOCompleto objModeloVista)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objModeloVista.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                HttpResponseMessage respuesta = await _servicioConsumoAPICompleto.consumoAPI(ConstantesConsumoAPI.gestionarMaestroConableAPIEliminar + objModeloVista.IdConMst, HttpMethod.Post, objModeloVista);

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

        #region Consultas
        [HttpGet]
        public async Task<ActionResult> BusquedaMaestroContable()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<MaestroContableDTOCompleto> listaResultado = new List<MaestroContableDTOCompleto>();

                try
                {
                    HttpResponseMessage respuesta = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.buscarMaestroContableAvanzado, HttpMethod.Get);

                    if (respuesta.IsSuccessStatusCode)
                        listaResultado = await LeerRespuestas<List<MaestroContableDTOCompleto>>.procesarRespuestasConsultas(respuesta);
                }
                catch (Exception ex)
                {

                }

                if (listaResultado == null)
                    listaResultado = new List<MaestroContableDTOCompleto>();

                return View("_ListaMaestroContable", listaResultado);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #endregion
    }
}
