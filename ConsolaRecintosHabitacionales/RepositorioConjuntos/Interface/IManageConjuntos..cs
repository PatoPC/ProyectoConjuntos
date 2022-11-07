using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageConjuntos
    {
        public Task<Conjunto> obtenerPorIDConjuntos(Guid idCondominio);
        public Task<List<Conjunto>> obtenerPorNombre(string nombreCondominio);
        public Task<List<Conjunto>> obtenerPorRUC(string ruc);
        public Task<List<Conjunto>> busquedaAvanzada(BusquedaConjuntos ruc);
    }
}
