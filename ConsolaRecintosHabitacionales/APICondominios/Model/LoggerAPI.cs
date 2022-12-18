using DTOs.Error;
using GestionLogs.Entidades;
using RepositorioLogs.Interface;

namespace APICondominios.Model
{
    public class LoggerAPI
    {
        private readonly IManageLogError _logError;
        public LoggerAPI(IManageLogError logError)
        {
            _logError = logError;
        }

        public async Task guardarError(string nombreControlador, string accion, string mensaje, string jsonObjeto, Guid? idUsuario = null)
        {
            LogsExcepcione objLog = new LogsExcepcione();

            objLog.Entidad = nombreControlador;
            objLog.Metodo = accion;

            objLog.Error = mensaje;

            if (!string.IsNullOrEmpty(jsonObjeto))
            {
                objLog.Descripcion = jsonObjeto;
            }
            else
            {
                objLog.Descripcion = "Se recibio la variable/objeto jsonObjeto Vacia";
            }
            objLog.IdUsuario = idUsuario;

            _logError.AddLogError(objLog);

            bool resultado = await _logError.saveError(objLog);
        }

        public async Task guardarError(Error_DTO objErrorDTO)
        {
            LogsExcepcione objLog = new LogsExcepcione();

            objLog.Entidad = objErrorDTO.nombreControlador;
            objLog.Metodo = objErrorDTO.accion;

            objLog.Error = objErrorDTO.mensaje;
            objLog.Descripcion = objErrorDTO.jsonObjeto;

            _logError.AddLogError(objLog);

            bool resultado = await _logError.saveError(objLog);
        }
    }
}
