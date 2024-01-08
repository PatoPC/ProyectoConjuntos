using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Comunicado;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_ContabilidadController : ControllerBase
    {
        private readonly IManageConMST _consultaMaestroCont;
        private readonly IManageContabilidad _consultaContabilidad;

        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;


        public API_ContabilidadController(IManageConMST consultaMaestroCont, IMapper mapper, IManageLogError logError, IManageContabilidad consultaContabilidad)
        {
            _consultaMaestroCont = consultaMaestroCont;
            _mapper = mapper;
            _logError = logError;
            _consultaContabilidad = consultaContabilidad;
        }

        [HttpGet("{id}", Name = "GetEncabezadoContabilidad")]
        public async Task<IActionResult> GetEncabezadoContabilidad(Guid id)
        {
            try
            {
                EncabezadoContabilidad objRepositorio = await _consultaContabilidad.EncabezadoContabilidadPorID(id);

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


    }
}
