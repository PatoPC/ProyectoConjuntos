using ConjuntosEntidades.Entidades;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageConjuntosCRUD<T> : IManageConjuntosCRUD<T> where T : class
    {
        public readonly ContextoDB_Condominios _context;

        public ManageConjuntosCRUD(ContextoDB_Condominios context)
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
                _context.AddAsync(obj);
            }
            catch (Exception exValidation)
            {
                try
                {
                    _context.AddAsync(obj);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async Task<(bool estado, string mensajeError)> saveRangeConjunto(List<Conjunto> listaConjuntos)
        {
            try
            {
                await _context.AddRangeAsync(listaConjuntos);

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
        public async Task<(bool estado, string mensajeError)> saveRangeMaestro(List<ConMst> listaMaestro)        
        {
            try
            {
                await _context.AddRangeAsync(listaMaestro);

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

        public void Edit(T obj)
        {
            try
            {
                obj.GetType().GetProperty("FechaModificacion").SetValue(obj, DateTime.Now);
            }
            catch (Exception ex)
            {


            }
        }
        //public void DeleteRango(List<AreasDepartamento> lista)
        //{
        //    _context.RemoveRange(lista);
        //}

        public void Delete(T obj)
        {
            _context.Remove(obj);
        }
        public async Task<(bool estado, string mensajeError)> DeleteRange(List<Conjunto> lista)
        {
            try
            {
                _context.RemoveRange(lista);

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
        

        public async Task<(bool estado, string mensajeError)> DeleteRange(List<Torre> lista)
        {
            try
            {
                _context.RemoveRange(lista);

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

        public async Task<(bool estado, string mensajeError)> DeleteRange(List<Departamento> lista)
        {
            try
            {
                _context.RemoveRange(lista);

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

        public async Task<(bool estado, string mensajeError)> DeleteRange(List<AreasDepartamento> lista)
        {
            try
            {
                _context.RemoveRange(lista);

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

                return (false, mensajeError);
            }
        }
    }
}
