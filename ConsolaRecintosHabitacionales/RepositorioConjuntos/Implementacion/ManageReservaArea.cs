using ConjuntosEntidades.Entidades;
using DTOs.ReservaArea;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageReservaArea: IManageReservaArea
    {
        public readonly ContextoDB_Condominios _context;

        public ManageReservaArea(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }


        public async Task<List<ReservaArea>> obtenerAvanzado(BusquedaReservaAreaDTO objBusqueda)
        {
            List<ReservaArea> listaRepositorio = new List<ReservaArea>();

            if (objBusqueda.IdAreaComunal != Guid.Empty && objBusqueda.IdAreaComunal != null)
            {
                listaRepositorio = await _context.ReservaAreas
                             .Where(x => x.IdAreaComunal == objBusqueda.IdAreaComunal).ToListAsync();
            }
            else if (objBusqueda.IdPersona != Guid.Empty && objBusqueda.IdPersona != null)
            {
                listaRepositorio = await _context.ReservaAreas                    
                             .Where(x => x.IdPersona == objBusqueda.IdPersona).ToListAsync();
            }
            else
            {
                listaRepositorio = await _context.ReservaAreas.ToListAsync();
            }           

            return listaRepositorio;
        }

        public async Task<ReservaArea> obtenerPorIDReservaArea(Guid idReservaArea)
        {
            try
            {
                ReservaArea objRepositorio = await _context.ReservaAreas
                    .Where(x => x.IdReservaArea == idReservaArea).FirstOrDefaultAsync();

                return objRepositorio;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<ReservaArea>> obtenerReservaAreaPorIdArea(Guid idAreaComunal) 
        {
            try
            {
               List<ReservaArea> ListRepositorio = await _context.ReservaAreas
                    .Where(x => x.IdAreaComunal == idAreaComunal).ToListAsync();

                return ListRepositorio;
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
