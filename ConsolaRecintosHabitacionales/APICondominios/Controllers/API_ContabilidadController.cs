using APICondominios.Helpers;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Comunicado;
using DTOs.Contabilidad;
using DTOs.Departamento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/API_Contabilidad")]
    [ApiController]
    public class API_ContabilidadController : ControllerBase
    {
        private readonly IManageContabilidad _consultaContabilidad;
        private readonly IManageConjuntosCRUD<EncabezadoContabilidad> _CRUD_Contabilidad;

        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;


        public API_ContabilidadController(IMapper mapper, IManageLogError logError, IManageContabilidad consultaContabilidad, IManageConjuntosCRUD<EncabezadoContabilidad> cRUD_Contabilidad)
        {
            _mapper = mapper;
            _logError = logError;
            _consultaContabilidad = consultaContabilidad;
            _CRUD_Contabilidad = cRUD_Contabilidad;
        }

        [HttpGet("{id}", Name = "GetEncabezadoContabilidad")]
        public async Task<IActionResult> GetEncabezadoContabilidad(Guid id)
        {
            try
            {
                EncabezadoContabilidad objRepositorio = await _consultaContabilidad.EncabezadoContabilidadPorID(id);

                if (objRepositorio == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                EncabezContDTOCompleto objDTO = _mapper.Map<EncabezContDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetSecuencialMaximoCabecera")]
        public ActionResult<int> GetSecuencialMaximoCabecera()
        {
            try
            {
                int secuencialMaximo = _consultaContabilidad.GetSecuencialMaximoCabecera();

                return Ok(secuencialMaximo);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        #region CRUD 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EncabezContDTOCrear objDTO)
        {
            try
            {
                if (objDTO == null)
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

                EncabezadoContabilidad objRepositorio = _mapper.Map<EncabezadoContabilidad>(objDTO);
                _CRUD_Contabilidad.Add(objRepositorio);

                var result = await _CRUD_Contabilidad.save();

                if (result.estado)
                {
                    EncabezContDTOCompleto objDTOResultado = _mapper.Map<EncabezContDTOCompleto>(objRepositorio);

                    return CreatedAtRoute("GetEncabezadoContabilidad", new { id = objDTOResultado.IdEncCont }, objDTOResultado);
                }
                else
                    await LoggingHelper.GuardarLogsAsync(_logError, this, JsonConvert.SerializeObject(objDTO), result.mensajeError);

            }
            catch (Exception ExValidation)
            {
                await LoggingHelper.GuardarLogsAsync(_logError, this, JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
            }

            return BadRequest(MensajesRespuesta.guardarError());
        }

        //[HttpPost("Editar")]
        //public async Task<IActionResult> Editar(Guid id, DepartamentoDTOEditar objDTO)
        //{
        //    try
        //    {
        //        EncabezadoContabilidad objRepository = await _consultaContabilidad.EncabezadoContabilidadPorID(id);

        //        List<AreasDepartamento> AreasDepartamentos = objRepository.AreasDepartamentos.ToList();

        //        var resultadoAreas = await _CRUD_AreaDepartamento.DeleteRange(AreasDepartamentos);
        //        //var resultadoAreas = await _CRUD_AreaDepartamento.save();

        //        _mapper.Map(objDTO, objRepository);

        //        _CRUD_Departamento.Edit(objRepository);
        //        var result = await _CRUD_Departamento.save();
        //        // se comprueba que se actualizo correctamente
        //        if (result.estado)
        //            return NoContent();
        //        else
        //            await LoggingHelper.GuardarLogsAsync(_logError, this, JsonConvert.SerializeObject(objDTO), result.mensajeError);


        //        return BadRequest(MensajesRespuesta.guardarError());
        //    }
        //    catch (Exception ExValidation)
        //    {
        //        await LoggingHelper.GuardarLogsAsync(_logError, this, JsonConvert.SerializeObject(objDTO), ExValidation.ToString());
        //    }
        //    return StatusCode(StatusCodes.Status406NotAcceptable);
        //}

        [HttpPost("Eliminar")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };

            EncabezadoContabilidad objRepositorio = await _consultaContabilidad.EncabezadoContabilidadPorID(id);

            //List<AreasDepartamento> AreasDepartamentos = objRepositorio.AreasDepartamentos.ToList();

            //var resultadoAreas = await _CRUD_AreaDepartamento.DeleteRange(AreasDepartamentos);
            //var resultadoAreas = await _CRUD_AreaDepartamento.save();

            _CRUD_Contabilidad.Delete(objRepositorio);
            var result = await _CRUD_Contabilidad.save();

            //Se comprueba que se actualizó correctamente
            if (result.estado)
                return NoContent();
            else
                await LoggingHelper.GuardarLogsAsync(_logError, this, JsonConvert.SerializeObject(objRepositorio), result.mensajeError);


            return BadRequest();
        }

        #endregion

        #region Busqueda
        [HttpGet("GetBusquedaAvanzadaContabilidad")]
        public async Task<ActionResult<List<EncabezContDTOCompleto>>> GetBusquedaAvanzadaContabilidad(BusquedaContabilidad objBusqueda)
        {
            try
            {
                List<EncabezadoContabilidad> listaRepositorioEncabezado = await _consultaContabilidad.GetBusquedaAvanzadaContabilidad(objBusqueda);

                List<EncabezContDTOCompleto> listaEncabezados = _mapper.Map<List<EncabezContDTOCompleto>>(listaRepositorioEncabezado);

                return Ok(listaEncabezados);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion
    }
}
