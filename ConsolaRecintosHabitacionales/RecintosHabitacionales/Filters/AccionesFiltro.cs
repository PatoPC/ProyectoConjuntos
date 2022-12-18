using DTOs.Usuarios;
using Newtonsoft.Json;
using Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace RecintosHabitacionales.Filters
{
    public class AccionesFiltro : ActionFilterAttribute
    {
        public string inicioSesion { get; set; }
        public string sesionHome { get; set; }
        public string nombreModulo { get; set; }
        public string nombreMenu { get; set; }
        public string tipoPermiso { get; set; }
        public bool concedido { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            bool banderaContinuar = false;
            try
            {
                string json = actionExecutingContext.HttpContext.Session.GetString(ConstantesAplicacion.nombreSesion);

                if (nombreModulo != null)
                {
                    var objSesion = JsonConvert.DeserializeObject<UsuarioSesionDTO>(json);

                    var modulo = objSesion.Modulos.Where(x => x.Nombre == nombreModulo).FirstOrDefault();

                    if (modulo != null)
                    {
                        var menu = modulo.Menus.Where(x => x.NombreMenu == nombreMenu).FirstOrDefault();
                        if (menu != null)
                        {
                            var permiso = menu.Permisos.Where(x => x.NombrePermiso == tipoPermiso).FirstOrDefault();
                            if (permiso != null)
                            {
                                if (permiso.Concedido == concedido)
                                {
                                    banderaContinuar = true;
                                }
                            }
                        }
                    }
                    if (!banderaContinuar)
                    {
                        actionExecutingContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "SinPermisos",
                            returnurl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(actionExecutingContext.HttpContext.Request)
                        }));
                    }
                }


            }
            catch (Exception ex)
            {

                actionExecutingContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "C_Ingreso",
                    action = "Ingresar",
                    returnurl = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetEncodedUrl(actionExecutingContext.HttpContext.Request)
                }));
            }

        }
    }
}
