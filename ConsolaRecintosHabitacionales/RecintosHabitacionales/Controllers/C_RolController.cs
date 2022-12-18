using AutoMapper;
using DTOs.Roles;
using DTOs.Select;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecintosHabitacionales.Filters;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio.Interface;
using RecintosHabitacionales.Servicio;
using Utilitarios;
using DTOs.CatalogoGeneral;
using DTOs.Conjunto;
using DTOs.Modulo;
using DTOs.Menu;

namespace RecintosHabitacionales.Controllers
{
    public class C_RolController : Controller
    {
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;
        private readonly IServicioConsumoAPI<ResultadoBusquedaConjuntos> _servicioConsumoAPIEmpresa;
        private readonly IServicioConsumoAPI<CatalogoDTOPaginaDropDown> _servicioConsumoAPICatalogosPagina;
        private readonly IServicioConsumoAPI<RolDTOCrear> _servicioConsumoAPI;
        private readonly IServicioConsumoAPI<RolDTOBusqueda> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<RolDTOEditar> _servicioConsumoAPIUpdate;
        private readonly IMapper _mapper;

        public C_RolController(IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IMapper mapper, IServicioConsumoAPI<RolDTOCrear> servicioConsumoAPI, IServicioConsumoAPI<RolDTOBusqueda> servicioConsumoAPIBusqueda, IServicioConsumoAPI<RolDTOEditar> servicioConsumoAPIUpdate, IServicioConsumoAPI<CatalogoDTOPaginaDropDown> servicioConsumoAPICatalogosPagina, IServicioConsumoAPI<ResultadoBusquedaConjuntos> servicioConsumoAPIEmpresa)
        {
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _mapper = mapper;
            _servicioConsumoAPI = servicioConsumoAPI;
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPIUpdate = servicioConsumoAPIUpdate;
            _servicioConsumoAPICatalogosPagina = servicioConsumoAPICatalogosPagina;
            _servicioConsumoAPIEmpresa = servicioConsumoAPIEmpresa;
        }

        //[AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Administrar Rol", tipoPermiso = "Lectura", concedido = true)]
        public async Task<ActionResult> AdministracionRol()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GetAllRolsByConjunto, HttpMethod.Get);
                var lista = await LeerRespuestas<List<RolDTOBusqueda>>.procesarRespuestasConsultas(respuesta);

                ViewData["listaRoles"] = lista;

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }//AdministracionRol


        #region Crear
        //[AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Escritura", concedido = true)]
        public async Task<IActionResult> CrearRol()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                await cargarInicial(objUsuarioSesion);
                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        //[AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Escritura", concedido = true)]
        public async Task<IActionResult> CrearRol([FromBody] RolDTOCompleto listaRoles)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                listaRoles.UsuarioCreacion = FuncionesUtiles.consturirUsuarioAuditoria(objUsuarioSesion);

                listaRoles.listaModulos = listaRoles.listaModulos.Where(x => x != null).ToList();

                RolDTOCrear objCrear = new RolDTOCrear();

                try
                {
                    _mapper.Map(listaRoles, objCrear);
                }
                catch (Exception ex)
                {

                }

                HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getGetRolCreate, HttpMethod.Post, objCrear);

                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "C_Rol", "AdministracionRol"));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #endregion

        #region Editar
        //[AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Edición", concedido = true)]
        public async Task<IActionResult> EditarRol(Guid IdRol)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                RolDTOCompleto objRol = await recuperarRolPorID(IdRol);
                await cargarInicial(objUsuarioSesion, objRol.IdPaginaInicioRol);

                return View(objRol);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        //[AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Edición", concedido = true)]
        public async Task<IActionResult> EditarRol([FromBody] RolDTOCompleto listaRoles)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                listaRoles.UsuarioModificacion = FuncionesUtiles.consturirUsuarioAuditoria(objUsuarioSesion);

                listaRoles.listaModulos = listaRoles.listaModulos.Where(x => x != null).ToList();

                foreach (ModuloDTOCompleto objModulo in listaRoles.listaModulos)
                {
                    objModulo.IconoModulo = await recuperarIconoModulo(objModulo.Nombre);
                    foreach (MenuDTOCompleto menu in objModulo.Menus)
                    {
                        menu.Datoicono = await recuperarIconoModulo(menu.NombreMenu);
                    }
                }

                RolDTOEditar objEditar = new RolDTOEditar();

                try
                {
                    _mapper.Map(listaRoles, objEditar);
                }
                catch (Exception ex)
                {

                }

                HttpResponseMessage respuesta = await _servicioConsumoAPIUpdate.consumoAPI(ConstantesConsumoAPI.getGetRolEditar + listaRoles.IdRol, HttpMethod.Post, objEditar);

                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "C_Rol", "AdministracionRol"));
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        #endregion Editar

        #region Detalle

        [AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Lectura", concedido = true)]
        public async Task<IActionResult> DetalleRol(Guid IdRol)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                RolDTOCompleto objRol = await recuperarRolPorID(IdRol);
                await cargarInicial(objUsuarioSesion, objRol.IdPaginaInicioRol);

                return View(objRol);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        #endregion

        #region EliminarRol
        [HttpGet]
        [AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Eliminar", concedido = true)]
        public async Task<IActionResult> EliminarRol(Guid IdRol)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                RolDTOCompleto objRol = await recuperarRolPorID(IdRol);
                await cargarInicial(objUsuarioSesion, objRol.IdPaginaInicioRol);

                return View(objRol);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        [HttpPost]
        [AccionesFiltro(nombreModulo = "Configuración", nombreMenu = "Roles", tipoPermiso = "Eliminar", concedido = true)]
        public async Task<IActionResult> EliminarRol(Guid IdRol, bool eliminar)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIUpdate.consumoAPI(ConstantesConsumoAPI.getGetRolEliminar + IdRol, HttpMethod.Delete);

            if (respuesta.IsSuccessStatusCode)
            {
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "C_Rol", "AdministracionRol", true));
            }
            //return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "", "", true));
            return new JsonResult(MensajesRespuesta.guardarErrorDatosDuplicados("<div>Error no se puede eliminar un rol asignado a un usuario, por favor verifique que no existen usuarios con este rol.</div>", "error", false));
        }

        #endregion

        #region Consultas
        private async Task<string> recuperarIconoModulo(string nomobreModulo)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.getNombreCatalogo + nomobreModulo, HttpMethod.Get);

            List<CatalogoDTOCompleto> lista = await LeerRespuestas<List<CatalogoDTOCompleto>>.procesarRespuestasConsultas(respuesta);

            CatalogoDTOCompleto objModulo = new CatalogoDTOCompleto();
            objModulo.DatoIcono = "";

            if (lista != null)
            {
                if (lista.Count() > 0)
                {
                    objModulo = lista.FirstOrDefault();
                }
            }

            return objModulo.DatoIcono;
        }

        private async Task<RolDTOCompleto> recuperarRolPorID(Guid IdRol)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.endPointRolByID + IdRol, HttpMethod.Get);

            RolDTOCompleto objRol = await LeerRespuestas<RolDTOCompleto>.procesarRespuestasConsultas(respuesta);

            return objRol;
        }


        public async Task<JsonResult> BusquedaRolesPorEmpresaID(Guid IdConjunto)
        {
            if (IdConjunto != ConstantesAplicacion.guidNulo)
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.GetAllRolsByConjunto, HttpMethod.Get);
                var lista = await LeerRespuestas<List<RolDTOBusqueda>>.procesarRespuestasConsultas(respuesta);

                var listaDropDown = lista.Select(x => new ObjetoSelectDropDown { id = x.IdRol.ToString(), texto = x.NombreRol }).ToList();

                return new JsonResult(listaDropDown);
            }

            return new JsonResult(FuncionesUtiles.crearListaOpcionesEnBlanco);
        }

        public async Task<IActionResult> BusquedaAvanzadaRol(RolDTOBusqueda objBusqueda)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getRolPorNombre+objBusqueda.NombreRol, HttpMethod.Get);
            var lista = await LeerRespuestas<List<RolDTOBusqueda>>.procesarRespuestasConsultas(respuesta);

            if (lista == null)
                lista = new List<RolDTOBusqueda>();


            return View("_ListaRoles", lista);
        }//AdministracionRol

        #endregion

        public async Task cargarInicial(UsuarioSesionDTO objUsuarioSesion, Guid? idPaginaDefault = null)
        {
            //ViewData["idEmpresa"] = objUsuarioSesion.IdEmpresadefault;
            ViewData["listaModulos"] = await DropDownsCatalogos<CatalogoDTODropDown>.cargarListaDropDownGenerico(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreModulo, "IdCatalogo", "Nombrecatalogo");

            HttpResponseMessage respuesta = await _servicioConsumoAPICatalogos.consumoAPI(ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padrePermisos, HttpMethod.Get);

            string listaPermisos = await LeerRespuestas<string>.procesarRespuestasJSON(respuesta);

            ViewData["listaPermisos"] = listaPermisos;

            List<ResultadoBusquedaConjuntos> ListaConjuntosAcceso = new List<ResultadoBusquedaConjuntos>();
            foreach (var item in objUsuarioSesion.ConjutosAccesoSelect)
            {
                ResultadoBusquedaConjuntos empresaTemporal = new();

                empresaTemporal.IdConjunto = Guid.Parse(item.Value);
                empresaTemporal.NombreConjunto = item.Text;

                ListaConjuntosAcceso.Add(empresaTemporal);
            }

            var selectConjuntos = new SelectList(ListaConjuntosAcceso, "IdConjunto", "NombreConjunto");
            ViewData["listaConjuntos"] = selectConjuntos;

            var listaCatalogos = await DropDownsCatalogos<CatalogoDTOPaginaDropDown>.procesarRespuestasConsultaCatlogoObjeto(_servicioConsumoAPICatalogosPagina, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padrePaginasRoles);

            var selectRolesPaginas = new SelectList(listaCatalogos, "IdCatalogo", "NombrePaginaDefault", idPaginaDefault);
            ViewData["listaPaginasInicioRol"] = selectRolesPaginas;
        }

    }//class
}
