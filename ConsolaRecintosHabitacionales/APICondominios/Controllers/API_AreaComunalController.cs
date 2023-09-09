using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Comunicado;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/AreaComunal")]
    [ApiController]
    public class API_AreaComunalController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<AreaComunal> _CRUD_Comunicado;      
        private readonly IManageAreaComunal _ConsultaAreaComunal;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_AreaComunalController(IMapper mapper, IManageConjuntosCRUD<AreaComunal> CRUD_Comunicado, IManageLogError logError, IManageAreaComunal consultaAdeudo)
        {
            _mapper = mapper;
            _CRUD_Comunicado = CRUD_Comunicado;
            _logError = logError;
            _ConsultaAreaComunal = consultaAdeudo;
        }

        [HttpGet("{id}", Name = "GetAreaComunal")]
        public async Task<IActionResult> GetAreaComunal(Guid id)
        {
            try
            {
                var objRepositorio = await _ConsultaAreaComunal.obtenerPorIDAreaComunal(id);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                AreaComunalDTOCompleto objDTO = _mapper.Map<AreaComunalDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AreaComunalDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)                
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());                

                AreaComunal objComunicado = _mapper.Map<AreaComunal>(objDTO);

                _CRUD_Comunicado.Add(objComunicado);

                var result = await _CRUD_Comunicado.save();

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

        #region Editar
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, AreaComunalDTOEditar objDTO)
        {
            try
            {
                var objRepositorio = await _ConsultaAreaComunal.obtenerPorIDAreaComunal(id);
                _mapper.Map(objDTO, objRepositorio);

                _CRUD_Comunicado.Edit(objRepositorio);
                var result = await _CRUD_Comunicado.save();
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
        #endregion

        #region Eliminar
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var objRepositorio = await _ConsultaAreaComunal.obtenerPorIDAreaComunal(id);

            _CRUD_Comunicado.Delete(objRepositorio);
            var result = await _CRUD_Comunicado.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)
                return NoContent();
            else
                await guardarLogs(id.ToString() + " Eliminar->API_Comunicado", result.mensajeError);


            return BadRequest();
        }
        #endregion

        #region Busquedas

        [HttpGet("BusquedaAvanzadaAreaComu")]
        public async Task<ActionResult<List<AreaComunalDTOCompleto>>> BusquedaAvanzadaAreaComu(BusquedaAreaComunal objBusqueda)
        {
            try
            {
                List<AreaComunal> listaResultado = new List<AreaComunal>();
                listaResultado = await _ConsultaAreaComunal.obtenerAvanzado(objBusqueda);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());


                List<AreaComunalDTOCompleto> listaResultadoDTO = _mapper.Map<List<AreaComunalDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objBusqueda), ex.ToString());
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region Varios
        private async Task guardarLogs(string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(_logError);

            await objLooger.guardarError(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), mensajeError, objetoJSON);

        }
        #endregion
    }

}
