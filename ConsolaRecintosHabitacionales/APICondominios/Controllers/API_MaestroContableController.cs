using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.MaestroContable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_MaestroContableController : ControllerBase
    {
        private readonly IManageConjuntosCRUD<ConMst> _CRUD_ConMST;
        private readonly IMapper _mapper;
        private readonly IManageLogError _logError;
        private readonly IManageConMST _consultaMaestroCont;

        public API_MaestroContableController(IManageConjuntosCRUD<ConMst> cRUD_ConMST, IMapper mapper, IManageLogError logError, IManageConMST consultaMaestroCont)
        {
            _CRUD_ConMST = cRUD_ConMST;
            _mapper = mapper;
            _logError = logError;
            _consultaMaestroCont = consultaMaestroCont;
        }


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

                    return CreatedAtRoute("GetConjuntoByID", new { id = objCatalogoResult.IdConMst }, objCatalogoResult);
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


        #region Varios
        private async Task guardarLogs(string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(_logError);

            await objLooger.guardarError(this.ControllerContext.RouteData.Values["controller"].ToString(), this.ControllerContext.RouteData.Values["action"].ToString(), mensajeError, objetoJSON);

        }
        #endregion

    }
}
