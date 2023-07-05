using ConjuntosEntidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageConjuntosCRUD<T> where T : class
    {
        #region CRUD
        public void Add(T obj);
        public Task<(bool estado, string mensajeError)> saveRangeConjunto(List<Conjunto> listaConjuntos);
        public Task<(bool estado, string mensajeError)> saveRangeMaestro(List<ConMst> listaMaestro);
        public Task<(bool estado, string mensajeError)> saveRangeAdeudo(List<Adeudo> listaMaestro);
        
        public void Edit(T obj);
        public void Delete(T obj);
        //public void DeleteRango(List<AreasDepartamento> lista);
        public Task<(bool estado, string mensajeError)> DeleteRange(List<Conjunto> lista);
        public Task<(bool estado, string mensajeError)> DeleteRange(List<Torre> lista);
        public Task<(bool estado, string mensajeError)> DeleteRange(List<Departamento> lista);
        public Task<(bool estado, string mensajeError)> DeleteRange(List<AreasDepartamento> lista);
        public Task<(bool estado, string mensajeError)> save();
        #endregion

    }
}
