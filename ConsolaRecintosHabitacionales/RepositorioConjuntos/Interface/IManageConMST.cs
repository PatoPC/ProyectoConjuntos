using ConjuntosEntidades.Entidades;
using DTOs.MaestroContable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageConMST
    {
        public Task<ConMst> obtenerPorIDConMST(Guid idConMST);
        public Task<List<ConMst>> obtenerPorCuenta(string numeroCuenta);
        public Task<List<ConMst>> obtenerPorNombreCuenta(string nombre);
        public Task<List<ConMst>> obtenerTodos();
        public Task<List<ConMst>> busquedaAvanzada(MaestroContableBusqueda objBusqueda);
    }
}
