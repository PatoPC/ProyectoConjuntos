using EntidadesPapelera.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioPapelera.Interface
{
    public interface IManageConsultasPepelera
    {
        #region Search
        public Task<DatosEliminado> GetCatalogoById(Guid idPapelera);
        public Task<List<DatosEliminado>> GetAllCatalogoPorFecha(DateTime fechaInicio, DateTime fechaFin);
       
        #endregion
    }
}
