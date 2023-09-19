using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Parametro;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/Parametro")]
    [ApiController]
    public class API_ParametroController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Parametro> _CRUD_Parametro;
        private readonly IManageParametro _Parametro;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ParametroController(IMapper mapper, IManageConjuntosCRUD<Parametro> cRUD_Parametro, IManageParametro parametro, IManageLogError logError)
        {
            _mapper = mapper;
            _CRUD_Parametro = cRUD_Parametro;
            _Parametro = parametro;
            _logError = logError;
        }

        [HttpGet("{id}", Name = "GetParametroByID")]
        public async Task<IActionResult> GetParametroByID(Guid id)
        {
            try
            {
                Parametro objRepositorio = await _Parametro.obtenerPorIDParametro(id);

                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());

                ParametroCompletoDTO objDTO = _mapper.Map<ParametroCompletoDTO>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ParametroCrearDTO objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());                

                Parametro objRepositorio = _mapper.Map<Parametro>(objDTO);
                _CRUD_Parametro.Add(objRepositorio);

                var result = await _CRUD_Parametro.save();

                if (result.estado)
                {
                    ParametroCompletoDTO objCatalogoResult = _mapper.Map<ParametroCompletoDTO>(objRepositorio);

                    return CreatedAtRoute("GetParametroByID", new { id = objCatalogoResult.IdParametro }, objCatalogoResult);
                }
                else                
                    await guardarLogs(JsonConvert.SerializeObject(objDTO), result.mensajeError);
                
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }

            return BadRequest(MensajesRespuesta.guardarError());
        }


        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, ProveedorDTOEditar objDTO)
        {
            try
            {
                var objRepository = await _Parametro.obtenerPorIDParametro(id);
                _mapper.Map(objDTO, objRepository);

                _CRUD_Parametro.Edit(objRepository);
                var result = await _CRUD_Parametro.save();
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
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            Parametro objRepositorio = await _Parametro.obtenerPorIDParametro(id);

            _CRUD_Parametro.Delete(objRepositorio);
            var result = await _CRUD_Parametro.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)            
                return NoContent();            
            else            
                await guardarLogs(JsonConvert.SerializeObject(objRepositorio, jsonSerializerSettings), result.mensajeError);           

            return BadRequest();
        }

        [HttpGet("BusquedaAvanzadaProveedor")]
        public async Task<ActionResult<List<ProveedorDTOCompleto>>> BusquedaAvanzadaProveedor(BusquedaProveedor objBusqueda)
        {
            try
            {
                List<Parametro> listaResultado = new List<Parametro>();
                //listaResultado = await _Parametro.busquedaAvanzada(objBusqueda);

                if (listaResultado.Count < 1)                
                    return NotFound(MensajesRespuesta.sinResultados());
                

                List<ProveedorDTOCompleto> listaResultadoDTO = _mapper.Map<List<ProveedorDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("ObtenerTodosProveedor")]
        public async Task<ActionResult<List<ProveedorDTOCompleto>>> ObtenerTodosProveedor(Guid idConjuunto)
        {
            try
            {
                List<Parametro> listaResultado = new List<Parametro>();
                //listaResultado = await _Parametro.busquedaTodosProveedor(idConjuunto);

                if (listaResultado.Count < 1)                
                    return NotFound(MensajesRespuesta.sinResultados());                

                List<ProveedorDTOCompleto> listaResultadoDTO = _mapper.Map<List<ProveedorDTOCompleto>>(listaResultado);

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
