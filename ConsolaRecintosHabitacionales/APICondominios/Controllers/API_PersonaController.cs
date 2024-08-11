using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Persona;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/persona")]
    [ApiController]
    public class API_PersonaController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Persona> _CRUD_Persona;
        private readonly IManageConjuntosCRUD<TipoPersona> _CRUD_TipoPersona;
        private readonly IManagePersona _ConsultasPersonas;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_PersonaController(IMapper mapper, IManageConjuntosCRUD<Persona> cRUD_Condominio, IManagePersona condominio, IManageConjuntosCRUD<TipoPersona> cRUD_TipoPersona, IManageLogError logError)
        {
            _mapper = mapper;
            _CRUD_Persona = cRUD_Condominio;
            _ConsultasPersonas = condominio;
            _CRUD_TipoPersona = cRUD_TipoPersona;
            _logError = logError;
        }

        [HttpGet("{id}", Name = "GetPersonaByID")]
        public async Task<IActionResult> GetPersonaByID(Guid id)
        {
            try
            {
                Persona objRepositorio = await _ConsultasPersonas.obtenerPorIDPersona(id);
                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());                

                PersonaDTOCompleto objDTO = _mapper.Map<PersonaDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)                
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                

                Persona objRepositorio = _mapper.Map<Persona>(objDTO);
                _CRUD_Persona.Add(objRepositorio);

                var result = await _CRUD_Persona.save();

                if (result.estado)
                {
                    PersonaDTOCompleto objCatalogoResult = _mapper.Map<PersonaDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetPersonaByID", new { id = objCatalogoResult.IdPersona }, objCatalogoResult);
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                }
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }

        #region Eliminar
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, PersonaDTOEditar objDTO)
        {
            try
            {
                var objRepository = await _ConsultasPersonas.obtenerPorIDPersona(id);
                _mapper.Map(objDTO, objRepository);

                _CRUD_Persona.Edit(objRepository);
                var result = await _CRUD_Persona.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)                
                    return NoContent();                
                else                
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                

                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }

            return StatusCode(StatusCodes.Status406NotAcceptable);
        }


        [HttpPost("Eliminar")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            Persona objRepositorio = await _ConsultasPersonas.obtenerPorIDPersona(id);

            _CRUD_Persona.Delete(objRepositorio);
            var result = await _CRUD_Persona.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)            
                return NoContent();            
            else            
                await guardarLogs(id.ToString()+ " Eliminar->API_Persona", result.mensajeError);
            

            return BadRequest();
        }
        #endregion

        #region Tipo Persona Departamento
        [HttpPost("CrearPersonaDepartamento")]
        public async Task<IActionResult> CrearPersonaDepartamento(TipoPersonaDTO objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                //Valida si el departamento ya tiene un inquilino/dueño, si existe se elimina para insertar el nuevo.
                ObjTipoPersonaDepartamento objDTOBusqueda = new ObjTipoPersonaDepartamento();
                objDTOBusqueda.IdTipoPersonaDepartamento = objDTO.IdTipoPersonaDepartamento;
                objDTOBusqueda.IdDepartamento = objDTO.IdDepartamento;

                TipoPersona resultadoRepositorio = await _ConsultasPersonas.busquedaPersonaDepartamento(objDTOBusqueda);

                if (resultadoRepositorio != null)                
                    _CRUD_TipoPersona.Delete(resultadoRepositorio);                
                //Fin Validación

                TipoPersona objRepositorio = _mapper.Map<TipoPersona>(objDTO);

                _CRUD_TipoPersona.Add(objRepositorio);

                var result = await _CRUD_TipoPersona.save();

                if (result.estado)
                    return Ok();
                else
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);

            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }

        [HttpGet("ConsultaPersonaDepartamento")]
        public async Task<IActionResult> ConsultaPersonaDepartamento(ObjTipoPersonaDepartamento objDTO)
        {

            try
            {
                TipoPersona listaResultado = await _ConsultasPersonas.busquedaPersonaDepartamento(objDTO);

                if (listaResultado ==null)
                    return NotFound(MensajesRespuesta.sinResultados());

                TipoPersonaDTO listaResultadoDTO = _mapper.Map<TipoPersonaDTO>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        [HttpGet("ObtenerPersonaAvanzado")]
        public async Task<ActionResult<List<PersonaDTOCompleto>>> ObtenerPersonaAvanzado(ObjetoBusquedaPersona objBusqueda)
        {
            try
            {
                List<Persona> listaResultado = new List<Persona>();

                listaResultado = await _ConsultasPersonas.busquedaAvanzada(objBusqueda);

                if (listaResultado.Count < 1)                
                    return NotFound(MensajesRespuesta.sinResultados());                

                List<PersonaDTOCompleto> listaResultadoDTO = _mapper.Map<List<PersonaDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("ObtenerPersonaIdentificacion")]
        public async Task<ActionResult<PersonaDTOCompleto>> ObtenerPersonaIdentificacion(string numeroIdentificacion)
        {
            try
            {
                List<Persona> listaResultado = new List<Persona>();

                listaResultado = await _ConsultasPersonas.obtenerPersonaPoNumeroIdentificacion(numeroIdentificacion);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());

                List<PersonaDTOCompleto> listaResultadoDTO = _mapper.Map<List<PersonaDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("ObtenerPersonaAutoCompletar")]
        public async Task<ActionResult<List<PersonaDTOCompleto>>> ObtenerPersonaAutoCompletar(string termino)
        {
            try
            {
                List<Persona> listaResultado = new List<Persona>();

                listaResultado = await _ConsultasPersonas.obtenerPersonaAutoCompletar(termino);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());

                List<PersonaDTOCompleto> listaResultadoDTO = _mapper.Map<List<PersonaDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        #region Varios
        private async Task guardarLogs(string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(_logError);

            await objLooger.guardarError(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), mensajeError, objetoJSON);

        }
        #endregion
    }

}
