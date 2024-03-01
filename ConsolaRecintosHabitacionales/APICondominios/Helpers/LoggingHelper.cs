using APICondominios.Model;
using Microsoft.AspNetCore.Mvc;
using RepositorioLogs.Interface;

namespace APICondominios.Helpers
{
    public static class LoggingHelper
    {
        public static async Task GuardarLogsAsync(IManageLogError logError, ControllerBase controller, string objetoJSON, string mensajeError)
        {
            LoggerAPI objLooger = new LoggerAPI(logError);

            await objLooger.guardarError(
                controller.ControllerContext.RouteData.Values["controller"].ToString(),
                controller.ControllerContext.RouteData.Values["action"].ToString(),
                mensajeError,
                objetoJSON
            );
        }
    }
}
