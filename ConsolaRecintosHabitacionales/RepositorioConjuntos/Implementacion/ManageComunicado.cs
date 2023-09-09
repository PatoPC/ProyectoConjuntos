using ConjuntosEntidades.Entidades;
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
    public class ManageComunicado : IManageComunicado
    {
        public readonly ContextoDB_Condominios _context;

        public ManageComunicado(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

       
        public async Task<Comunicado> obtenerPorIDComunicado(Guid idComunicado)
        {
            try
            {
                Comunicado objRepositorio = await _context.Comunicados
                    .Where(x => x.IdComunicado == idComunicado).FirstOrDefaultAsync();

                return objRepositorio;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Comunicado>> obtenerAvanzado(BusquedaComunicadoDTO objBusqueda)
        {
            List<Comunicado> objRepositorio = new List<Comunicado>();

            if (objBusqueda.IdConjunto!=Guid.Empty)
            {
                objRepositorio = await _context.Comunicados
                           .Where(x => x.IdConjunto == objBusqueda.IdConjunto).ToListAsync(); 
            }
            else
            {
                objRepositorio = await _context.Comunicados.ToListAsync();
            }

            if (!string.IsNullOrEmpty(objBusqueda.Titulo))
            {
                objRepositorio = objRepositorio.Where(x => x.Titulo.Contains(objBusqueda.Titulo)).ToList();  
            }

            return objRepositorio;
        }
    }
}
