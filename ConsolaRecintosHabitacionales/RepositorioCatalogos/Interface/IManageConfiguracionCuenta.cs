using EntidadesCatalogos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCatalogos.Interface
{
    public interface IManageConfiguracionCuenta
    {

        #region Search
        public Task<Configuracioncuentum> GetConfiguracionCuenta(Guid idConfiguracionCuenta);
        public Task<Configuracioncuentum> GetConfigCuentaConjunto(Guid idConjunto);
       
        #endregion
    }
}
