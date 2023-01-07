using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Proveedor;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    
    [Route("api/proveeedor")]
    [ApiController]
    public class API_ProveedoresController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<Proveedore> _CRUD_Proveedor;
        private readonly IManageProveedor _Proveedor;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ProveedoresController(IMapper mapper, IManageConjuntosCRUD<Proveedore> cRUD_Proveedor, IManageProveedor condominio, IManageLogError logError)
        {
            _mapper = mapper;
            _CRUD_Proveedor = cRUD_Proveedor;
            _Proveedor = condominio;
            _logError = logError;
        }

        [HttpGet("{id}", Name = "GetProveedorByID")]
        public async Task<IActionResult> GetProveedorByID(Guid id)
        {
            try
            {
                Proveedore objRepositorio = await _Proveedor.obtenerPorIDProveedor(id);
                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());
                

                ProveedorDTOCompleto objDTO = _mapper.Map<ProveedorDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProveedorDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());                

                Proveedore objRepositorio = _mapper.Map<Proveedore>(objDTO);
                _CRUD_Proveedor.Add(objRepositorio);

                var result = await _CRUD_Proveedor.save();

                if (result.estado)
                {
                    ProveedorDTOCompleto objCatalogoResult = _mapper.Map<ProveedorDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetProveedorByID", new { id = objCatalogoResult.IdProveedor }, objCatalogoResult);
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
                var objRepository = await _Proveedor.obtenerPorIDProveedor(id);
                _mapper.Map(objDTO, objRepository);

                _CRUD_Proveedor.Edit(objRepository);
                var result = await _CRUD_Proveedor.save();
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

            Proveedore objRepositorio = await _Proveedor.obtenerPorIDProveedor(id);

            _CRUD_Proveedor.Delete(objRepositorio);
            var result = await _CRUD_Proveedor.save();

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
                List<Proveedore> listaResultado = new List<Proveedore>();
                listaResultado = await _Proveedor.busquedaAvanzada(objBusqueda);

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
                List<Proveedore> listaResultado = new List<Proveedore>();
                listaResultado = await _Proveedor.busquedaTodosProveedor(idConjuunto);

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
