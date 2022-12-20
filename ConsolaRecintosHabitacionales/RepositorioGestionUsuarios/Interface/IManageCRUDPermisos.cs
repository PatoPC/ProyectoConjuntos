using GestionUsuarioDB.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Interface
{
    public interface IManageCRUDPermisos<T> where T : class
    {
        #region CRUD
        public void Add(T obj);
        public void Edit(T obj);
        public void EditUltimoIngreso(Usuario obj);
        public List<Modulo> EditRol(Rol objRol, List<Modulo> listaModulos);
        public void Delete(T obj);
        public void DeleteRange(List<UsuarioConjunto> listaUsuariosConjuntos);
        public Task<(bool estado, string mensajeError)> save();
        #endregion
    }
}