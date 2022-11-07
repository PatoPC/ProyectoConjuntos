using ConjuntosEntidades.Entidades;
using DTOs.Torre;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;



namespace RepositorioConjuntos.Implementacion
{
    public class ManageTorre : IManageTorre
    {
        public readonly ContextoDB_Condominios _context;

        public ManageTorre(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

      
        public async Task<List<Torre>> busquedaAvanzada(BusquedaTorres objBusquedaTorreo)
        {
            try
            {
                var conjuntos = await _context.Torres.Where(x => x.IdConjunto==objBusquedaTorreo.IdConjunto).Include(x => x.Departamentos).ToListAsync();

               
                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<Torre> obtenerPorIDTorre(Guid idTorre)
        {
            try
            {
                var torre = await _context.Torres.Where(x => x.IdTorres == idTorre).FirstOrDefaultAsync();

                return torre;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Torre>> obtenerTorrePorNombre(string nombreTorre)
        {
            try
            {
                var torre = await _context.Torres.Where(x => x.NombreTorres.ToUpper().Trim().Contains(nombreTorre.ToUpper().Trim())).ToListAsync();

                return torre;
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
