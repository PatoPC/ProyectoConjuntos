using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Parametro;
using DTOs.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageParametro
    {
        public Task<Parametro> obtenerPorIDParametro(Guid idParametro);        
        public Task<List<Parametro>> busquedaAvanzada(BusquedaParametro objBusqueda);
        public Task<Parametro> obtenerParametroPorIDCatalogo(Guid idModuloCatalogo);
    }
}
