using ConjuntosEntidades.Entidades;
using DTOs.Parametro;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageParametro : IManageParametro
    {
        public readonly ContextoDB_Condominios _context;

        public ManageParametro(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public Task<Parametro> obtenerPorIDParametro(Guid idParametro)
        {
            var objRespositorio = _context.Parametros.Where(x => x.IdParametro == idParametro).FirstOrDefaultAsync();

            return objRespositorio;
        }

        public async Task<List<Parametro>> busquedaAvanzada(BusquedaParametro objBusqueda)
        {
            List<Parametro> listaRespositorio = new List<Parametro>();

            if (objBusqueda.IdParametro!=Guid.Empty)
            {
                listaRespositorio = await _context.Parametros.Where(x => x.IdConjunto == objBusqueda.IdConjunto).ToListAsync();
            }
            else if (objBusqueda.IdConjunto != Guid.Empty)
            {
                listaRespositorio = await _context.Parametros.Where(x => x.IdConjunto == objBusqueda.IdConjunto).ToListAsync();
            }

            if(!string.IsNullOrEmpty(objBusqueda.NombreParametro))
                listaRespositorio = listaRespositorio.Where(x => x.NombreParametro.ToUpper().Trim().Contains(objBusqueda.NombreParametro.ToUpper().Trim())).ToList();

            return listaRespositorio;
        }

        public async Task<Parametro> obtenerParametroPorIDCatalogo(Guid idModuloCatalogo)
        {
            var parametro = await _context.Parametros.Where(x => x.IdModulo == idModuloCatalogo).FirstOrDefaultAsync();

            return parametro;
        }
    }
}
