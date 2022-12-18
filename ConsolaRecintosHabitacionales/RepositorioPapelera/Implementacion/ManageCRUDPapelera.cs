
using EntidadesPapelera.Entidades;
using RepositorioPapelera.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioPapelera.Implementacion
{
    public class ManageCRUDPapelera<T> : IManageCRUDPapelera<T> where T : class
    {
        public readonly ContextoDB_Papelera _context;

        public ManageCRUDPapelera(ContextoDB_Papelera context)
        {
            _context = context;
        }

        public void Add(T obj)
        {
            try
            {
                obj.GetType().GetProperty("FechaEliminacion").SetValue(obj, DateTime.Now);
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
