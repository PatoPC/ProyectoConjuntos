﻿using APICondominios.Model;
using AutoMapper;
using ConjuntosEntidades.Entidades;
using DTOs.Departamento;
using DTOs.Departamento;
using DTOs.Torre;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RepositorioConjuntos.Interface;
using RepositorioLogs.Interface;
using Utilitarios;

namespace APICondominios.Controllers
{
	[Route("api/APIDepartamento")]
	[ApiController]
	public class API_DepartamentoController : ControllerBase
	{
		private readonly IManageConjuntosCRUD<Departamento> _CRUD_Departamento;
		private readonly IManageConjuntosCRUD<AreasDepartamento> _CRUD_AreaDepartamento;
		private readonly IManageDepartamento _Departamentos;
		private readonly IMapper _mapper;
		private readonly IManageLogError _logError;

		public API_DepartamentoController(IMapper mapper, IManageConjuntosCRUD<Departamento> cRUD_Condominio, IManageDepartamento torres, IManageLogError logError, IManageConjuntosCRUD<AreasDepartamento> cRUD_AreaDepartamento)
		{
			_mapper = mapper;
			_CRUD_Departamento = cRUD_Condominio;
			_Departamentos = torres;
			_logError = logError;
			_CRUD_AreaDepartamento = cRUD_AreaDepartamento;
		}

		#region CRUD
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] DepartamentoDTOCrear objDTO)
		{
			try
			{
				if (objDTO == null)
					return BadRequest(MensajesRespuesta.noSePermiteObjNulos());

				Departamento objRepositorio = _mapper.Map<Departamento>(objDTO);
				_CRUD_Departamento.Add(objRepositorio);

				var result = await _CRUD_Departamento.save();

				if (result.estado)
				{
					DepartamentoDTOCompleto objDTOResultado = _mapper.Map<DepartamentoDTOCompleto>(objRepositorio);

					return CreatedAtRoute("GetDepartamentoByID", new { id = objDTOResultado.IdTorres }, objDTOResultado);
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
		public async Task<IActionResult> Editar(Guid id, DepartamentoDTOEditar objDTO)
		{
			try
			{
				Departamento objRepository = await _Departamentos.obtenerPorIDDepartamento(id);

				List<AreasDepartamento> AreasDepartamentos = objRepository.AreasDepartamentos.ToList();

				var resultadoAreas = await _CRUD_AreaDepartamento.DeleteRange(AreasDepartamentos);
				//var resultadoAreas = await _CRUD_AreaDepartamento.save();

				_mapper.Map(objDTO, objRepository);

				_CRUD_Departamento.Edit(objRepository);
				var result = await _CRUD_Departamento.save();
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

			Departamento objRepositorio = await _Departamentos.obtenerPorIDDepartamento(id);

			List<AreasDepartamento> AreasDepartamentos = objRepositorio.AreasDepartamentos.ToList();

			var resultadoAreas = await _CRUD_AreaDepartamento.DeleteRange(AreasDepartamentos);
			//var resultadoAreas = await _CRUD_AreaDepartamento.save();

			_CRUD_Departamento.Delete(objRepositorio);
			var result = await _CRUD_Departamento.save();

			//Se comprueba que se actualizó correctamente
			if (result.estado)
				return NoContent();
			else
				await guardarLogs(JsonConvert.SerializeObject(objRepositorio, jsonSerializerSettings), result.mensajeError);


			return BadRequest();
		}

		#endregion

		[HttpGet("{id}", Name = "GetDepartamentoByID")]
		public async Task<IActionResult> GetDepartamentoByID(Guid id)
		{
			try
			{
				Departamento objRepositorio = await _Departamentos.obtenerPorIDDepartamento(id);
				if (objRepositorio == null)
					return NotFound(MensajesRespuesta.sinResultados());

				DepartamentoDTOCompleto objDTO = _mapper.Map<DepartamentoDTOCompleto>(objRepositorio);

				return Ok(objDTO);
			}
			catch (Exception ex)
			{
				await guardarLogs("GetDepartamentoByID: id " + id.ToString(), ex.ToString());
			}

			return StatusCode(StatusCodes.Status500InternalServerError);
		}


		[HttpGet("ObtenerTorresAvanzado")]
		public async Task<ActionResult<List<DepartamentoDTOCompleto>>> ObtenerTorresAvanzado(DepartamentoBusquedaDTO objBusqueda)
		{
			List<Departamento> listaResultado = new List<Departamento>();

			listaResultado = await _Departamentos.busquedaAvanzadaDepartamento(objBusqueda);

			if (listaResultado.Count < 1)
				return NotFound(MensajesRespuesta.sinResultados());

			List<DepartamentoDTOCompleto> listaResultadoDTO = _mapper.Map<List<DepartamentoDTOCompleto>>(listaResultado);

            listaResultadoDTO = listaResultadoDTO.OrderBy(x => x.CodigoDepartamento).ToList();

            return Ok(listaResultadoDTO);
		}

		[HttpGet("ObtenerDepartamentoPorIDTorre")]
		public async Task<ActionResult<List<DepartamentoDTOCompleto>>> ObtenerDepartamentoPorIDTorre(Guid idTorre)
		{
			List<Departamento> listaResultado = await _Departamentos.obtenerPorDeparta_IDTorre(idTorre);


			if (listaResultado.Count < 1)
				return NotFound(MensajesRespuesta.sinResultados());


			List<DepartamentoDTOCompleto> listaResultadoDTO = _mapper.Map<List<DepartamentoDTOCompleto>>(listaResultado);


			return Ok(listaResultadoDTO);
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
