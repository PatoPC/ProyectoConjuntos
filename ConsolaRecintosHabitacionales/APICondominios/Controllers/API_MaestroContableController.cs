using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.MaestroContable;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/API_MaestroContable")]
    [ApiController]
    public class API_MaestroContableController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<ConMst> _CRUD_ConMST;
        private readonly IManageConjuntosCRUD<List<ConMst>> _CRUD_ConMSTLista;
        private readonly IManageConMST _consultaMaestroCont;

        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_MaestroContableController(IManageConjuntosCRUD<ConMst> cRUD_ConMST, IMapper mapper, IManageLogError logError, IManageConMST consultaMaestroCont, IManageConjuntosCRUD<List<ConMst>> cRUD_ConMSTLista)
        {
            _CRUD_ConMST = cRUD_ConMST;
            _mapper = mapper;
            _logError = logError;
            _consultaMaestroCont = consultaMaestroCont;
            _CRUD_ConMSTLista = cRUD_ConMSTLista;
        }

        #region CRUD

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MaestroContableDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                ConMst objRepositorio = _mapper.Map<ConMst>(objDTO);

                _CRUD_ConMST.Add(objRepositorio);

                var result = await _CRUD_ConMST.save();

                if (result.estado)
                {
                    MaestroContableDTOCompleto objCatalogoResult = _mapper.Map<MaestroContableDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetMaestroContableByID", new { id = objCatalogoResult.IdConMst }, objCatalogoResult);
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


        [HttpPost("CrearListaMaestro")]
        public async Task<IActionResult> CrearListaMaestro([FromBody] List<MaestroContableDTOCrear> objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                List<ConMst> objRepositorio = _mapper.Map<List<ConMst>>(objDTO);

                var result = await _CRUD_ConMSTLista.saveRangeMaestro(objRepositorio);

                if (result.estado)
                {
                    List<MaestroContableDTOCompleto> listaResult = _mapper.Map<List<MaestroContableDTOCompleto>>(objRepositorio);

                    return Ok(listaResult);
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
        public async Task<IActionResult> Editar(Guid id, MaestroContableDTOEditar objDTO)
        {
            try
            {
                ConMst objRepository = await _consultaMaestroCont.obtenerPorIDConMST(id);

                _mapper.Map(objDTO, objRepository);

                _CRUD_ConMST.Edit(objRepository);
                var result = await _CRUD_ConMST.save();
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

            ConMst objRepositorio = await _consultaMaestroCont.obtenerPorIDConMST(id);

            _CRUD_ConMST.Delete(objRepositorio);
            var result = await _CRUD_ConMST.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)            
                return NoContent();            
            else            
                await guardarLogs(JsonConvert.SerializeObject(objRepositorio, jsonSerializerSettings), result.mensajeError);            

            return BadRequest();
        }


        #endregion

        [HttpGet("{id}", Name = "GetMaestroContableByID")]
        public async Task<IActionResult> GetMaestroContableByID(Guid id)
        {
            try
            {
                ConMst objRepositorio = await _consultaMaestroCont.obtenerPorIDConMST(id);

                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());

                MaestroContableDTOCompleto objDTO = _mapper.Map<MaestroContableDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("ObtenerMaestroContableTodos")]
        public async Task<ActionResult<List<ResultadoBusquedaConjuntos>>> ObtenerMaestroContableTodos()
        {
            try
            {
                List<ConMst> listaResultado = await _consultaMaestroCont.obtenerTodos();

                if (listaResultado.Count ==0)                
                    return NotFound(MensajesRespuesta.sinResultados());                

                List<MaestroContableDTOCompleto> listaResultadoDTO = _mapper.Map<List<MaestroContableDTOCompleto>>(listaResultado);

                listaResultadoDTO = listaResultadoDTO.OrderBy(x => x.CuentaCon).ToList();

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
