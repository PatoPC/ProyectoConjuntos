using AutoMapper;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RecintosHabitacionales.Filters;
using RecintosHabitacionales.Models;
using RecintosHabitacionales.Servicio.Interface;
using RecintosHabitacionales.Servicio;
using Utilitarios;
using DTOs.CatalogoGeneral;
using DTOs.Persona;
using DTOs.Conjunto;
using DTOs.Select;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RecintosHabitacionales.Controllers
{
    public class C_IngresoController : Controller
    {
        private readonly IServicioConsumoAPI<UsuarioDTOCompleto> _servicioConsumoAPI;
        private readonly IServicioConsumoAPI<CatalogoDTODropDown> _servicioConsumoAPICatalogos;

        //private readonly IServicioConsumoAPI<PersonaActualizarDTO> _servicioConsumoAPIActualizar;
        private readonly IServicioConsumoAPI<UsuarioCambioContrasena> _servicioConsumoAPICambioContrasena;
        private readonly IServicioConsumoAPI<OlvideMicontrasenaDTO> _servicioConsumoAPIOlvideMicontraseña;
        private readonly IMapper _mapper;
        public C_IngresoController(IServicioConsumoAPI<UsuarioDTOCompleto> servicioConsumoAPI, IMapper mapper, IServicioConsumoAPI<CatalogoDTODropDown> servicioConsumoAPICatalogos, IServicioConsumoAPI<UsuarioCambioContrasena> servicioConsumoAPICambioContrasena, IServicioConsumoAPI<OlvideMicontrasenaDTO> servicioConsumoAPIOlvideMicontraseña)
        {
            _servicioConsumoAPI = servicioConsumoAPI;
            _mapper = mapper;
            _servicioConsumoAPICatalogos = servicioConsumoAPICatalogos;
            _servicioConsumoAPICambioContrasena = servicioConsumoAPICambioContrasena;
            _servicioConsumoAPIOlvideMicontraseña = servicioConsumoAPIOlvideMicontraseña;
        }

        public IActionResult Ingresar(string sesionCaducada)
        {

            if (!string.IsNullOrEmpty(sesionCaducada))
            {
                ViewBag.errorLogin = sesionCaducada;
            }
            return View();
        }

        [HttpPost]
        [AccionesFiltro(inicioSesion = "Inicio")]
        public async Task<IActionResult> Ingresar(string email, string password, string InputToken)
        {

            //GoogleRecapchaData objGooogle = new GoogleRecapchaData
            //{
            //    secret = "6LfgZx4fAAAAABOjbFImRpsGxNO0kgudFsRB6RML",
            //    response = InputToken
            //};

            //HttpClient cliente = new HttpClient();

            //var resultado = await cliente.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={objGooogle.secret}&response={objGooogle.response}");

            //var objeto = JObject.Parse(resultado);
            //var banderaCapchaGoogle = (bool)objeto.SelectToken("success");

            //if (banderaCapchaGoogle)
            //{
            try
            {
                string nuevaContrasena = FuncionesContrasena.encriptarContrasena(password);
                string urlAPI_Ingresar = ConstantesConsumoAPI.getLogin + "user=" + email + "&contrasena=" + nuevaContrasena;
                HttpResponseMessage respuesta = await _servicioConsumoAPI.consumoAPI(urlAPI_Ingresar, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                {
                    UsuarioSesionDTO objUsuario = await LeerRespuestas<UsuarioSesionDTO>.procesarRespuestasConsultas(respuesta);

                    if (objUsuario != null)
                    {
                        if (objUsuario.Nombre != null)
                        {
                            if (!objUsuario.Estado)
                                ViewBag.errorLogin = "<div>Su usuario se encuentra desactivado, contactese con la empresa.</div>";
                            else
                            {
                                objUsuario.NombreUsuario = FuncionesUtiles.consturirNombreUsuario(objUsuario);
                                HttpContext.Session.Clear();


                                HttpResponseMessage respuestaConjuntos = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.TodosConjuntos, HttpMethod.Get);

                                List<ResultadoBusquedaConjuntos> listaConjuntosAcceso = await LeerRespuestas<List<ResultadoBusquedaConjuntos>>.procesarRespuestasConsultas(respuestaConjuntos);

                                SelectList SelectListaConjuntos = new SelectList(listaConjuntosAcceso, "IdConjunto", "NombreConjunto", objUsuario.IdConjuntoDefault);

                                objUsuario.ListaConjuntosAcceso = listaConjuntosAcceso;
                                objUsuario.ListaConjuntos = _mapper.Map<CustomSelectConjuntos>(SelectListaConjuntos);


                                SesionExtensions.SetObject(HttpContext.Session, ConstantesAplicacion.nombreSesion, objUsuario);





                                HttpResponseMessage respuestaPersona = await _servicioConsumoAPI.consumoAPI(ConstantesConsumoAPI.gestionarPersonaAPI + objUsuario.IdPersona, HttpMethod.Get);

                                PersonaDTOCompleto objDTO = await LeerRespuestas<PersonaDTOCompleto>.procesarRespuestasConsultas(respuestaPersona);

                                await cargaInicial();

                                if (!objUsuario.ContrasenaInicial)
                                {
                                    if (!String.IsNullOrEmpty(objUsuario.PaginaDefault))
                                    {
                                        string nombrePersona = FuncionesUtiles.primeraLetraMayuscula(objDTO.NombresPersona) + " " + FuncionesUtiles.primeraLetraMayuscula(objDTO.ApellidosPersona);

                                        string[] PaginaInicio = objUsuario.PaginaDefault.Split("/");

                                        return RedirectToAction(PaginaInicio[2], PaginaInicio[1], new { @nombrePersona = nombrePersona });
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                                else
                                {
                                    UsuarioCambioContrasena objUsuarioContrasena = _mapper.Map<UsuarioCambioContrasena>(objUsuario);

                                    return RedirectToAction("CambioContraseniaUsuario", "C_Ingreso", objUsuarioContrasena);
                                }
                            }
                        }
                        else
                        {
                            ViewBag.errorLogin = "Usuario y contraseña incorrecto.";
                        }
                    }
                    else
                    {
                        ViewBag.errorLogin = "Usuario o contraseña incorrecto.";
                    }
                }
                else
                {
                    MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();

                    ViewBag.errorLogin = "Error al intentar comunicarse con el servidor. "+ objMensajeRespuesta.message +" "+objMensajeRespuesta.state;
                }
            }
            catch (Exception ex)
            {
                ViewBag.errorLogin = "Ocurrió un error al intentar ingresar. ";
            }
            //}
            //    else
            //    {
            //        ViewBag.errorLogin = "Recaptcha incorrecto.";
            //    }

            return View();
        }

        public IActionResult OlvidoContrasena()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> OlvidoContrasena(OlvideMicontrasenaDTO objOlvideMicontraseñaDTO)
        {

            if (string.IsNullOrEmpty(objOlvideMicontraseñaDTO.email) && string.IsNullOrEmpty(objOlvideMicontraseñaDTO.identifiacion))
            {
                return new JsonResult(MensajesRespuesta.ReseteoContrasenaNoSellenoCamposRequeridos());
            }
            if (objOlvideMicontraseñaDTO != null)
            {
                HttpContext.Session.Clear();
                //EnviarCorreoOlvidoConstrasena
                HttpResponseMessage respuestaCatalogo = await _servicioConsumoAPIOlvideMicontraseña.consumoAPI(ConstantesConsumoAPI.apiOlvidoContrasena, HttpMethod.Post, objOlvideMicontraseñaDTO);

                if (respuestaCatalogo.IsSuccessStatusCode)
                {
                    return new JsonResult(MensajesRespuesta.guardarReseteoContrasena());
                }
                else
                {
                    ViewBag.errorLogin = "Ha ocurrido un error al intentar generar una nueva contraseña, por favor inténtelo más tarde.";
                }
            }
            else
            {
                ViewBag.errorLogin = "Por favor revisa su correo. ";

            }

            return View();
        }


        [HttpGet]
        public IActionResult CambioContraseniaUsuario(UsuarioCambioContrasena objUsuarioContrasena)
        {
            return View(objUsuarioContrasena);
        }


        [HttpPost]
        public async Task<IActionResult> CambioContraseniaUsuario(UsuarioCambioContrasena objUsuarioContrasena, string ContraseniaNueva, string ContraseniaConfirma)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion != null)
            {
                objUsuarioContrasena.UsuarioModificacion = objUsuarioSesion.IdUsuario + ";" + objUsuarioSesion.Nombre + " " + objUsuarioSesion.Apellido;
                objUsuarioContrasena.NuevaContrasena = FuncionesContrasena.encriptarContrasena(ContraseniaNueva);
                objUsuarioContrasena.ContraseniaConfirma = FuncionesContrasena.encriptarContrasena(ContraseniaConfirma);
                //apiUsuarioCambioContrasena
                HttpResponseMessage respuesta = await _servicioConsumoAPICambioContrasena.consumoAPI(ConstantesConsumoAPI.apiUsuarioCambioContrasena + objUsuarioContrasena.IdUsuario, HttpMethod.Post, objUsuarioContrasena);


                if (respuesta.IsSuccessStatusCode)
                {
                    if (!String.IsNullOrEmpty(objUsuarioSesion.PaginaDefault))
                    {
                        string[] PaginaInicio = objUsuarioSesion.PaginaDefault.Split("/");
                        return RedirectToAction(PaginaInicio[2], PaginaInicio[1]);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            ViewData["errorCambioContraseña"] = MensajesRespuesta.mensajeErrorCambioContrasena;
            return View(objUsuarioContrasena);
        }



        private async Task cargaInicial()
        {
            var listaTipoIdentificacionTask = DropDownsCatalogos<CatalogoDTODropDown>.procesarRespuestasConsultaCatlogoObjeto(_servicioConsumoAPICatalogos, ConstantesConsumoAPI.getGetCatalogosHijosPorCodigoPadre + ConstantesAplicacion.padreTipoIdentificacion);

            var listaTipoIdentificacion = await listaTipoIdentificacionTask;

            ViewData["listaTipoIdentificacion"] = listaTipoIdentificacion;




        }

    }
}
