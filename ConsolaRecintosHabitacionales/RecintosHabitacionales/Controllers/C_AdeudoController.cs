﻿using DTOs.Adeudo;
using DTOs.CatalogoGeneral;
using DTOs.Persona;
using DTOs.Proveedor;
using DTOs.Torre;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_AdeudoController : Controller
    {
        private readonly IServicioConsumoAPI<BusquedaTorres> _servicioConsumoAPIBusqueda;
        private readonly IServicioConsumoAPI<List<AdeudoDTOCrear>> _servicioConsumoAPICrear;

        public C_AdeudoController(IServicioConsumoAPI<BusquedaTorres> servicioConsumoAPIBusqueda, IServicioConsumoAPI<List<AdeudoDTOCrear>> servicioConsumoAPICrear)
        {
            _servicioConsumoAPIBusqueda = servicioConsumoAPIBusqueda;
            _servicioConsumoAPICrear = servicioConsumoAPICrear;
        }

        public IActionResult GestionarAdeudo()
        {
            return View();
        }

        public IActionResult GenearAdeudo()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<int> listaAnios = obtenerAnios().ToList();

                ViewData["listaAnios"] = listaAnios;
              
                ViewData["listaConjuntos"] = objUsuarioSesion.ConjutosAccesoSelect;            

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }
        [HttpPost]
        public async Task<ActionResult> GenearAdeudo(GenerarAdeudo variable)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                List<AdeudoDTOCrear> listaAdeudos = new List<AdeudoDTOCrear>();
                DateTime fechaADeudoActual = FuncionesUtiles.ObtenerUltimoDiaDelMes(variable.mes, variable.anio);
                if (variable.IdConjunto!=null)
                {

                    BusquedaTorres objBusquedaTorres = new BusquedaTorres();
                    objBusquedaTorres.IdConjunto = (Guid) variable.IdConjunto;

                    List<TorreDTOCompleto> listaResultado = new List<TorreDTOCompleto>();

                    HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.buscarTorresAvanzado, HttpMethod.Get, objBusquedaTorres);


                    CatalogoDTOResultadoBusqueda objCataArrendatario = await tipoPersonaDepartmento(ConstantesAplicacion.tipoPersonaCondomino);

                    //CatalogoDTOResultadoBusqueda objCataDueno = await tipoPersonaDepartmento(ConstantesAplicacion.tipoPersonaPropietario);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        listaResultado = await LeerRespuestas<List<TorreDTOCompleto>>.procesarRespuestasConsultas(respuesta);

                        foreach (TorreDTOCompleto torre in listaResultado)
                        {
                            foreach (var departamento in torre.Departamentos)
                            {
                                AdeudoDTOCrear objAdeudo = new AdeudoDTOCrear();

                                objAdeudo.IdDepartamento = departamento.IdDepartamento;
                                objAdeudo.MontoAdeudos = departamento.AliqDepartamento;
                                objAdeudo.EstadoAdeudos = false;
                                objAdeudo.FechaAdeudos = fechaADeudoActual;
                                objAdeudo.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                                if (departamento.TipoPersonas != null)
                                {
                                    if (departamento.TipoPersonas.Count>0)
                                    {
                                        var personaArrend = departamento.TipoPersonas.Where(x => x.IdTipoPersonaDepartamento == objCataArrendatario.IdCatalogo).FirstOrDefault();
                                        objAdeudo.FechaCreacion = DateTime.Now;
                                        if (personaArrend != null)
                                        {
                                            objAdeudo.IdPersona = (Guid)personaArrend.IdPersona;
                                            objAdeudo.NombresPersona = personaArrend.NombrePersona;
                                           
                                        }
                                        else
                                        {
                                            objAdeudo.IdPersona = (Guid)departamento.TipoPersonas.FirstOrDefault().IdPersona;
                                            objAdeudo.NombresPersona = departamento.TipoPersonas.FirstOrDefault().NombrePersona;
                                        } 

                                        listaAdeudos.Add(objAdeudo);
                                    }

                                }
                            }
                        }

                        if (listaAdeudos.Count > 0)
                        {
                            HttpResponseMessage httpCrearAdeudo = await _servicioConsumoAPICrear.consumoAPI(ConstantesConsumoAPI.gestionarAdeudoAPI, HttpMethod.Post, listaAdeudos);

                            if (httpCrearAdeudo.IsSuccessStatusCode)
                            {
                                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta));
                            }
                            else
                            {
                                MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                                return new JsonResult(objMensajeRespuesta);
                            }
                        }
                    }
                       
                }

                return View();
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        /// <summary>
        /// Recpera los catalogos de acuerdo al codigo catalogo, para saber si es un arrendatario o un dueño de departamento
        /// </summary>
        /// <param name="codigoPersona"></param>
        private async Task<CatalogoDTOResultadoBusqueda> tipoPersonaDepartmento(string codigoPersona)
        {
            HttpResponseMessage respuesta = await _servicioConsumoAPIBusqueda.consumoAPI(ConstantesConsumoAPI.getCodigoCatalogo + codigoPersona, HttpMethod.Get);

            var objCatalogo = await LeerRespuestas<CatalogoDTOResultadoBusqueda>.procesarRespuestasConsultas(respuesta);

            return objCatalogo;

        }

        public static IEnumerable<int> obtenerAnios()
        {
            int currentYear = DateTime.Now.Year;
            while (currentYear >= 2023)
            {
                yield return currentYear;
                currentYear--;
            }
        }

    }
}
