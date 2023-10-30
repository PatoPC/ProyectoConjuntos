using ConjuntosEntidades.Entidades;
using DTOs.MaestroContable;
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

        public async Task<List<ConMst>> obtenerTodos()
        {
            List<ConMst> listaRepositorio = await _context.ConMsts.ToListAsync();

            return listaRepositorio;
        }

        public async Task<List<ConMst>> busquedaAvanzada(MaestroContableBusqueda objBusqueda)
        {
            List<ConMst> listaRepositorio = await _context.ConMsts.Where(x => x.IdConjunto == objBusqueda.IdConjunto).ToListAsync();

            if (!string.IsNullOrEmpty(objBusqueda.NombreCuenta))
            {
                listaRepositorio = listaRepositorio.Where(x => x.NombreCuenta.Trim().ToUpper().Contains(objBusqueda.NombreCuenta.Trim().ToUpper())).ToList();
            }

            if (!string.IsNullOrEmpty(objBusqueda.CuentaCon))
            {
                listaRepositorio = listaRepositorio
                    .Where(x => x.CuentaCon.Trim() == objBusqueda.CuentaCon.Trim()).ToList();
            }

            if (objBusqueda.Grupo != null)
            {
                listaRepositorio = listaRepositorio
                .Where(x => x.Grupo == (bool)objBusqueda.Grupo).ToList();
            }

            return listaRepositorio;
        }
    }
}
