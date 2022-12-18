
using GestionLogs.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioLogs.Interface
{
    public interface IManageLogError
    {
        #region CRUD
        public void AddLogError(LogsExcepcione objLogError);
        public Task<bool> saveError(LogsExcepcione objLog);
        #endregion

        #region Search
        public LogsExcepcione ByIDLog(Guid idLog);
        public Task<LogsExcepcione> ByIDUsuario(Guid idUsuario);
        public Task<LogsExcepcione> GeByMetodo(string metodo);
        public List<LogsExcepcione> GetByDate(DateTime fechaInicial, DateTime fechaFinal);
        #endregion
    }
}
