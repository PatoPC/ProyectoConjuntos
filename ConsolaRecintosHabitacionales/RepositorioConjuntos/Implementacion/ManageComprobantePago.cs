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

        public async Task<ComprobantePago> obtenerComprobanteIDDetalle(Guid IdAdeudo)
        {
            //DetalleComprobantePago objDetalle = await _context.DetalleComprobantePagos.Where(x => x.IdTablaDeuda == IdAdeudo).FirstOrDefaultAsync();

            //ComprobantePago objComprobanteTemp = await _context.ComprobantePagos
            //    .Where(x => x.IdComprobantePago == objDetalle.IdComprobantePago)
            //    .Include(x => x.DetalleComprobantePagos).FirstOrDefaultAsync();

            //ComprobantePago objComprobanteTemp2 = await _context.ComprobantePagos
            // .Where(x => x.DetalleComprobantePagos.Where( y => y.IdTablaDeuda == IdAdeudo).Count()>0)
            // .Include(x => x.DetalleComprobantePagos).FirstOrDefaultAsync();

            ComprobantePago objComprobante = await _context.ComprobantePagos
             .Include(x => x.DetalleComprobantePagos)
             .Include(x => x.SecuencialComprobantePagos)
             .FirstOrDefaultAsync(x => x.DetalleComprobantePagos.Any(y => y.IdTablaDeuda == IdAdeudo));



            return objComprobante;
        }

        public int GetSecuencialComprobantePago()
        {
            try
            {
                int objAnexo = (int)_context.SecuencialComprobantePagos.Max(x => x.SecuencialComprobante);

                return objAnexo;
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
    }
}
