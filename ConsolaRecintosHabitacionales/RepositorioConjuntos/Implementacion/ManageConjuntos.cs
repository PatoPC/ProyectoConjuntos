using ConjuntosEntidades.Entidades;
using DTOs.Conjunto;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageConjuntos : IManageConjuntos
    {
        public readonly ContextoDB_Condominios _context;

        public ManageConjuntos(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Conjunto> obtenerPorIDConjuntos(Guid idCondominio)
        {
            try
            {
                var condominio = await _context.Conjuntos
                    .Where(x => x.IdConjunto == idCondominio)
                    .Include(x => x.Torres)
                    .ThenInclude(x => x.Departamentos).FirstOrDefaultAsync();

                return condominio;
            }
            catch (Exception ex)
            {

            }

            return default;

        }

        public async Task<List<Conjunto>> obtenerPorNombre(string nombreCondominio)
        {
            try
            {
                var conjuntos = await _context.Conjuntos.Where(x => x.NombreConjunto.Contains(nombreCondominio)).ToListAsync();

                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Conjunto>> obtenerPorRUC(string ruc)
        {
            try
            {
                var conjuntos = await _context.Conjuntos.Where(x => x.NombreConjunto.Contains(ruc)).ToListAsync();

                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Conjunto>> busquedaAvanzada(BusquedaConjuntos objBusqueda)
        {
            try
            {
                var conjuntos = await _context.Conjuntos.Include(x => x.Torres).ThenInclude(x => x.Departamentos).ToListAsync();

                if (!string.IsNullOrEmpty(objBusqueda.NombreConjunto))
                    conjuntos = conjuntos.Where(x => x.NombreConjunto.ToUpper().Trim().Contains(objBusqueda.NombreConjunto.ToUpper().Trim())).ToList();

                if (!string.IsNullOrEmpty(objBusqueda.RucConjunto))
                    conjuntos = conjuntos.Where(x => x.RucConjunto.Trim().Contains(objBusqueda.NombreConjunto.Trim())).ToList();

                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Conjunto>> busquedaTodosConjuntos()
        {
            try
            {
                var conjuntos = await _context.Conjuntos.ToListAsync();

                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

    }
}
