using DTOs.Usuarios;
using GestionUsuarioDB.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioGestionUsuarios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Implementacion
{
    public class ManageConsultasUsuario : IManageConsultasUsuario
    {
        public readonly ContextoDB_Permisos _context;
        public ManageConsultasUsuario(ContextoDB_Permisos context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }
  
        #region Search
        public async Task<Usuario> getUserById(Guid idUser)
        {
            try
            {
                
                Usuario objUser = await _context.Usuarios.Where(c => c.IdUsuario == idUser).Include(x => x.UsuarioConjuntos).FirstOrDefaultAsync();

                return objUser;
            }
            catch (Exception ex)
            {
            }

            return null;
        } // End get user by id
        public async Task<Usuario> getUserByCorreo(string correoUser)
        {
            try
            {
                Usuario objUser = await _context.Usuarios.Where(c => c.CorreoElectronico == correoUser).FirstOrDefaultAsync();
                return objUser;
            }
            catch (Exception ex)
            {

            }

            return default;
        }// get user by name

        public async Task<Usuario> getLoginUser(string usuario, string contrasena)
        {
            try
            {
                    Usuario objUser = await _context.Usuarios.
                             Where(c => c.CorreoElectronico == usuario 
                             && c.Contrasena == contrasena
                             )
                             .Include(x => x.UsuarioConjuntos)
                             .ThenInclude(x => x.IdUsuairoConjunto).
                             Include(x => x.IdRolNavigation)
                             .ThenInclude(x => x.Modulos.OrderBy(x => x.Nombre))
                             .ThenInclude(x => x.Menus.OrderBy(x => x.NombreMenu))
                             .ThenInclude(x => x.Permisos)
                             .AsNoTracking()
                             .FirstOrDefaultAsync();

                    return objUser;
            }
            catch (Exception ex)
            {
            }

            return null;

        }// get user by name

        public async Task<Usuario> ValidaLogin(string email, string password)
        {
            Usuario objUsuario = new();
            try
            {
                objUsuario = await _context.Usuarios
                    .Where(x => 
                    (x.CorreoElectronico == email) 
                    && x.Contrasena == password)
                    .Include(x => x.IdRolNavigation)
                    .ThenInclude(x => x.Modulos)
                    .ThenInclude(x => x.Menus)
                    .ThenInclude(x => x.Permisos)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
            }
            return objUsuario;
        }
        public async Task<List<Usuario>> getAllUsers()
        {
            List<Usuario> listUseres = new List<Usuario>();
            try
            {
                listUseres = await _context.Usuarios.Select(p => p).ToListAsync();
            }
            catch (Exception exValidation)
            {
            }

            return listUseres;
        } //  end get all users 

        public async Task<List<Usuario>> GetUserAdvanced(ObjetoBusquedaUsuarios objBusqueda)
        {
            List<Usuario> listUser = new List<Usuario>();
            try
            {
                if (objBusqueda.IdRol == null)
                {
                    listUser = await _context.Usuarios.Select(x => x).Include(x => x.IdRolNavigation).Include(x => x.UsuarioConjuntos).ToListAsync();
                }
                else
                {
                    listUser = await _context.Usuarios.Where(x => x.IdRol == objBusqueda.IdRol).Include(x => x.IdRolNavigation).Include(x => x.UsuarioConjuntos).ToListAsync();
                }

                if(objBusqueda.IdConjunto!=null && objBusqueda.IdConjunto != Guid.NewGuid())
                {
                    listUser = listUser.Where(x => x.IdConjuntoDefault == objBusqueda.IdConjunto).ToList();
                }

            }
            catch (Exception ex)
            {
            }

            return listUser;
        }

        public async Task<Usuario> getUserByIdPersona(Guid idPersona)
        {
            try
            {
                Usuario objUser = await _context.Usuarios.Where(c => c.IdPersona == idPersona).Include(x => x.UsuarioConjuntos).FirstOrDefaultAsync();

                return objUser;
            }
            catch (Exception)
            {
            }

            return null;
        }

        public async Task<Usuario> getUserByIdPersonaPassword(Guid idPersona, string contrasena)
        {
            try
            {
                Usuario objUser = await _context.Usuarios.
                        Where(c => c.IdPersona == idPersona
                        && c.Contrasena == contrasena)
                        .Include(x => x.UsuarioConjuntos)                        
                        .Include(x => x.IdRolNavigation)
                        .ThenInclude(x => x.Modulos.OrderBy(x => x.Nombre))
                        .ThenInclude(x => x.Menus.OrderBy(x => x.NombreMenu))
                        .ThenInclude(x => x.Permisos).AsNoTracking()
                        .FirstOrDefaultAsync();


                return objUser;
            }
            catch (Exception ex)
            {
            }

            return null;

        }
        #endregion
         // usuariosEmpresa Tabla rompimiento 
         public async Task<List<UsuarioConjunto>> getUsuariosEmpresasByIdUsuario(Guid idUser) 
        {
            try
            {
                List<UsuarioConjunto> ListaEmpresaUsuario = await _context.UsuarioConjuntos.Where(x => x.IdUsuario == idUser).ToListAsync();
                return ListaEmpresaUsuario;
            }
            catch (Exception ex )
            {
        
            }
            return null;
        }

    }//class
}
