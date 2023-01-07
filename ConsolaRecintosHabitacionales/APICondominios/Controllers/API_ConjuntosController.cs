using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_ConjuntosController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Conjunto> _CRUD_Conjuntos;
        private readonly IManageConjuntos _Conjuntos;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ConjuntosController(IMapper mapper, IManageConjuntosCRUD<Conjunto> cRUD_Condominio, IManageConjuntos condominio, IManageLogError logError)
        {
            _mapper = mapper;
            _CRUD_Conjuntos = cRUD_Condominio;
            _Conjuntos = condominio;
            _logError = logError;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}", Name = "GetConjuntoByID")]
        public async Task<IActionResult> GetConjuntoByID(Guid id)
        {
            try
            {
                Conjunto objRepositorio = await _Conjuntos.obtenerPorIDConjuntos(id);
                if (objRepositorio == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }

                ConjuntoDTOCompleto objDTO = _mapper.Map<ConjuntoDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConjuntoDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }

                Conjunto objRepositorio = _mapper.Map<Conjunto>(objDTO);
                _CRUD_Conjuntos.Add(objRepositorio);

                var result = await _CRUD_Conjuntos.save();

                if (result.estado)
                {
                    ConjuntoDTOCompleto objCatalogoResult = _mapper.Map<ConjuntoDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetConjuntoByID", new { id = objCatalogoResult.IdConjunto }, objCatalogoResult);
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


        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, ConjuntoDTOEditar objDTO)
        {
            try
            {
                var objRepository = await _Conjuntos.obtenerPorIDConjuntos(id);
                _mapper.Map(objDTO, objRepository);

                _CRUD_Conjuntos.Edit(objRepository);
                var result = await _CRUD_Conjuntos.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                {
                    return NoContent();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                }

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
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            Conjunto objRepositorio = await _Conjuntos.obtenerPorIDConjuntos(id);

            _CRUD_Conjuntos.Delete(objRepositorio);
            var result = await _CRUD_Conjuntos.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)
            {
                return NoContent();
            }
            else
            {
                await guardarLogs(JsonConvert.SerializeObject(objRepositorio, jsonSerializerSettings), result.mensajeError);
            }

            return BadRequest();
        }

        [HttpGet("ObtenerConjutosAvanzado")]
        public async Task<ActionResult<List<ResultadoBusquedaConjuntos>>> ObtenerConjutosAvanzado(BusquedaConjuntos objBusqueda)
        {
            try
            {

                List<Conjunto> listaResultado = new List<Conjunto>();

                listaResultado = await _Conjuntos.busquedaAvanzada(objBusqueda);


                if (listaResultado.Count < 1)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }

                List<ResultadoBusquedaConjuntos> listaResultadoDTO = _mapper.Map<List<ResultadoBusquedaConjuntos>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }



        [HttpGet("ObtenerTodosConjuntos")]
        public async Task<ActionResult<List<ResultadoBusquedaConjuntos>>> ObtenerTodosConjuntos()
        {
            try
            {

                List<Conjunto> listaResultado = new List<Conjunto>();

                listaResultado = await _Conjuntos.busquedaTodosConjuntos();


                if (listaResultado.Count < 1)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }

                List<ResultadoBusquedaConjuntos> listaResultadoDTO = _mapper.Map<List<ResultadoBusquedaConjuntos>>(listaResultado);

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
