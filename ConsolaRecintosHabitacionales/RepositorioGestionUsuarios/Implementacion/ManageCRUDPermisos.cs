using GestionUsuarioDB.Entidades;
using RepositorioGestionUsuarios.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGestionUsuarios.Implementacion
{
    public class ManageCRUDPermisos<T> : IManageCRUDPermisos<T> where T : class
    {
        public readonly ContextoDB_Permisos _context;

        public ManageCRUDPermisos(ContextoDB_Permisos context)
        {
            _context = context;
        }

        public void Add(T obj)
        {
            try
            {
                obj.GetType().GetProperty("FechaCreacion").SetValue(obj, DateTime.Now);
                obj.GetType().GetProperty("FechaModificacion").SetValue(obj, DateTime.Now);
                obj.GetType().GetProperty("UsuarioModificacion").SetValue(obj, obj.GetType().GetProperty("UsuarioCreacion").GetValue(obj, null));
                
            }
            catch (Exception exValidation)
            {
                
            }

            _context.AddAsync(obj);
        }

        public void Edit(T obj)
        {
            obj.GetType().GetProperty("FechaModificacion").SetValue(obj, DateTime.Now);
        }

        public void EditUltimoIngreso(Usuario obj)
        {
            obj.FechaUltimoIngreso = DateTime.Now;
        }



        public List<Modulo> EditRol(Rol objRol, List<Modulo> listaModulos)
        {
            try
            {
                objRol.FechaModificacion = DateTime.Now;
                _context.Modulos.RemoveRange(objRol.Modulos);
                _context.Modulos.AddRange(listaModulos);
                
                return listaModulos;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public void Delete(T obj)
        {
            _context.Remove(obj);
        }

        public void DeleteRange(List<UsuarioConjunto> listaUsuariosConjuntos)
        {
            _context.RemoveRange(listaUsuariosConjuntos);
        }
        public async Task<(bool estado, string mensajeError)> save()
        {
            try
            {
                var created = await _context.SaveChangesAsync();
                return (created > 0, string.Empty);
            }
            catch (Exception ex)
            {
                string mensajeError = "";
                if (ex.InnerException != null)
                    mensajeError = ex.InnerException.Message;
                else if(ex.Message!=null)
                    mensajeError = ex.Message;

                return (false, mensajeError);
            }
        }

        public async Task<(bool estado, string mensajeError)> saveRangeUsuarioConjunto(List<UsuarioConjunto> listaUsuariosConjuntos)
        {
            try
            {
                await _context.AddRangeAsync(listaUsuariosConjuntos);

                var created = await _context.SaveChangesAsync();

                return (created > 0, string.Empty);
            }
            catch (Exception ex)
            {
                string mensajeError = "";
                if (ex.InnerException != null)
                    mensajeError = ex.InnerException.Message;

                return (false, mensajeError);
            }
        }
    }
}
