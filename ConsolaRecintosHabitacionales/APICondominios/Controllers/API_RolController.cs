using APICondominios.Model;
using AutoMapper;
using DTOs.Roles;
using GestionUsuarioDB.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioGestionUsuarios.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/Rol")]
    [ApiController]
    public class API_RolController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IManageCRUDPermisos<Rol> _manageRol;
        private readonly IManageCRUDPermisos<Modulo> _manageModulo;
        private readonly IManageCRUDPermisos<Menu> _manageMenu;
        private readonly IManageCRUDPermisos<Permiso> _managePermiso;
        private readonly IManageConsultasPermisos _manageRolConsultas;
        private readonly IManageLogError _logError;
        public API_RolController(IMapper mapper, IManageCRUDPermisos<Rol> manageRol, IManageConsultasPermisos manageRolConsultas, IManageLogError logError, IManageCRUDPermisos<Modulo> manageModulo, IManageCRUDPermisos<Menu> manageMenu, IManageCRUDPermisos<Permiso> managePermiso)
        {
            _mapper = mapper;
            _manageRol = manageRol;
            _manageRolConsultas = manageRolConsultas;
            _logError = logError;
            _manageModulo = manageModulo;
            _manageMenu = manageMenu;
            _managePermiso = managePermiso;
        }

        #region CRUD

        #region Create
        [HttpPost("Create")]
        [HttpHead]
        public async Task<IActionResult> Create([FromBody] RolDTOCrear objRolCreadorDTO)
        {
            try
            {
                if (objRolCreadorDTO == null)                
                    return BadRequest();
                
                Rol objetoDB = _mapper.Map<Rol>(objRolCreadorDTO);
                _manageRol.Add(objetoDB);

                var result = await _manageRol.save();
                if (result.estado)
                {
                    RolDTOCrear objCatalogoResult = _mapper.Map<RolDTOCrear>(objetoDB);
                    return CreatedAtRoute("GetRolByID", new { IdRol = objetoDB.IdRol }, objCatalogoResult);
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objRolCreadorDTO), result.mensajeError);
                }
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objRolCreadorDTO), ex.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }
        #endregion

        #region Edit
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid idRol, RolDTOEditar objDTO)
        {
            try
            {
                if (objDTO == null)                
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                
                Rol objDB = await _manageRolConsultas.GetRolByID(idRol);

                try
                {
                    foreach (var modulo in objDB.Modulos)
                    {
                        foreach (var menu in modulo.Menus)
                        {
                            _manageMenu.Delete(menu);

                            foreach (var permiso in menu.Permisos)
                            {
                                _managePermiso.Delete(permiso);
                            }
                        }
                        _manageModulo.Delete(modulo);
                    }

                    var elminarModulo = await _manageModulo.save();
                    var elminarMenu = await _manageMenu.save();
                    var elminarPermiso = await _managePermiso.save();
                }
                catch (Exception ex)
                {

                }

                List<Modulo> objetoModulosDB = _mapper.Map<List<Modulo>>(objDTO.listaModulos);
                _mapper.Map(objDTO, objDB);

                var listaModulos = _manageRol.EditRol(objDB, objetoModulosDB);

                //objDB.Modulos = listaModulos;

                var result = await _manageRol.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                {
                    return NoContent();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                    return BadRequest(MensajesRespuesta.guardarError());
                }
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);

        }
        #endregion

        #region Delete
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid idRol)
        {
            try
            {

                Rol objDB = await _manageRolConsultas.GetRolByID(idRol);

                try
                {
                    foreach (var modulo in objDB.Modulos)
                    {
                        foreach (var menu in modulo.Menus)
                        {                            
                            foreach (var permiso in menu.Permisos)
                            {
                                _managePermiso.Delete(permiso);                                
                            }
                            _manageMenu.Delete(menu);                            
                        }
                        _manageModulo.Delete(modulo);                        
                    }

                    var elminarPermiso = await _managePermiso.save();
                    //var elminarMenu = await _manageMenu.save();
                    //var elminarModulo = await _manageModulo.save();
                }
                catch (Exception ex)
                {
                }
                _manageRol.Delete(objDB);
                var result = await _manageRol.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)                
                    return NoContent();                
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objDB), result.mensajeError);
                    return BadRequest(MensajesRespuesta.guardarError());
                }
            }
            catch (Exception ex)
            {
                await guardarLogs(idRol.ToString(), ex.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);

        }
        #endregion

        #endregion

        #region Get all Rols
        [HttpGet("GetAllRolsByConjunto")]
        public async Task<ActionResult<RolDTOBusqueda>> GetAllRolsByCompany()
        {
            try
            {
                List<Rol> listRols = await _manageRolConsultas.GetAllRolsByConjuntos();

                if (listRols.Count < 1)                
                    return NotFound(MensajesRespuesta.sinResultados());
                

                List<RolDTOBusqueda> listRolsDTO = _mapper.Map<List<RolDTOBusqueda>>(listRols);

                return Ok(listRolsDTO);
            }
            catch (Exception ex)
            {
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetRolPorNombre")]
        public async Task<ActionResult<RolDTOBusqueda>> GetRolPorNombre(string nombreRol)
        {
            try
            {
                List<Rol> objRepositorio = await _manageRolConsultas.GetRolPorNombre(nombreRol);

                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());
                

                List<RolDTOBusqueda> listaDTO = _mapper.Map<List<RolDTOBusqueda>>(objRepositorio);

                return Ok(listaDTO);
            }
            catch (Exception ex)
            {
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetRolPorNombreExacto")]
        public async Task<ActionResult<RolDTOBusqueda>> GetRolPorNombreExacto(string nombreRolExacto)
        {
            try
            {
                Rol objRepositorio = await _manageRolConsultas.GetRolPorNombreExacto(nombreRolExacto);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());


                RolDTOBusqueda listaDTO = _mapper.Map<RolDTOBusqueda>(objRepositorio);

                return Ok(listaDTO);
            }
            catch (Exception ex)
            {
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }




        [HttpGet("{IdRol}", Name = "GetRolByID")]
        public async Task<IActionResult> GetRolByID(Guid IdRol)
        {
            try
            {
                Rol objDB = await _manageRolConsultas.GetRolByID(IdRol);

                if (objDB == null)                
                    return NotFound(MensajesRespuesta.sinResultados());
                

                RolDTOCompleto obj = _mapper.Map<RolDTOCompleto>(objDB);

                return Ok(obj);
            }
            catch (Exception ex)
            {
            }

            return BadRequest(MensajesRespuesta.errorInesperado());
        }
        #endregion

        #region Varios
        private async Task guardarLogs(string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(_logError);

            await objLooger.guardarError(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), mensajeError, objetoJSON);

        }
        #endregion

    }//class
}
