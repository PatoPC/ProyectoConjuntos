using GestionUsuarioDB.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Interface
{
    public interface IManageConsultasPermisos
    {
        public Task<List<Rol>> GetAllRolsByConjuntos();
        public Task<Rol> GetRolByID(Guid IdRol);
        public Task<List<Rol>> GetRolPorNombre(string nombreRol);

    }
}
