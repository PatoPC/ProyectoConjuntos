using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Comunicado;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/Comunicado")]
    [ApiController]
    public class API_ComunicadoController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Comunicado> _CRUD_Comunicado;      
        private readonly IManageComunicado _ConsultaComunicado;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ComunicadoController(IMapper mapper, IManageConjuntosCRUD<Comunicado> CRUD_Comunicado, IManageLogError logError, IManageComunicado consultaAdeudo)
        {
            _mapper = mapper;
            _CRUD_Comunicado = CRUD_Comunicado;
            _logError = logError;
            _ConsultaComunicado = consultaAdeudo;
        }

        [HttpGet("{id}", Name = "GetComunicadoID")]
        public async Task<IActionResult> GetComunicadoID(Guid id)
        {
            try
            {
                var objRepositorio = await _ConsultaComunicado.obtenerPorIDComunicado(id);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                ComunicadoDTOCompleto objDTO = _mapper.Map<ComunicadoDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ComunicadoDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)                
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());                

                Comunicado objComunicado = _mapper.Map<Comunicado>(objDTO);

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
        public async Task<IActionResult> Editar(Guid id, ComunicadoDTOEditar objDTO)
        {
            try
            {
                var objRepositorio = await _ConsultaComunicado.obtenerPorIDComunicado(id);
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
            var objRepositorio = await _ConsultaComunicado.obtenerPorIDComunicado(id);

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

        [HttpGet("BusquedaAvanzadaComunicado")]
        public async Task<ActionResult<List<ComunicadoDTOCompleto>>> BusquedaAvanzadaComunicado(BusquedaComunicadoDTO objBusqueda)
        {
            try
            {
                List<Comunicado> listaResultado = new List<Comunicado>();
                listaResultado = await _ConsultaComunicado.obtenerPorIDComunicado(objBusqueda);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());


                List<ComunicadoDTOCompleto> listaResultadoDTO = _mapper.Map<List<ComunicadoDTOCompleto>>(listaResultado);

                return Ok(listaResultadoDTO);
            }
            catch (Exception ex)
            {

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
