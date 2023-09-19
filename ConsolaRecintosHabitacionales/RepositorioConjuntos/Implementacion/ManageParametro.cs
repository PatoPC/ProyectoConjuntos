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
    }
}
