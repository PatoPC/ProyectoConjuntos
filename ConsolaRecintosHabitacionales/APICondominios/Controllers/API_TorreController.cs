using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Torre;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_TorreController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Torre> _CRUD_Torres;
        private readonly IManageTorre _Torres;
        private readonly IMapper _mapper;

        public API_TorreController(IMapper mapper, IManageConjuntosCRUD<Torre> cRUD_Condominio, IManageTorre torres)
        {
            _mapper = mapper;
            _CRUD_Torres = cRUD_Condominio;
            _Torres = torres;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "GetTorreByID")]
        public async Task<IActionResult> GetTorreByID(Guid id)
        {
            try
            {
                Torre objRepositorio = await _Torres.obtenerPorIDTorre(id);
                if (objRepositorio == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }

                TorreDTOCompleto objDTO = _mapper.Map<TorreDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TorreDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }

                Torre objRepositorio = _mapper.Map<Torre>(objDTO);
                _CRUD_Torres.Add(objRepositorio);

                var result = await _CRUD_Torres.save();

                if (result.estado)
                {
                    TorreDTOCompleto objTorreDTO = _mapper.Map<TorreDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetTorreByID", new { id = objTorreDTO.IdTorres }, objTorreDTO);
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
        public async Task<IActionResult> Editar(Guid id, TorreDTOEditar objDTO)
        {
            try
            {
                Torre objRepository = new();
                if (id == ConstantesAplicacion.guidNulo)
                {
                    objRepository = await _Torres.obtenerPorIDTorre(objDTO.IdTorresEditar);
                }
                else
                {
                    objRepository = await _Torres.obtenerPorIDTorre(id);
                }
                
                _mapper.Map(objDTO, objRepository);

                _CRUD_Torres.Edit(objRepository);
                var result = await _CRUD_Torres.save();
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
            Torre objRepositorio = await _Torres.obtenerPorIDTorre(id);

            _CRUD_Torres.Delete(objRepositorio);
            var result = await _CRUD_Torres.save();

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

        [HttpGet("ObtenerTorresAvanzado")]
        public async Task<ActionResult<List<TorreDTOCompleto>>> ObtenerTorresAvanzado(BusquedaTorres objBusqueda)
        {
            List<Torre> listaResultado = new List<Torre>();


            listaResultado = await _Torres.busquedaAvanzada(objBusqueda);


            if (listaResultado.Count < 1)
            {
                return NotFound(MensajesRespuesta.sinResultados());
            }

            List<TorreDTOCompleto> listaResultadoDTO = _mapper.Map<List<TorreDTOCompleto>>(listaResultado);


            return Ok(listaResultadoDTO);
        }


    }
}
