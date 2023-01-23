using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Usuarios;
using GestionUsuarioDB.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioGestionUsuarios.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class API_UsuarioController : ControllerBase
    {
        private readonly IManageCRUDPermisos<Usuario> _CRUDRepository;
        private readonly IManageCRUDPermisos<UsuarioConjunto> _CRUDRepositoryUsuarioConjunto;
        private readonly IManageConsultasUsuario _UsuarioConsultasRepository;
        private readonly IManageConjuntos _Conjuntos;
        private readonly IMapper _mapper;

        private readonly IManagePersona _Consultas_Persona;
        //private readonly IManageConsultasCatalogos _Consultas_Catalogo;
        private readonly IManageLogError _logError;

        public API_UsuarioController(IManageConsultasUsuario UsuarioRepository, IMapper mapper, IManageCRUDPermisos<Usuario> cRUDRepository, IManageCRUDPermisos<UsuarioConjunto> cRUDRepositoryUsuarioConjuto = null, IManagePersona consultas_Persona = null, IManageLogError logError = null, IManageConjuntos conjuntos = null)
        {
            _UsuarioConsultasRepository = UsuarioRepository ?? throw new ArgumentException(nameof(UsuarioRepository));
            this._mapper = mapper;
            _CRUDRepository = cRUDRepository;
            _CRUDRepositoryUsuarioConjunto = cRUDRepositoryUsuarioConjuto;
            _Consultas_Persona = consultas_Persona;
            _logError = logError;
            _Conjuntos = conjuntos;
        }

        #region CRUD
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UsuarioDTOCrear objUsuarioDTO)
        {
            try
            {
                if (objUsuarioDTO == null)
                    return BadRequest();

                Usuario objUsuario = _mapper.Map<Usuario>(objUsuarioDTO);

                _CRUDRepository.Add(objUsuario);
                var result = await _CRUDRepository.save();

                if (result.estado)
                {
                    UsuarioDTOCompleto objUsuarioResult = _mapper.Map<UsuarioDTOCompleto>(objUsuario);
                    return CreatedAtRoute("GetUsuarioById", new { idUsuario = objUsuario.IdUsuario }, objUsuarioResult);
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), result.mensajeError);
                }
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), ex.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        } // end Create Usuario

        [HttpPost("CreateUsuarioConjunto")]
        public async Task<IActionResult> CreateUsuarioConjunto([FromBody] UsuarioConjuntoDTO objUsuarioDTO)
        {
            try
            {
                if (objUsuarioDTO == null)
                    return BadRequest();

                UsuarioConjunto objUsuario = _mapper.Map<UsuarioConjunto>(objUsuarioDTO);

                _CRUDRepositoryUsuarioConjunto.Add(objUsuario);
                var result = await _CRUDRepositoryUsuarioConjunto.save();

                if (result.estado)
                {
                    return Ok();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), result.mensajeError);
                }
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), ex.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        } // end Create Usuario

        [HttpPost("CreateUsuarioConjuntoLista")]
        public async Task<IActionResult> CreateUsuarioConjuntoLista([FromBody] List<UsuarioConjuntoDTO> listaObjUsuarioDTO)
        {
            try
            {
                if (listaObjUsuarioDTO == null)
                    return BadRequest();

                List<UsuarioConjunto> listaObjUsuario = _mapper.Map<List<UsuarioConjunto>>(listaObjUsuarioDTO);

                var result = await _CRUDRepositoryUsuarioConjunto.saveRangeUsuarioConjunto(listaObjUsuario);

                if (result.estado)                
                    return Ok();                
                else                
                    await guardarLogs(JsonConvert.SerializeObject(listaObjUsuarioDTO), result.mensajeError);
                
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(listaObjUsuarioDTO), ex.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        } // end Create Usuario



        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(Guid IdUsuario, UsuarioDTOEditar objUsuarioDTO)
        {
            try
            {
                if (objUsuarioDTO == null || IdUsuario == ConstantesAplicacion.guidNulo)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                Usuario objUsuaioRepository = await _UsuarioConsultasRepository.getUserById(IdUsuario);

                List<UsuarioConjunto> listaUsuariosConjuntos = objUsuaioRepository.UsuarioConjuntos.ToList();

                if (listaUsuariosConjuntos.Count()>0)
                {
                    _CRUDRepositoryUsuarioConjunto.DeleteRange(listaUsuariosConjuntos);

                    var resultado = await _CRUDRepositoryUsuarioConjunto.save();

                    if (!resultado.estado)
                    {
                        await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), resultado.mensajeError);
                        return BadRequest(MensajesRespuesta.guardarError());
                    }                 
                }    
                    

                _mapper.Map(objUsuarioDTO, objUsuaioRepository);
                List<UsuarioConjuntoDTO> usuariosConjuntos = _mapper.Map<List<UsuarioConjuntoDTO>>(objUsuarioDTO.UsuarioConjuntos);


                _CRUDRepository.Edit(objUsuaioRepository);
                var result = await _CRUDRepository.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                    return NoContent();
                else
                    await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), result.mensajeError);


                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), ExValidation.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid IdUsuario)
        {
            try
            {
                Usuario objUsuaioRepository = await _UsuarioConsultasRepository.getUserById(IdUsuario);

                _CRUDRepository.Delete(objUsuaioRepository);
                var result = await _CRUDRepository.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                    return NoContent();
                else
                    await guardarLogs(JsonConvert.SerializeObject(objUsuaioRepository), result.mensajeError);


                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(IdUsuario.ToString(), ExValidation.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        #endregion

        #region Varios

        private async Task guardarLogs(string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(_logError);

            await objLooger.guardarError(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), mensajeError, objetoJSON);

        }

        #endregion

        #region Login
        [HttpGet("LoginUsuario")]
        public async Task<IActionResult> LoginUsuario(string user, string contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(contrasena))
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                Persona objPersona = new Persona();
                Usuario objUsuario = new Usuario();

                if (user.Contains("@"))
                {
                    objUsuario = await _UsuarioConsultasRepository.getLoginUser(user, contrasena);

                    if (objUsuario != null)
                        objPersona = await _Consultas_Persona.obtenerPorIDPersona(objUsuario.IdPersona);
                }
                else
                {
                    objPersona = await _Consultas_Persona.obtenerPersonaPoNumeroIdentificacionExacta(user);
                    if (objPersona != null)
                        objUsuario = await _UsuarioConsultasRepository.getUserByIdPersonaPassword(objPersona.IdPersona, contrasena);
                }

                if (objPersona != null && objUsuario != null)
                {
                    if (objPersona == null)
                        return NotFound(MensajesRespuesta.usuarioContrasenaIncorrecta());

                    UsuarioSesionDTO usuarioDTO = _mapper.Map<UsuarioSesionDTO>(objUsuario);
                    usuarioDTO.Nombre = objPersona.NombresPersona;
                    usuarioDTO.Apellido = objPersona.ApellidosPersona;
                    usuarioDTO.IdPersona = objPersona.IdPersona;
                    usuarioDTO.NumeroIdentificacion = objPersona.IdentificacionPersona;

                    //Actualición ultimo ingreso
                    Usuario objUsuaioRepository = await _UsuarioConsultasRepository.getUserById(usuarioDTO.IdUsuario);
                    _CRUDRepository.EditUltimoIngreso(objUsuaioRepository);
                    var editarIngreso = await _CRUDRepository.save();

                    usuarioDTO.Fechaultimoingreso = objUsuaioRepository.FechaUltimoIngreso;

                    usuarioDTO.CorreoElectronico = usuarioDTO.CorreoElectronico != null ? usuarioDTO.CorreoElectronico.Trim() : "";

                    return Ok(usuarioDTO);
                }
                return Ok();
            }
            catch (Exception ex)
            {
            }

            return BadRequest(MensajesRespuesta.errorInesperado());
        } // LoginUsuario



        [HttpPost("UpdateCambioContrasenia")]
        public async Task<IActionResult> UpdateCambioContrasenia(Guid idUsuario, UsuarioCambioContrasena objUsuarioDTO)
        {
            try
            {
                if (objUsuarioDTO == null)
                    return BadRequest();


                Usuario objUsuarioRepositorio = await _UsuarioConsultasRepository.getUserById(idUsuario);

                if (objUsuarioRepositorio != null)
                {
                    _mapper.Map(objUsuarioDTO, objUsuarioRepositorio);

                    objUsuarioRepositorio.Contrasena = objUsuarioDTO.NuevaContrasena;
                    objUsuarioRepositorio.ContrasenaInicial = false;
                    //_repositorio.CambioContraseniaUsuario(objUsuarioRepositorio);
                    //var result = await _repositorio.GuardarUsuario();

                    _CRUDRepository.Edit(objUsuarioRepositorio);
                    var result = await _CRUDRepository.save();

                    if (result.estado)
                        return NoContent();
                    else
                    {
                        await guardarLogs(JsonConvert.SerializeObject(objUsuarioDTO), result.mensajeError);

                        return BadRequest(MensajesRespuesta.errorConexion());
                    }
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }


        #endregion

        #region Busquedas
        [HttpGet("{idUsuario}", Name = "GetUsuarioById")]
        public async Task<IActionResult> GetUsuarioById(Guid idUsuario)
        {
            try
            {
                Usuario objUsuario = await _UsuarioConsultasRepository.getUserById(idUsuario);

                if (objUsuario == null)
                    return NotFound();

                UsuarioDTOCompleto objUsuarioDTO = _mapper.Map<UsuarioDTOCompleto>(objUsuario);

                objUsuarioDTO = await completarDatosUsuario(objUsuarioDTO);

                return Ok(objUsuarioDTO);
            }
            catch (Exception ex)
            {
            }

            return BadRequest(MensajesRespuesta.errorInesperado());
        }

        [HttpGet("GetUsuariosPorIDPersona")]
        public async Task<IActionResult> GetUsuariosPorIDPersona(Guid idPersona)
        {
            try
            {
                Usuario objUsuario = await _UsuarioConsultasRepository.getUserByIdPersona(idPersona);

                if (objUsuario == null)
                    return NotFound(MensajesRespuesta.usuarioContrasenaIncorrecta());


                UsuarioDTOCompleto objUsuarioDTO = _mapper.Map<UsuarioDTOCompleto>(objUsuario);

                return Ok(objUsuarioDTO);
            }
            catch (Exception ex)
            {
            }

            return BadRequest(MensajesRespuesta.errorInesperado());
        }


        [HttpGet("GetUsuariosAdvanced")]
        public async Task<ActionResult<List<UsuarioResultadoBusquedaDTO>>> GetUsuariosAdvanced(ObjetoBusquedaUsuarios objBusqueda)
        {
            List<Usuario> listaUsuarios = await _UsuarioConsultasRepository.GetUserAdvanced(objBusqueda);

            if (listaUsuarios.Count < 1)
                return NotFound(MensajesRespuesta.sinResultados());

            List<UsuarioResultadoBusquedaDTO> listaResultadoDTO = _mapper.Map<List<UsuarioResultadoBusquedaDTO>>(listaUsuarios);

            listaResultadoDTO = await completarDatosUsuario(listaResultadoDTO, objBusqueda);

            return Ok(listaResultadoDTO);
        }

        [HttpGet("GetUsuariosbyIdentificacion")]

        public async Task<ActionResult<UsuarioResultadoBusquedaDTO>> GetUsuariosbyIdentificacion(string identificacion)
        {
            UsuarioResultadoBusquedaDTO usuarioResultadoBusquedaDTO = new();
            Persona objPersona = await _Consultas_Persona.obtenerPersonaPoNumeroIdentificacionExacta(identificacion);
            if (objPersona != null)
            {
                Usuario usuario = await _UsuarioConsultasRepository.getUserByIdPersona(objPersona.IdPersona);
                usuarioResultadoBusquedaDTO = _mapper.Map<UsuarioResultadoBusquedaDTO>(usuario);
                return Ok(usuarioResultadoBusquedaDTO);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion Busquedas

        #region Varios

        private async Task<UsuarioDTOCompleto> completarDatosUsuario(UsuarioDTOCompleto objDTO)
        {
            List<Conjunto> listaConjuntos = await _Conjuntos.busquedaTodosConjuntos();


            Persona objPersona = await _Consultas_Persona.obtenerPorIDPersona(objDTO.IdPersona);

            objDTO.Nombre = objPersona.NombresPersona;
            objDTO.Apellido = objPersona.ApellidosPersona;
            objDTO.numeroIdentificacion = objPersona.IdentificacionPersona;
            Conjunto objConjunto = listaConjuntos.Where(x => x.IdConjunto == objDTO.IdConjuntoDefault).FirstOrDefault();

            if (objConjunto!=null)
            {
                objDTO.NombreConjunto = objConjunto.NombreConjunto;

                foreach (var item in objDTO.UsuarioConjuntos)
                {
                    if (objConjunto!=null)
                    {
                        if (item.IdConjunto != objConjunto.IdConjunto)
                        {
                            Conjunto objConjuntoTemporal = listaConjuntos.Where(x => x.IdConjunto == item.IdConjunto).FirstOrDefault();

                            item.NombreConjunto = objConjuntoTemporal.NombreConjunto;
                        }
                        else
                            item.NombreConjunto = objConjunto.NombreConjunto; 
                    }
                    
                } 
            }

            return objDTO;
        }

        private async Task<List<UsuarioResultadoBusquedaDTO>> completarDatosUsuario(List<UsuarioResultadoBusquedaDTO> listaResultadoDTO, ObjetoBusquedaUsuarios objBusqueda)
        {
            List<Conjunto> listaConjuntos = await _Conjuntos.busquedaTodosConjuntos();

            foreach (var usuario in listaResultadoDTO)
            {
                Persona objPersona = await _Consultas_Persona.obtenerPorIDPersona(usuario.IdPersona);

                if (objBusqueda != null && objPersona != null)
                {
                    usuario.Nombre = objPersona.NombresPersona;
                    usuario.Apellido = objPersona.ApellidosPersona;
                    usuario.numeroIdentificacion = objPersona.IdentificacionPersona;
                }
            }

            if (!string.IsNullOrEmpty(objBusqueda.nombres))
            {
                listaResultadoDTO = listaResultadoDTO.Where(x => x.Nombre.ToUpper().Trim().Contains(objBusqueda.nombres.ToUpper().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(objBusqueda.apellidos))
            {
                listaResultadoDTO = listaResultadoDTO.Where(x => x.Apellido.ToUpper().Trim().Contains(objBusqueda.apellidos.ToUpper().Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(objBusqueda.numeroIdentificacion))
            {
                listaResultadoDTO = listaResultadoDTO.Where(x => x.numeroIdentificacion.Trim() == objBusqueda.numeroIdentificacion.Trim()).ToList();
            }

            var listaUsuariosPorConjunto = listaResultadoDTO.GroupBy(x => x.IdConjunto);
            List<UsuarioResultadoBusquedaDTO> listaResultadoDTOFinal = new();

            foreach (var item in listaUsuariosPorConjunto)
            {
                List<UsuarioResultadoBusquedaDTO> listaTemporal = new();
                Conjunto objConjunto = listaConjuntos.Where(x => x.IdConjunto == item.Key).FirstOrDefault();
                try
                {
                    listaTemporal = item.Select(x => new UsuarioResultadoBusquedaDTO
                    {
                        IdUsuario = x.IdUsuario,
                        IdPersona = x.IdPersona,
                        IdConjunto = x.IdConjunto,
                        Estado = x.Estado,
                        numeroIdentificacion = x.numeroIdentificacion,
                        Perfil = x.Perfil,
                        CorreoElectronico = x.CorreoElectronico,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,
                        UsuarioConjuntos = x.UsuarioConjuntos,
                        NombreConjunto = objConjunto!=null ? objConjunto.NombreConjunto : ""

                    }).ToList();

                    listaResultadoDTOFinal = listaResultadoDTOFinal.Union(listaTemporal).ToList();

                    foreach (var objTemporal in listaResultadoDTOFinal)
                    {
                        foreach (var usuarioConjunto in objTemporal.UsuarioConjuntos)
                        {
                            if (objConjunto!=null)
                            {
                                if (usuarioConjunto.IdConjunto != objConjunto.IdConjunto)
                                {
                                    try
                                    {
                                        Conjunto objConjuntoTemporal = listaConjuntos.Where(x => x.IdConjunto == usuarioConjunto.IdConjunto).FirstOrDefault();

                                        if (objConjuntoTemporal != null)
                                            usuarioConjunto.NombreConjunto = objConjuntoTemporal.NombreConjunto;

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                else
                                {
                                    if (objConjunto != null)
                                        usuarioConjunto.NombreConjunto = objConjunto.NombreConjunto;
                                } 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return listaResultadoDTOFinal;
        }


        #endregion
    }
}
