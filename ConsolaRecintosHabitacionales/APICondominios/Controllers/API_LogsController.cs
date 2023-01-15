using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Error;
using DTOs.Logs;
using GestionLogs.Entidades;
using GestionUsuarioDB.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositorioConjuntos.Interface;
using RepositorioGestionUsuarios.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/Logs")]
    [ApiController]
    public class API_LogsController : ControllerBase
    {
        private readonly IManageConsultasUsuario _UsuarioConsultasRepository;
        private readonly IManagePersona _ConsultasPersonas;
        private readonly IManageLogError _logError;
        private readonly IMapper _mapper;

        public API_LogsController(IMapper mapper, IManageLogError logError, IManageConsultasUsuario usuarioConsultasRepository, IManagePersona consultasPersonas)
        {
            this._mapper = mapper;
            _logError = logError;
            _UsuarioConsultasRepository = usuarioConsultasRepository;
            _ConsultasPersonas = consultasPersonas;
        }


        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Error_DTO objErrorDTO)
        {
            try
            {
                LoggerAPI objLooger = new LoggerAPI(_logError);

                if (objErrorDTO == null)
                    return BadRequest();


                bool respuesta = await objLooger.guardarError(objErrorDTO);

                return Ok();
            }
            catch (Exception ex)
            {

            }
            return BadRequest(MensajesRespuesta.guardarError());
        }
        #endregion

        #region get by id
        [HttpGet("{idLog}", Name = "GetLogById")]
        public async Task<IActionResult> GetLogById(Guid idLog)
        {
            try
            {
                LogsExcepcione company = _logError.ByIDLog(idLog);

                if (company == null)
                    return NotFound(MensajesRespuesta.sinResultados());


                LogErrorCompleto objDTO = _mapper.Map<LogErrorCompleto>(company);

                if (company.IdUsuario != null)
                {
                    Usuario obUsuarioRepository = await _UsuarioConsultasRepository.getUserById((Guid)company.IdUsuario);
                    Persona objPersona = await _ConsultasPersonas.obtenerPorIDPersona(obUsuarioRepository.IdPersona);

                    objDTO.IdUsuario = company.IdUsuario;
                    objDTO.IdPersona = objPersona.IdPersona;
                    objDTO.NombrePersona = objPersona.NombresPersona + " " + objPersona.ApellidosPersona;
                }


                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region Get by Advanced
        [HttpGet("BusquedaAvanzada")]
        public ActionResult<List<LogBusqueda>> BusquedaAvanzada(LogBusqueda objBusqueda)
        {
            try
            {
                List<LogsExcepcione> listaRepositorio = _logError.GetByDate(objBusqueda.FechaInicio, objBusqueda.FechaFin);

                if (listaRepositorio.Count < 1)
                    return NotFound(MensajesRespuesta.sinResultados());

                List<ResultadoBusquedaLogDTO> listaDTO = _mapper.Map<List<ResultadoBusquedaLogDTO>>(listaRepositorio);

                return Ok(listaDTO);
            }
            catch (Exception ex)
            {
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

    }
}
