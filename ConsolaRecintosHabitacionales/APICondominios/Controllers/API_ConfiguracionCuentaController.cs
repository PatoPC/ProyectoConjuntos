using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using DTOs.ConfiguracionCuenta;
using DTOs.Departamento;
using EntidadesCatalogos.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioCatalogos.Interface;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/ConfiguracionCuenta")]
    [ApiController]
    public class API_ConfiguracionCuentaController : ControllerBase
    {
        private readonly IManageCRUDCatalogo<Configuracioncuentum> _CRUD_Configuracion;
        private readonly IManageConfiguracionCuenta _ConsultaConfiguracion;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;

        public API_ConfiguracionCuentaController(IMapper mapper, IManageLogError logError, IManageCRUDCatalogo<Configuracioncuentum> cRUD_Configuracion, IManageConfiguracionCuenta consultaConfiguracion)
        {
            _mapper = mapper;
            _logError = logError;
            _CRUD_Configuracion = cRUD_Configuracion;
            _ConsultaConfiguracion = consultaConfiguracion;
        }

        #region Crear
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConfiguraCuentasDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                Configuracioncuentum objRespositorio = _mapper.Map<Configuracioncuentum>(objDTO);

                _CRUD_Configuracion.Add(objRespositorio);

                var result = await _CRUD_Configuracion.save();

                if (result.estado)
                    return Ok();
                else
                    await guardarLogs(JsonConvert.SerializeObject(objRespositorio), result.mensajeError);

            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }
        #endregion Crear

        #region Editar
        [HttpPost("Editar")]
        public async Task<IActionResult> Editar(Guid id, ConfiguraCuentasDTOEditar objDTO)
        {
            try
            {
                Configuracioncuentum objRepository = await _ConsultaConfiguracion.GetConfiguracionCuenta(id);
                              
                _mapper.Map(objDTO, objRepository);

                _CRUD_Configuracion.Edit(objRepository);
                var result = await _CRUD_Configuracion.save();
                
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
        #endregion Editar

        
        #region Eliminar
        [HttpPost("Eliminar")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            Configuracioncuentum objRepository = await _ConsultaConfiguracion.GetConfiguracionCuenta(id);

            _CRUD_Configuracion.Delete(objRepository);

            var result = await _CRUD_Configuracion.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)
                return NoContent();
            else
                await guardarLogs(JsonConvert.SerializeObject(objRepository, jsonSerializerSettings), result.mensajeError);

            return BadRequest();
        }
        #endregion Eliminar



        [HttpGet("{id}", Name = "GetConfigCuentaByID")]
        public async Task<IActionResult> GetConfigCuentaByID(Guid id)
        {
            try
            {
                Configuracioncuentum objRepositorio = await _ConsultaConfiguracion.GetConfiguracionCuenta(id);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                ConfiguraCuentasDTOCompleto objDTO = _mapper.Map<ConfiguraCuentasDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {
                await guardarLogs("GetDepartamentoByID: id " + id.ToString(), ex.ToString());
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        [HttpGet("GetConfigCuentaByIDConjunto")]
        public async Task<IActionResult> GetConfigCuentaByIDConjunto(Guid idConjunto)
        {
            try
            {
                Configuracioncuentum objRepositorio = await _ConsultaConfiguracion.GetConfigCuentaConjunto(idConjunto);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                ConfiguraCuentasDTOCompleto objDTO = _mapper.Map<ConfiguraCuentasDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {
                await guardarLogs("GetDepartamentoByID: id " + idConjunto.ToString(), ex.ToString());
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
