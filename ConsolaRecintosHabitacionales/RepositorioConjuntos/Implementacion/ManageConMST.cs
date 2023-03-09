using ConjuntosEntidades.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageConMST : IManageConMST
    {
        public readonly ContextoDB_Condominios _context;

        public ManageConMST(ContextoDB_Condominios context)
        {
            _context = context;
        }

        public async Task<ConMst> obtenerPorIDConMST(Guid idConMST)
        {
            ConMst objRepositorio = await _context.ConMsts
                .Where(x => x.IdConMst == idConMST).FirstOrDefaultAsync();

            return objRepositorio;

        }

        public async Task<List<ConMst>> obtenerPorCuenta(string numeroCuenta)
        {
            List<ConMst> listaRepositorio = await _context.ConMsts
                .Where(x => x.CuentaCon.Contains(numeroCuenta)).ToListAsync();

            return listaRepositorio;
        }        

        public async Task<List<ConMst>> obtenerPorNombreCuenta(string nombre)
        {
            List<ConMst> listaRepositorio = await _context.ConMsts
                .Where(x => x.NombreCuenta.Contains(nombre)).ToListAsync();

            return listaRepositorio;
        }
    }
}
