using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Comunicado;
using DTOs.ReservaArea;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{

    [Route("api/ReservaArea")]
    [ApiController]
    public class API_ReservaAreaController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<ReservaArea> _CRUD_Comunicado;
        private readonly IManageReservaArea _ConsultaReservaArea;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ReservaAreaController(IMapper mapper, IManageConjuntosCRUD<ReservaArea> CRUD_Comunicado, IManageLogError logError, IManageReservaArea consultaAdeudo)
        {
            _mapper = mapper;
            _CRUD_Comunicado = CRUD_Comunicado;
            _logError = logError;
            _ConsultaReservaArea = consultaAdeudo;
        }

        [HttpGet("{id}", Name = "GetReservaArea")]
        public async Task<IActionResult> GetReservaArea(Guid id)
        {
            try
            {
                var objRepositorio = await _ConsultaReservaArea.obtenerPorIDReservaArea(id);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                ReservaAreaDTOCompleto objDTO = _mapper.Map<ReservaAreaDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaAreaDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                ReservaArea objComunicado = _mapper.Map<ReservaArea>(objDTO);

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
        public async Task<IActionResult> Editar(Guid id, ReservaAreaDTOEditar objDTO)
        {
            try
            {
                var objRepositorio = await _ConsultaReservaArea.obtenerPorIDReservaArea(id);
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
            var objRepositorio = await _ConsultaReservaArea.obtenerPorIDReservaArea(id);

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

        [HttpGet("BusquedaAvanzadaReservaArea")]
        public async Task<ActionResult<List<ReservaAreaDTOCompleto>>> BusquedaAvanzadaReservaArea(BusquedaReservaAreaDTO objBusqueda)
        {
            try
            {
                List<ReservaArea> listaResultado = new List<ReservaArea>();
                listaResultado = await _ConsultaReservaArea.obtenerAvanzado(objBusqueda);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());


                List<ReservaAreaDTOCompleto> listaResultadoDTO = _mapper.Map<List<ReservaAreaDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objBusqueda), ex.ToString());
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("BuscarReservaAreaPorIdArea")]
        public async Task<ActionResult<List<ReservaAreaDTOCompleto>>> BuscarReservaAreaPorIdArea(Guid idAreaComunal)
        {
            try
            {
                List<ReservaArea> listaResultado = new List<ReservaArea>();
                listaResultado = await _ConsultaReservaArea.obtenerReservaAreaPorIdArea(idAreaComunal);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());


                List<ReservaAreaDTOCompleto> listaResultadoDTO = _mapper.Map<List<ReservaAreaDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(idAreaComunal), ex.ToString());
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
