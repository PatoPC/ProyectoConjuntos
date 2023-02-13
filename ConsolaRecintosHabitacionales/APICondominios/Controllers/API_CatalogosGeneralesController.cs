using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.CatalogoGeneral;
using DTOs.Varios;
using EntidadesCatalogos.Entidades;
using EntidadesPapelera.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioCatalogos.Interface;
using RepositorioLogs.Interface;
using RepositorioPapelera.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
    [Route("api/Catalogo")]
    [ApiController]
    public class API_CatalogosGeneralesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IManageCRUDCatalogo<Catalogo> _CRUD_Catalogo;
        private readonly IManageConsultasCatalogos _CatalogoConsultas;
        private readonly IManageCRUDPapelera<DatosEliminado> _CRUDRepositoryPapeleta;

        private readonly IManageLogError _logError;

        public API_CatalogosGeneralesController(IMapper mapper, IManageCRUDCatalogo<Catalogo> CRUD_Catalogo, IManageLogError logError, IManageConsultasCatalogos catalogoContext, IManageConsultasCatalogos catalogoTarjetasBusquedas, IManageCRUDPapelera<DatosEliminado> cRUDRepositoryPapeleta)
        {
            _mapper = mapper;
            _CRUD_Catalogo = CRUD_Catalogo;
            _logError = logError;
            _CatalogoConsultas = catalogoContext;
            _CRUDRepositoryPapeleta = cRUDRepositoryPapeleta;
        }

        #region CRUD
        #region Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CatalogoDTOCrear objCatalogoDTO)
        {
            try
            {
                if (objCatalogoDTO == null)
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }

                Catalogo objCatalogo = _mapper.Map<Catalogo>(objCatalogoDTO);
                _CRUD_Catalogo.Add(objCatalogo);

                var result = await _CRUD_Catalogo.save();

                if (result.estado)
                {
                    CatalogoDTOCompleto objCatalogoResult = _mapper.Map<CatalogoDTOCompleto>(objCatalogo);

                    return CreatedAtRoute("GetCatalogoById", new { idCatalogo = objCatalogo.IdCatalogo }, objCatalogoResult);
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), result.mensajeError);
                }
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), ExValidation.ToString());
            }
            return BadRequest(MensajesRespuesta.guardarError());
        }


        #endregion

        #region Edit
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(Guid idCatalogo, CatalogoDTOActualizar objCatalogoDTO)
        {
            try
            {
                if (objCatalogoDTO == null)
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }
                Catalogo objCatalogoRepository = await _CatalogoConsultas.GetCatalogoById(idCatalogo);

                _mapper.Map(objCatalogoDTO, objCatalogoRepository);
                if (objCatalogoRepository.IdCatalogopadre == ConstantesAplicacion.guidNulo)
                {
                    objCatalogoRepository.IdCatalogopadre = null;
                }
                _CRUD_Catalogo.Edit(objCatalogoRepository);
                var result = await _CRUD_Catalogo.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                {
                    return NoContent();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), result.mensajeError);
                }

                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), ExValidation.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        [HttpPost("EditarCatalogoNivel")]
        public async Task<IActionResult> EditarCatalogoNivel(CatalogoDTOEditarNivel objCatalogoDTO)
        {
            try
            {
                if (objCatalogoDTO == null)
                {
                    return BadRequest(MensajesRespuesta.noSePermiteObjNulos());
                }
                Catalogo objCatalogoRepository = await _CatalogoConsultas.GetCatalogoById(objCatalogoDTO.IdCatalogo);
                _mapper.Map(objCatalogoDTO, objCatalogoRepository);

                _CRUD_Catalogo.Edit(objCatalogoRepository);
                var result = await _CRUD_Catalogo.save();
                // se comprueba que se actualizo correctamente
                if (result.estado)
                {
                    return NoContent();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), result.mensajeError);
                }

                return BadRequest(MensajesRespuesta.guardarError());
            }
            catch (Exception ExValidation)
            {
                await guardarLogs(JsonConvert.SerializeObject(objCatalogoDTO), ExValidation.ToString());
            }
            return StatusCode(StatusCodes.Status406NotAcceptable);
        }

        #endregion

        #region Delete
        
        [HttpPost("DeleteCatalogo")]
        public async Task<IActionResult> DeleteCatalogo(ObjetoEliminar objEliminar)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            try
            {

                Catalogo objCatalogoRepository = await _CatalogoConsultas.GetCatalogoById(objEliminar.IdObjetoEliminado);

                DatosEliminado objDatoElminado = new DatosEliminado();
                _mapper.Map(objEliminar, objDatoElminado);
                objDatoElminado.DatosEliminados = JsonConvert.SerializeObject(objCatalogoRepository, jsonSerializerSettings);
                objDatoElminado.TipoDatoEliminado = objCatalogoRepository.GetType().FullName;

                if (objCatalogoRepository == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                _CRUD_Catalogo.Delete(objCatalogoRepository);
                var result = await _CRUD_Catalogo.save();

                //Se comprueba que se actualizó correctamente
                if (result.estado)
                {
                    _CRUDRepositoryPapeleta.Add(objDatoElminado);
                    var resultPapelera = await _CRUDRepositoryPapeleta.save();

                    return NoContent();
                }
                else
                {
                    await guardarLogs(JsonConvert.SerializeObject(objCatalogoRepository, jsonSerializerSettings), result.mensajeError);
                }
            }
            catch (Exception ex)
            {
                await guardarLogs(JsonConvert.SerializeObject(objEliminar, jsonSerializerSettings), ex.ToString());
            }

            return BadRequest();
        }
        
        #endregion

        #endregion

    
        #region Search

        #region Get Catalogo Por Nivel
        [HttpGet("GetCatalogosByLevel")]
        public async Task<ActionResult<CatalogoDTOCompleto>> GetCatalogosByLevel(int nivelCatalogo)
        {
            try
            {
                List<Catalogo> listaRepositorio = await _CatalogoConsultas.GetAllCatalogosPorNivel(nivelCatalogo);
                if (listaRepositorio != null)
                {
                    if (listaRepositorio.Count() <= 0)
                        return NotFound(MensajesRespuesta.sinResultados());
                }

                List<CatalogoDTOCompleto> listaDTO = _mapper.Map<List<CatalogoDTOCompleto>>(listaRepositorio);

                return Ok(listaDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region Get Catalogo by ID
        [HttpGet("{idCatalogo}", Name = "GetCatalogoById")]
        public async Task<ActionResult<CatalogoDTOCompleto>> GetCatalogoById(Guid idCatalogo)
        {
            try
            {
                Catalogo Catalogo = await _CatalogoConsultas.GetCatalogoById(idCatalogo);
                if (Catalogo == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                CatalogoDTOCompleto objCatalogoDTO = _mapper.Map<CatalogoDTOCompleto>(Catalogo);
                return Ok(objCatalogoDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion


        #region Get All GetAllCatalogosByIDConjunto
        [HttpGet("GetAllCatalogosByIDConjunto")]
        public async Task<ActionResult<List<CatalogoDTOResultadoBusqueda>>> GetAllCatalogosByIDConjunto(Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listCatalogo = await _CatalogoConsultas.GetAllCatalogoByConjunto(idEmpresa);
                if (listCatalogo.Count() < 1)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTOResultadoBusqueda> listCajaDTO = _mapper.Map<List<CatalogoDTOResultadoBusqueda>>(listCatalogo);

                listCajaDTO = listCajaDTO.OrderBy(x => x.NombreCatalogo).ToList();

                return Ok(listCajaDTO);
            }
            catch (Exception Ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetAllCatalogosWithOutConjunto")]
        public async Task<ActionResult<List<CatalogoDTOResultadoBusqueda>>> GetAllCatalogosWithOutConjunto()
        {
            try
            {
                List<Catalogo> listCatalogo = await _CatalogoConsultas.GetAllCatalogos();
                if (listCatalogo.Count() < 1)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTOResultadoBusqueda> listCajaDTO = _mapper.Map<List<CatalogoDTOResultadoBusqueda>>(listCatalogo);

                listCajaDTO = listCajaDTO.OrderBy(x => x.NombreCatalogo).ToList();

                return Ok(listCajaDTO);
            }
            catch (Exception Ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region Get By Name y Empresa
        [HttpGet("GetCatalogoByNameIdConjunto")]
        public async Task<ActionResult<List<CatalogoDTOCompleto>>> GetCatalogoByNameIdConjunto(string nameCatalogo, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listCatalago = await _CatalogoConsultas.GetCatalogoByNameIdConjunto(nameCatalogo, idEmpresa);
                if (listCatalago == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTOCompleto> listCatalogoDTO = _mapper.Map<List<CatalogoDTOCompleto>>(listCatalago);

                listCatalogoDTO = listCatalogoDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listCatalogoDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region Get By Name
        [HttpGet("GetCatalogoByName")]
        public async Task<ActionResult<List<CatalogoDTOCompleto>>> GetCatalogoByName(string nameCatalogo)
        {
            try
            {
                List<Catalogo> listCatalago = await _CatalogoConsultas.GetCatalogoByName(nameCatalogo);
                if (listCatalago == null)                
                    return NotFound(MensajesRespuesta.sinResultados());
                
                if (listCatalago.Count() == 0)                
                    return NotFound(MensajesRespuesta.sinResultados());
                
                List<CatalogoDTOCompleto> listCatalogoDTO = _mapper.Map<List<CatalogoDTOCompleto>>(listCatalago);

                listCatalogoDTO = listCatalogoDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listCatalogoDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

         [HttpGet("GetCatalogoByNameExact")]
        public async Task<ActionResult<List<CatalogoDTOCompleto>>> GetCatalogoByNameExact(string nameCatalogo)
        {
            try
            {
                Catalogo objRepositorio = await _CatalogoConsultas.GetCatalogoByNameExact(nameCatalogo);
                if (objRepositorio == null)                
                    return NotFound(MensajesRespuesta.sinResultados());
                
              
                
                CatalogoDTOCompleto objDTO = _mapper.Map<CatalogoDTOCompleto>(objRepositorio);

                return Ok(objDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }



        #endregion

        #region get by Codigo
        [HttpGet("GetCatalogoByCodeIDConjunto")]
        public async Task<ActionResult<CatalogoDTOResultadoBusqueda>> GetCatalogoByCodeIDEmpresa(string codigoCatalgo, Guid idEmpresa)
        {
            try
            {
                Catalogo catalago = await _CatalogoConsultas.GetCatalogoByCodeIDEmpresa(codigoCatalgo, idEmpresa);

                if (catalago == null)
                    return NotFound(MensajesRespuesta.sinResultados());

                CatalogoDTOResultadoBusqueda objCatalogoDTO = _mapper.Map<CatalogoDTOResultadoBusqueda>(catalago);
                return Ok(objCatalogoDTO);
            }
            catch (Exception ex)
            {

            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region get Childs
        [HttpGet("GetCatalogsChildsIDConjunto")]
        public async Task<ActionResult<List<CatalogoDTOResultadoBusqueda>>> GetCatalogsChildsIDConjunto(string codigoPadreCatalgo, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetChildByParentCodeIDEmpresa(codigoPadreCatalgo, idEmpresa);

                List<CatalogoDTOResultadoBusqueda> listaCatalogosDTO = _mapper.Map<List<CatalogoDTOResultadoBusqueda>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.NombreCatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("GetCatalogsChildsIDConjuntoIDCatalogoPadre")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsChildsIDConjuntoIDCatalogoPadre(Guid idCatalogoPadre, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetCatalogsChildsIDConjuntoIDCatalogoPadre(idCatalogoPadre, idEmpresa);

                List<CatalogoDTOResultadoBusqueda> listaCatalogosDTO = _mapper.Map<List<CatalogoDTOResultadoBusqueda>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.NombreCatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


        [HttpGet("GetCatalogsChildsByIDFather")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsChildsByIDFather(Guid idCodigoPadreCatalgo)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetCatalogsChildsByIDFather(idCodigoPadreCatalgo);

                List<CatalogoDTOResultadoBusqueda> listaCatalogosDTO = _mapper.Map<List<CatalogoDTOResultadoBusqueda>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.NombreCatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region get Childs by ID Fathers
        [HttpGet("GetCatalogsChildsByID_Conjunto")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsChildsByID_Conjunto(Guid idPadre, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetChildByParentCodeByID_IDEmpresa(idPadre, idEmpresa);

                List<CatalogoDTODropDown> listaCatalogosDTO = _mapper.Map<List<CatalogoDTODropDown>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region get Childs by Name Fathers
        //Este solo se debe ocupar para roles
        [HttpGet("GetCatalogsChildsByName_ConjuntoRol")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsChildsByName_ConjuntoRol(string nombrePadre)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetChildByParentCodeByID_Nombre(nombrePadre);
                if (listaCatalogos == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTODropDown> listaCatalogosDTO = _mapper.Map<List<CatalogoDTODropDown>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region get Catalogos un nivel Arriba
        [HttpGet("GetCatalogsUpLevel_Conjunto")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsUpLevel_Conjunto(Guid idCatalogo, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetCatalogsUpLevel_Conjunto(idCatalogo, idEmpresa);
                if (listaCatalogos == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTODropDown> listaCatalogosDTO = _mapper.Map<List<CatalogoDTODropDown>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion

        #region get Childs by ID Fathers
        [HttpGet("GetCatalogsSameLevelByID_Conjunto")]
        public async Task<ActionResult<List<CatalogoDTODropDown>>> GetCatalogsSameLevelByID_Conjunto(Guid idCatalogoHermano, Guid idEmpresa)
        {
            try
            {
                List<Catalogo> listaCatalogos = await _CatalogoConsultas.GetCatalogsSameLevelByID_Conjunto(idCatalogoHermano, idEmpresa);
                if (listaCatalogos == null)
                {
                    return NotFound(MensajesRespuesta.sinResultados());
                }
                List<CatalogoDTODropDown> listaCatalogosDTO = _mapper.Map<List<CatalogoDTODropDown>>(listaCatalogos);

                listaCatalogosDTO = listaCatalogosDTO.OrderBy(x => x.Nombrecatalogo).ToList();

                return Ok(listaCatalogosDTO);
            }
            catch (Exception ex)
            {

            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

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
