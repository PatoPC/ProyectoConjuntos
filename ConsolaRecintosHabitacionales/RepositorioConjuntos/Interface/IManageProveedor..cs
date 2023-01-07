using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using DTOs.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageProveedor
    {
        public Task<Proveedore> obtenerPorIDProveedor(Guid idProveedor);
        public Task<List<Proveedore>> obtenerPorNombre(string nombreProducto);
        public Task<List<Proveedore>> obtenerPorRUC(string ruc);
        public Task<List<Proveedore>> busquedaAvanzada(BusquedaProveedor objBusqueda);
        public Task<List<Proveedore>> busquedaTodosProveedor(Guid idConjuunto);
    }
}
