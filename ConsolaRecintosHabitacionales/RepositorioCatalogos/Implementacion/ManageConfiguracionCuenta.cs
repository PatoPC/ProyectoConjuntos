using EntidadesCatalogos.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioCatalogos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioCatalogos.Implementacion
{
    public class ManageConfiguracionCuenta : IManageConfiguracionCuenta
    {
        public readonly ContextoDB_Catalogos _context;

        public ManageConfiguracionCuenta(ContextoDB_Catalogos context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Configuracioncuentum> GetConfiguracionCuenta(Guid idConfiguracionCuenta)
        {
            var objRepositorio = 
                await _context.Configuracioncuenta
                .Where(x => x.IdConfiguracionCuenta == idConfiguracionCuenta).FirstOrDefaultAsync();

            return objRepositorio;
        }

        public async Task<Configuracioncuentum> GetConfigCuentaConjunto(Guid idConjunto)
        {
            var objRepositorio = 
                await _context.Configuracioncuenta
                .Where(x => x.IdConjunto == idConjunto).FirstOrDefaultAsync();

            return objRepositorio;
        }

    }
}
