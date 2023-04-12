using AutoMapper;
using DTOs.CatalogoGeneral;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio.Interface;
using RecintosHabitacionales.Servicio;
using Utilitarios;
using DTOs.Conjunto;
using DTOs.Select;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace RecintosHabitacionales.Controllers
{
    public class C_CatalogoController : Controller
    {
        private readonly IServicioConsumoAPI<CatalogoDTOCrear> _servicioConsumoAPI;
        private readonly IServicioConsumoAPI<CatalogoDTOActualizar> _servicioConsumoAPIUpdate;
        private readonly IServicioConsumoAPI<ResultadoBusquedaConjuntos> _servicioConsumoAPIEmpresa;

        private readonly IMapper _mapper;
        public C_CatalogoController(IMapper mapper = null, IServicioConsumoAPI<CatalogoDTOCrear> servicioConsumoAPI = null, IServicioConsumoAPI<CatalogoDTOActualizar> servicioConsumoAPIUpdate = null, IServicioConsumoAPI<ResultadoBusquedaConjuntos> servicioConsumoAPIEmpresa = null)
        {
            _mapper = mapper;
            _servicioConsumoAPI = servicioConsumoAPI;
            _servicioConsumoAPIUpdate = servicioConsumoAPIUpdate;
            _servicioConsumoAPIEmpresa = servicioConsumoAPIEmpresa;
        }
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Lectura", concedido = true)]
        public async Task<IActionResult> GestionCatalogos()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            if (objUsuarioSesion != null)
            {
            await cargaInicial(objUsuarioSesion);
            

            return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #region Crear
        [HttpGet]
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Escritura", concedido = true)]
        public IActionResult CrearCatalogo()
        {
            return View();
        }

        [HttpPost]
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Escritura", concedido = true)]
        public async Task<IActionResult> CrearCatalogo(CatalogoDTOCrear objCatalogo)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objCatalogo.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                if (objCatalogo.IdCatalogopadre != null && objCatalogo.IdCatalogopadre != ConstantesAplicacion.guidNulo)
                {
                    HttpResponseMessage respuestaCatlogoPadre = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + objCatalogo.IdCatalogopadre, HttpMethod.Get);

                    CatalogoDTOCompleto objCatalogoPadre = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaCatlogoPadre);

                    if (objCatalogoPadre != null)
                    {
                        objCatalogo.NivelCatalogo = objCatalogoPadre.NivelCatalogo + 1;
                    }
                }
                else
                {
                    objCatalogo.NivelCatalogo = 0;
                }

                HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosCreate, HttpMethod.Post, objCatalogo);

                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion

        #region Editar

        [HttpGet]
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Editar", concedido = true)]
        public async Task<IActionResult> EditarCatalogo(Guid idCatalogo)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + idCatalogo, HttpMethod.Get);

            CatalogoDTOCompleto objCatalogo = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuesta);

            await cargaInicial(objUsuarioSesion);

            return View(objCatalogo);
        }

        [HttpPost]
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Editar", concedido = true)]
        public async Task<IActionResult> EditarCatalogo(Guid IdCatalogo, CatalogoDTOActualizar objCatalogo)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);
            if (IdCatalogo == objCatalogo.IdCatalogopadre)
            {
                return new JsonResult(MensajesRespuesta.errorCatalogoPadreRecursivo());
            }


            objCatalogo.Usuariomodificacion = objUsuarioSesion.Nombre;

            if (objCatalogo.IdCatalogopadre != null && objCatalogo.IdCatalogopadre != ConstantesAplicacion.guidNulo)
            {
                HttpResponseMessage respuestaCatlogoPadre = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + objCatalogo.IdCatalogopadre, HttpMethod.Get);

                CatalogoDTOCompleto objCatalogoPadre = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuestaCatlogoPadre);

                if (objCatalogoPadre != null)
                {
                    objCatalogo.NivelCatalogo = objCatalogoPadre.NivelCatalogo + 1;
                }
            }
            else
            {
                objCatalogo.NivelCatalogo = 0;
            }

            HttpResponseMessage respuestaEditar = await _servicioConsumoAPIUpdate.consumoAPI(ConstantesConsumoAPI.getEditCatalogo + IdCatalogo, HttpMethod.Put, objCatalogo);



            return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuestaEditar, "C_Catalogo", "GestionCatalogos"));
        }
        #endregion


        [HttpGet]
        //[AccionesFiltro(nombreModulo = "Configuraciones", nombreMenu = "Catálogo", tipoPermiso = "Lectura", concedido = true)]
        public async Task<IActionResult> DetalleCatalogo(Guid idCatalogo)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + idCatalogo, HttpMethod.Get);

                CatalogoDTOCompleto objCatalogo = await LeerRespuestas<CatalogoDTOCompleto>.procesarRespuestasConsultas(respuesta);

                await cargaInicial(objUsuarioSesion);

                return View(objCatalogo);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        private async Task cargaInicial(UsuarioSesionDTO objUsuarioSesion)
        //private async Task cargaInicial()
        {
            if (objUsuarioSesion != null)
            {
                List<CatalogoDTOResultadoBusqueda> listaCatalogosOrganizada = await RecuperarListaCatalogos();

                //Cargar todos los catalogos
                ViewData["listaTodosCatalogos"] = listaCatalogosOrganizada;

                if (objUsuarioSesion.ListaConjuntos == null)
                {
                    //SelectList listaConjuntos = await DropDownsCatalogos<ResultadoBusquedaConjuntos>.cargarListaDropDownGenerico(_servicioConsumoAPIEmpresa, ConstantesConsumoAPI.TodosConjuntos, "IdConjunto", "NombreConjunto", objUsuarioSesion.IdConjuntoDefault);
                    //objUsuarioSesion.ListaConjuntos = _mapper.Map<CustomSelectConjuntos>(listaConjuntos);

                    HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.TodosConjuntos, HttpMethod.Get);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        List<ResultadoBusquedaConjuntos> listaConjuntosAcceso = await LeerRespuestas<List<ResultadoBusquedaConjuntos>>.procesarRespuestasConsultas(respuesta);

                        SelectList SelectListaConjuntos = new SelectList(listaConjuntosAcceso, "IdConjunto", "NombreConjunto", objUsuarioSesion.IdConjuntoDefault);

                        objUsuarioSesion.ListaConjuntosAcceso = listaConjuntosAcceso;
                        objUsuarioSesion.ListaConjuntos = _mapper.Map<CustomSelectConjuntos>(SelectListaConjuntos);

                        ViewData["listaConjuntos"] = SelectListaConjuntos;

                    }
                    else
                    {
                        List<ResultadoBusquedaConjuntos> listaConjuntosAcceso = new List<ResultadoBusquedaConjuntos>();

                        SelectList SelectListaConjuntos = new SelectList(listaConjuntosAcceso, "IdConjunto", "NombreConjunto", objUsuarioSesion.IdConjuntoDefault);

                        objUsuarioSesion.ListaConjuntosAcceso = listaConjuntosAcceso;
                        objUsuarioSesion.ListaConjuntos = _mapper.Map<CustomSelectConjuntos>(SelectListaConjuntos);

                        ViewData["listaConjuntos"] = SelectListaConjuntos;

                    }
                    

                    SesionExtensions.SetObject(HttpContext.Session, ConstantesAplicacion.nombreSesion, objUsuarioSesion);
                }
                else
                {
                    SelectList objSelect = _mapper.Map<SelectList>(objUsuarioSesion.ListaConjuntos);
                    ViewData["listaConjuntos"] = objSelect;
                }
            }
        }

        #region Búsquedas
        public async Task<IActionResult> BusquedaPorCodigo(string objCodigo, Guid idConjunto)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + objCodigo + "&idConjunto" + idConjunto, HttpMethod.Get);

            List<CatalogoDTOResultadoBusqueda> listaCatalogo = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuesta);

            return View("_ListaCatalogo", listaCatalogo);
        }

        public async Task<IActionResult> BusquedaTodosCatalogosPorIDEmpresa(Guid IdConjuntoBusqueda)
        {
            List<CatalogoDTOResultadoBusqueda> listaCatalogosOrganizada = await RecuperarListaCatalogos(IdConjuntoBusqueda);

            return View("_ListaCatalogo", listaCatalogosOrganizada);
        }

        public async Task<List<CatalogoDTOResultadoBusqueda>> RecuperarListaCatalogos(Guid? IdConjuntoBusqueda = null)
        {
            HttpResponseMessage respuesta = new HttpResponseMessage();

            if (IdConjuntoBusqueda != null)
            {
                respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.obtenerCatalogoPorIDConjuntos + IdConjuntoBusqueda, HttpMethod.Get);
            }
            else
            {
                respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.obtenerTodosLosCatalogo, HttpMethod.Get);
            }

            List<CatalogoDTOResultadoBusqueda> listaCatalogo = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuesta);

            //HttpResponseMessage respuestaCatalogos = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.obtenerTodosLosCatalogo, HttpMethod.Get);

            //List<CatalogoResultadoBusqueda> listaCatalogoSinEmpresa = await LeerRespuestas<List<CatalogoResultadoBusqueda>>.procesarRespuestasConsultas(respuestaCatalogos);

            //listaCatalogo = listaCatalogo.Union(listaCatalogoSinEmpresa).ToList();

            if (listaCatalogo == null)
            {
                listaCatalogo = new List<CatalogoDTOResultadoBusqueda>();
            }

            List<CatalogoDTOResultadoBusqueda> listaCatalogosOrganizada = listaCatalogo.Where(x => x.IdCatalogopadre == null).ToList();

            return listaCatalogosOrganizada;
        }

        public async Task<List<CatalogoDTODropDown>> RecuperarHijosPorIDCatologPadreIDEmpresa(Guid IdConjuntoBusqueda, Guid idCatalogoPadre)
        {
            if (IdConjuntoBusqueda == ConstantesAplicacion.guidNulo)
            {
                var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

                if (objUsuarioSesion != null)
                    IdConjuntoBusqueda = objUsuarioSesion.IdConjuntoDefault;
            }

            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorIDCatalogoPadre + idCatalogoPadre + "&idConjunto=" + IdConjuntoBusqueda, HttpMethod.Get);

            List<CatalogoDTODropDown> listaCatalogos = await LeerRespuestas<List<CatalogoDTODropDown>>.procesarRespuestasConsultas(respuestaCatalogo);

            return listaCatalogos;
        }

        public async Task<List<CatalogoDTODropDown>> RecuperarHijosPorIDCatologPadreIDEmpresaRol(Guid IdConjuntoBusqueda, Guid idCatalogoPadre)
        {
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorIDCatalogoPadre + idCatalogoPadre + "&idConjunto=" + IdConjuntoBusqueda, HttpMethod.Get);

            List<CatalogoDTODropDown> listaCatalogos = await LeerRespuestas<List<CatalogoDTODropDown>>.procesarRespuestasConsultas(respuestaCatalogo);

            return listaCatalogos;
        }

        public async Task<List<CatalogoDTODropDown>> RecuperarCatalogoHijosPorcodigoCatalogo(Guid IdConjuntoBusqueda, string codigoCatalogoPadre)
        {
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + codigoCatalogoPadre + "&idConjunto=" + IdConjuntoBusqueda, HttpMethod.Get);

            List<CatalogoDTODropDown> listaCatalogos = await LeerRespuestas<List<CatalogoDTODropDown>>.procesarRespuestasConsultas(respuestaCatalogo);

            return listaCatalogos;
        }

        public async Task<List<CatalogoDTODropDown>> RecuperarCatalogoHijosPorNombre(Guid IdConjuntoBusqueda, string nombreCatalogo)
        {
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorNombre + nombreCatalogo + "&idConjunto=" + IdConjuntoBusqueda, HttpMethod.Get);

            List<CatalogoDTODropDown> listaCatalogos = await LeerRespuestas<List<CatalogoDTODropDown>>.procesarRespuestasConsultas(respuestaCatalogo);

            return listaCatalogos;
        }

        public async Task<JsonResult> cargarListaDropDownCiudades(Guid idPadreCatalogoCiudad)
        {
            HttpResponseMessage restapuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorHijosPorIDPadre + idPadreCatalogoCiudad, HttpMethod.Get);

            string responseJSON = await LeerRespuestas<HttpResponseMessage>.procesarRespuestasJSON(restapuestaCatalogo);
            var listaCatalogos = JsonConvert.DeserializeObject<List<CatalogoDTODropDown>>(responseJSON);

            return new JsonResult(listaCatalogos);
        }

        public async Task<JsonResult> recuperarCatalogoPorID(Guid idCatalogo)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorIdCatalogo + idCatalogo, HttpMethod.Get);
            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);

            return new JsonResult(objCatalogo);
        }

        public async Task<JsonResult> recuperarCatalogosPorIDCatalogoPadre(Guid idCatalogoPadre)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosPorHijosPorIDPadre + idCatalogoPadre, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<List<CatalogoDTOResultadoBusqueda>>.procesarRespuestasConsultas(respuesta);

            return new JsonResult(objCatalogo);
        }       


        public async Task<List<CatalogoDTODropDown>> RecuperarCatalogoHermanosPorIDCatalogo(Guid idCatalogo)
        {
            HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHermanosPorID + idCatalogo, HttpMethod.Get);

            List<CatalogoDTODropDown> listaCatalogos = await LeerRespuestas<List<CatalogoDTODropDown>>.procesarRespuestasConsultas(respuestaCatalogo);

            return listaCatalogos;
        }
        #endregion
    }
}
