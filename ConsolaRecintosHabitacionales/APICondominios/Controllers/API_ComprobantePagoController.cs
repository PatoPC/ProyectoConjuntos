using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using Utilitarios;
using AutoMapper;
using RepositorioLogs.Interface;
using DTOs.Comprobantes;
using APICondominios.Model;

namespace APICondominios.Controllers
{
    [Route("api/ComprobantePago")]
    [ApiController]
    public class API_ComprobantePagoController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<ComprobantePago> _CRUD_ComprobantePago;
        private readonly IManageComprobantePago _consultaComprobantes;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ComprobantePagoController(IManageConjuntosCRUD<ComprobantePago> cRUD_ComprobantePago, IManageComprobantePago consultaComprobantes, IMapper mapper, IManageLogError logError)
        {
            _CRUD_ComprobantePago = cRUD_ComprobantePago;
            _consultaComprobantes = consultaComprobantes;
            _mapper = mapper;
            _logError = logError;
        }

        [HttpGet("{id}", Name = "GetComprobanteByID")]
        public async Task<IActionResult> GetComprobanteByID(Guid id)
        {
            try
            {
                ComprobantePago objRepositorio = await _consultaComprobantes.obtenerComprobanteID(id);
                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                AdeudoDTOCompleto objDTO = _mapper.Map<AdeudoDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ComprobantePagoDTOCompleto objComprobante)
        {
            try
            {
                if (objComprobante == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                ComprobantePago objRepositorio = _mapper.Map<ComprobantePago>(objComprobante);

                _CRUD_ComprobantePago.Add(objRepositorio);
                var result = await _CRUD_ComprobantePago.save();

                if (result.estado)
                    return Ok();
                else
                    await guardarLogs(JsonConvert.SerializeObject(objComprobante), result.mensajeError);

            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objComprobante), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }


        [HttpGet("GetComprobanteByIDDetalle")]
        public async Task<IActionResult> GetComprobanteByIDDetalle(Guid idAdeudo)
        {
            try
            {
                ComprobantePago objRepositorio = await _consultaComprobantes.obtenerComprobanteIDDetalle(idAdeudo);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                ComprobantePagoDTOCompleto objDTO = _mapper.Map<ComprobantePagoDTOCompleto>(objRepositorio);

                return Ok(objDTO);
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
