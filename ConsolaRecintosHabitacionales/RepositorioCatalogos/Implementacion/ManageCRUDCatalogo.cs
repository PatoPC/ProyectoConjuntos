using EntidadesCatalogos.Entidades;
using RepositorioCatalogos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCatalogos.Implementacion
{
    public class ManageCRUDCatalogo<T> : IManageCRUDCatalogo<T> where T : class
    {
        public readonly ContextoDB_Catalogos _context;

        public ManageCRUDCatalogo(ContextoDB_Catalogos context)
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

            }
        }

        public void AddRange(T obj)
        {
            try
            {               
                _context.AddRangeAsync(obj);
            }
            catch (Exception exValidation)
            {

            }
        }

        public void Edit(T obj)
        {
            obj.GetType().GetProperty("FechaModificacion").SetValue(obj, DateTime.Now);
        }

        public void Delete(T obj)
        {
            _context.Remove(obj);
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
