using DTOs.Adeudo;
using DTOs.ConfiguracionCuenta;
using DTOs.Conjunto;
using DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using RecintosHabitacionales.Servicio;
using RecintosHabitacionales.Servicio.Interface;
using Utilitarios;

namespace RecintosHabitacionales.Controllers
{
    public class C_ConfigCuentaController : Controller
    {
        private readonly IServicioConsumoAPI<ConfiguraCuentasDTOCrear> _servicioConsumoAPIGestionar;
        private readonly IServicioConsumoAPI<ConfiguraCuentasDTOEditar> _servicioConsumoAPIGestionarEditar;

        public C_ConfigCuentaController(IServicioConsumoAPI<ConfiguraCuentasDTOCrear> servicioConsumoAPIGestionar, IServicioConsumoAPI<ConfiguraCuentasDTOEditar> servicioConsumoAPIGestionarEditar)
        {
            _servicioConsumoAPIGestionar = servicioConsumoAPIGestionar;
            _servicioConsumoAPIGestionarEditar = servicioConsumoAPIGestionarEditar;
        }

        public async Task<ActionResult> GestionarConfiguracion()
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            ViewData["listaConjuntos"] = objUsuarioSesion.ConjuntosAccesoSelect;

            ConfiguraCuentasDTOCompleto objConfigurar = await recuperarRegistro(objUsuarioSesion.IdConjuntoDefault);

            if(objConfigurar.Parametrizacion!= null)
            {
                foreach (var item in objUsuarioSesion.ListaConjuntosAcceso)
                {
                    if (objConfigurar.IdConjunto == item.IdConjunto)                    
                        objConfigurar.NombreConjunto = item.NombreConjunto;                    
                }
            }
            else
            {
                objConfigurar.IdConjunto = objUsuarioSesion.IdConjuntoDefault;
            }

            ViewData["objConfigurarCuenta"] = objConfigurar;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GestionarConfiguracionCuenta(ConfiguraCuentasDTOCompleto objCuentas)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            if (objUsuarioSesion == null)
                return RedirectToAction("Ingresar", "C_Ingreso");

            HttpResponseMessage respuesta = new HttpResponseMessage();

            if (objCuentas.IdConfiguracionCuenta == Guid.Empty)
            {
                ConfiguraCuentasDTOCrear objCrear = new ConfiguraCuentasDTOCrear();

                objCrear.IdConjunto = objCuentas.IdConjunto;
                objCrear.Parametrizacion = objCuentas.Parametrizacion;
                objCrear.UsuarioCreacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                respuesta = await _servicioConsumoAPIGestionar.consumoAPI(ConstantesConsumoAPI.gestionarConfiguracionAPI, HttpMethod.Post, objCrear);
            }
            else
            {
                ConfiguraCuentasDTOEditar objEditar = new ConfiguraCuentasDTOEditar();

                objEditar.IdConjunto = objCuentas.IdConjunto;
                objEditar.Parametrizacion = objCuentas.Parametrizacion;
                objEditar.UsuarioModificacion = FuncionesUtiles.construirUsuarioAuditoria(objUsuarioSesion);

                respuesta = await _servicioConsumoAPIGestionarEditar.consumoAPI(ConstantesConsumoAPI.gestionarConfiguracionAPIEditar + objCuentas.IdConfiguracionCuenta, HttpMethod.Post, objEditar);
            }           

            if (respuesta.IsSuccessStatusCode)
                return new JsonResult(LeerRespuestas<MensajesRespuesta>.procesarRespuestaCRUD(respuesta, "C_ConfigCuenta", "GestionarConfiguracion"));

            else
            {
                MensajesRespuesta objMensajeRespuesta = await respuesta.ExceptionResponse();
                return new JsonResult(objMensajeRespuesta);
            }

        }


        [HttpPost]
        public async Task<ActionResult> BusquedaAvanzadaConfigCuenta(Guid IdConjunto)
        {
            var objUsuarioSesion = Sesion<UsuarioSesionDTO>.recuperarSesion(HttpContext.Session, ConstantesAplicacion.nombreSesion);

            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

            if (objUsuarioSesion != null)
            {
                objConfigurar = await recuperarRegistro(IdConjunto);

                if (objConfigurar == null)
                {
                    objConfigurar = new ConfiguraCuentasDTOCompleto();
                    objConfigurar.IdConjunto = IdConjunto;
                }


                return View("_ConfiugracionCuenta", objConfigurar);
            }

            return RedirectToAction("Ingresar", "C_Ingreso");
        }

        private async Task<ConfiguraCuentasDTOCompleto> recuperarRegistro(Guid IdConjunto)
        {
            ConfiguraCuentasDTOCompleto objConfigurar = new ConfiguraCuentasDTOCompleto();

            try
            {
                HttpResponseMessage respuesta = await _servicioConsumoAPIGestionarEditar.consumoAPI(ConstantesConsumoAPI.buscarConfiguracion + IdConjunto, HttpMethod.Get);

                if (respuesta.IsSuccessStatusCode)
                    objConfigurar = await LeerRespuestas<ConfiguraCuentasDTOCompleto>.procesarRespuestasConsultas(respuesta);
            }
            catch (Exception ex)
            {
                objConfigurar = new ConfiguraCuentasDTOCompleto();
            }

            return objConfigurar;
        }
    }
}
