using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Comunicado;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageAreaComunal : IManageAreaComunal
    {
        public readonly ContextoDB_Condominios _context;

        public ManageAreaComunal(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }


        public async Task<List<AreaComunal>> obtenerAvanzado(BusquedaAreaComunal objBusqueda)
        {
            List<AreaComunal> listaRepositorio = new List<AreaComunal>();

            if (objBusqueda.IdAreaComunal!=Guid.Empty && objBusqueda.IdAreaComunal != null)
            {
                listaRepositorio = await _context.AreaComunals
                             .Where(x => x.IdAreaComunal == objBusqueda.IdAreaComunal).ToListAsync(); 
            }
            else if (objBusqueda.IdConjunto != Guid.Empty && objBusqueda.IdConjunto != null)
            {
                listaRepositorio = await _context.AreaComunals
                             .Where(x => x.IdConjunto == objBusqueda.IdConjunto).ToListAsync();
            }
            else
            {
                listaRepositorio = await _context.AreaComunals.ToListAsync();
            }

            if (!string.IsNullOrEmpty(objBusqueda.NombreArea))
            {
                listaRepositorio = listaRepositorio
                    .Where(x => x.NombreArea.ToUpper().Trim().Contains(objBusqueda.NombreArea.ToUpper().Trim())).ToList();
            }

            if (!string.IsNullOrEmpty(objBusqueda.NombreArea))
            {
                listaRepositorio = listaRepositorio
                    .Where(x => x.NombreArea.ToUpper().Trim().Contains(objBusqueda.NombreArea.ToUpper().Trim())).ToList();
            }

            if(objBusqueda.HoraInicio != null && objBusqueda.HoraFin != null) {

                listaRepositorio = listaRepositorio.Where(area =>
                (!objBusqueda.HoraInicio.HasValue || area.HoraInicio >= objBusqueda.HoraInicio) &&
                        (!objBusqueda.HoraFin.HasValue || area.HoraFin <= objBusqueda.HoraFin)
                 ).ToList();
            }

            return listaRepositorio;
        }

        public async Task<AreaComunal> obtenerPorIDAreaComunal(Guid idAreaComunal)
        {
            try
            {
                AreaComunal objRepositorio = await _context.AreaComunals
                    .Where(x => x.IdAreaComunal == idAreaComunal).FirstOrDefaultAsync();

                return objRepositorio;
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
