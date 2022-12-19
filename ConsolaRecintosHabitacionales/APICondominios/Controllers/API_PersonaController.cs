using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Persona;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/persona")]
    [ApiController]
    public class API_PersonaController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Persona> _CRUD_Persona;
        private readonly IManagePersona _ConsultasPersonas;
        private readonly IMapper _mapper;

        public API_PersonaController(IMapper mapper, IManageConjuntosCRUD<Persona> cRUD_Condominio, IManagePersona condominio)
        {
            _mapper = mapper;
            _CRUD_Persona = cRUD_Condominio;
            _ConsultasPersonas = condominio;
        }
       
        [HttpGet("{id}", Name = "GetPersonaByID")]
        public async Task<IActionResult> GetPersonaByID(Guid id)
        {
            try
            {
                Persona objRepositorio = await _ConsultasPersonas.obtenerPorIDPersona(id);
                if (objRepositorio == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }

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
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }

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
                    //await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                }
            }
            catch (Exception ExValidation)
            {
                //await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }


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
                {
                    return NoContent();
                }
                else
                {
                    //await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), result.mensajeError);
                }

                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                //await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), ExValidation.ToString());
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
            {
                return NoContent();
            }
            else
            {
                //await guardarLogs(JsonConvert.SerializeObject(objCatalogoRepository, jsonSerializerSettings), result.mensajeError);
            }

            return BadRequest();
        }

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
    }
    
}
