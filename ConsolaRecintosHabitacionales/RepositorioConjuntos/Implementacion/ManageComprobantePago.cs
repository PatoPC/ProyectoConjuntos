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
    public class ManageComprobantePago : IManageComprobantePago
    {
        public readonly ContextoDB_Condominios _context;

        public ManageComprobantePago(ContextoDB_Condominios context)
        {
            _context = context;
        }

        public async Task<ComprobantePago> obtenerComprobanteID(Guid IdComprobantePago)
        {
            ComprobantePago objComprobante = await _context.ComprobantePagos.Where(x => x.IdComprobantePago == IdComprobantePago).FirstOrDefaultAsync();

            return objComprobante;
        }
    }
}
