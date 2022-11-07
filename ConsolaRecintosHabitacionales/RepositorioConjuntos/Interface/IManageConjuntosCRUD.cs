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
        public void AddRange(T obj);
        public void Edit(T obj);
        public void Delete(T obj);
        public Task<(bool estado, string mensajeError)> save();
        #endregion

    }
}
