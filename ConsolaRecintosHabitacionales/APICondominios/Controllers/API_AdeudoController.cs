using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;
using XAct;

namespace APICondominios.Controllers
{
    
    [Route("api/adeudo")]
    [ApiController]
    public class API_AdeudoController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<List<Adeudo>> _CRUD_Persona;
        private readonly IManageConjuntosCRUD<Adeudo> _CRUD_Adeudo;
        private readonly IManagePersona _ConsultasPersonas;
        private readonly IManageAdeudo _ConsultaAdeudo;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_AdeudoController(IMapper mapper, IManageConjuntosCRUD<List<Adeudo>> cRUD_Condominio, IManagePersona condominio, IManageLogError logError, IManageAdeudo consultaAdeudo, IManageConjuntosCRUD<Adeudo> cRUD_Adeudo)
        {
            _mapper = mapper;
            _CRUD_Persona = cRUD_Condominio;
            _ConsultasPersonas = condominio;
            _logError = logError;
            _ConsultaAdeudo = consultaAdeudo;
            _CRUD_Adeudo = cRUD_Adeudo;
        }

        [HttpGet("{id}", Name = "GetAdeudoByID")]
        public async Task<IActionResult> GetAdeudoByID(Guid id)
        {
            try
            {
                Adeudo objRepositorio = await _ConsultaAdeudo.obtenerAdeudosAvanzado(id);
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
        public async Task<IActionResult> Post([FromBody] List<AdeudoDTOCrear> listaDTO)
        {
            try
            {
                if (listaDTO == null)                
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                

                List<Adeudo> listaRepositorio = _mapper.Map<List<Adeudo>>(listaDTO);               

                var result = await _CRUD_Persona.saveRangeAdeudo(listaRepositorio);

                if (result.estado)
                    return Ok();
                else
                    await guardarLogs(JsonConvert.SerializeObject(listaDTO), result.mensajeError);
                
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(listaDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }

        #region Editar
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, AdeudoDTOEditar objDTO)
        {
            try
            {
                Adeudo objRepository = await _ConsultaAdeudo.obtenerAdeudosAvanzado(id);
                _mapper.Map(objDTO, objRepository);

                _CRUD_Adeudo.Edit(objRepository);

                var result = await _CRUD_Persona.save();
                
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
        //[HttpPost("Eliminar")]
        //public async Task<IActionResult> Eliminar(Guid id)
        //{
        //    Adeudo objRepositorio = await _ConsultasPersonas.obtenerPorIDPersona(id);

        //    _CRUD_Persona.Delete(objRepositorio);
        //    var result = await _CRUD_Persona.save();

        //    //Se comprueba que se actualizó correctamente
        //    if (result.estado)            
        //        return NoContent();            
        //    else            
        //        await guardarLogs(id.ToString()+ " Eliminar->API_Persona", result.mensajeError);


        //    return BadRequest();
        //}
        #endregion

        #region Busquedas

        [HttpGet("BusquedaAvanzadaAdeudo")]
        public async Task<ActionResult<List<AdeudoDTOCompleto>>> BusquedaAvanzadaAdeudo(GenerarAdeudo objBusqueda)
        {
            try
            {
                List<Adeudo> listaResultado = new List<Adeudo>();
                listaResultado = await _ConsultaAdeudo.obtenerAdeudosAvanzado(objBusqueda);

                if (listaResultado.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());

                List<AdeudoDTOCompleto> listaResultadoDTO = _mapper.Map<List<AdeudoDTOCompleto>>(listaResultado);

                if (!string.IsNullOrEmpty(objBusqueda.nombrePersona))
                {
                    listaResultadoDTO = listaResultadoDTO.Where(x => x.Nombre.ToUpper().Contains(objBusqueda.nombrePersona.ToUpper()) 
                                    || x.Apellido.ToUpper().Contains(objBusqueda.nombrePersona.ToUpper())).ToList();
                }

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
