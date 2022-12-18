using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionLogs.Entidades;
using RepositorioLogs.Interface;

namespace GestionLogs.Implementacion
{
    public class ManageLogError : IManageLogError
    {
        public ContextoDB_Logs _contextLogs;

        public ManageLogError(ContextoDB_Logs contextLogs)
        {
            _contextLogs = contextLogs;
        }


        #region CRUD
        public void AddLogError(LogsExcepcione objLogError)
        {
            objLogError.FechaError = DateTime.Now;
        }

        public async Task<bool> saveError(LogsExcepcione objLog)
        {
            try
            {
                _contextLogs = new ContextoDB_Logs();

                _contextLogs.LogsExcepciones.Add(objLog);

                var create = await _contextLogs.SaveChangesAsync();
                return create > 0;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _contextLogs.Dispose();
            }
            return false;
        }

        #endregion

        #region Consultas
        public LogsExcepcione ByIDLog(Guid idLog)
        {
            LogsExcepcione objRepositorio = _contextLogs.LogsExcepciones.Where(x => x.IdLogsExcepciones == idLog).FirstOrDefault();
          

            return objRepositorio;
        }

        public async Task<LogsExcepcione> ByIDUsuario(Guid idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<LogsExcepcione> GeByMetodo(string metodo)
        {
            throw new NotImplementedException();
        }

        public List<LogsExcepcione> GetByDate(DateTime fechaInicial, DateTime fechaFinal)
        {
            fechaFinal = fechaFinal.AddSeconds(59);
            fechaFinal = fechaFinal.AddMinutes(59);
            fechaFinal = fechaFinal.AddHours(23);

            List<LogsExcepcione> listaRepositorio = _contextLogs.LogsExcepciones.Where(x => x.FechaError >=fechaInicial && x.FechaError<=fechaFinal).ToList();


            return listaRepositorio;
        }
        #endregion

    }
}
