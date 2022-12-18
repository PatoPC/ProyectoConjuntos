using DTOs.Usuarios;
using GestionUsuarioDB.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Interface
{
    public interface IManageConsultasUsuario
    {
        #region Search
        public Task<Usuario> getUserById(Guid idUser);
        public Task<Usuario> getUserByIdPersona(Guid idPersona);
        public Task<Usuario> getUserByCorreo(string correoUser);
        public Task<List<Usuario>> getAllUsers();
        public Task<Usuario> getUserByIdPersonaPassword(Guid idPersona, string contrasena);
        public Task<Usuario> getLoginUser(string correoUser, string contrasena);
        public Task<List<Usuario>> GetUserAdvanced(ObjetoBusquedaUsuarios objBusqueda);

        public Task<Usuario> ValidaLogin(string email, string password);
        public Task<List<UsuarioConjunto>> getUsuariosEmpresasByIdUsuario(Guid idUser);
        #endregion
    }
}
