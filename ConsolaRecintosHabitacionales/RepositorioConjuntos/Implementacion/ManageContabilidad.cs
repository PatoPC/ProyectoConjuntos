using ConjuntosEntidades.Entidades;
using DTOs.Contabilidad;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageContabilidad : IManageContabilidad
    {
        public readonly ContextoDB_Condominios _context;

        public ManageContabilidad(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<EncabezadoContabilidad> EncabezadoContabilidadPorID(Guid IdEncCont)
        {
            EncabezadoContabilidad objRepositorio = await _context.EncabezadoContabilidads.Where(x => x.IdEncCont == IdEncCont).FirstOrDefaultAsync();

            return objRepositorio;
        }

        public int GetSecuencialMaximoCabecera()
        {
            try
            {
                int objAnexo = (int)_context.SecuencialCabeceraConts.Max(x => x.Secuencial);

                return objAnexo;
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public async Task<List<EncabezadoContabilidad>> GetBusquedaAvanzadaContabilidad(BusquedaContabilidad objBusqueda)
        {
            List<EncabezadoContabilidad> objRepositorio = new List<EncabezadoContabilidad>();

            if(objBusqueda.TipoDocNEncCont != Guid.Empty )
            {
                objRepositorio = await _context.EncabezadoContabilidads.Where(x => x.TipoDocNEncCont == objBusqueda.TipoDocNEncCont && x.IdConjunto == objBusqueda.IdConjunto)
					.Include(x => x.SecuencialCabeceraConts).ToListAsync();

                DateTime fechaFinal = ((DateTime)objBusqueda.FechaFinEncCont).AddHours(23);

                fechaFinal = fechaFinal.AddMinutes(59);
                fechaFinal = fechaFinal.AddSeconds(59);

                objRepositorio = await _context.EncabezadoContabilidads.Where(x => x.FechaEncCont >= objBusqueda.FechaInicioEncCont && x.FechaEncCont <= fechaFinal).ToListAsync();
            }
            else if(objBusqueda.FechaInicioEncCont != null && objBusqueda.FechaFinEncCont != null && objBusqueda.FechaInicioEncCont != DateTime.MinValue && objBusqueda.FechaFinEncCont != DateTime.MinValue)
            {
                DateTime fechaFinal = ((DateTime)objBusqueda.FechaFinEncCont).AddHours(23);

                fechaFinal = fechaFinal.AddMinutes(59);
                fechaFinal = fechaFinal.AddSeconds(59);

                objRepositorio = await _context.EncabezadoContabilidads.Where(x => x.FechaEncCont >= objBusqueda.FechaInicioEncCont && x.FechaEncCont <= fechaFinal && x.IdConjunto == objBusqueda.IdConjunto)
                    .Include(x => x.SecuencialCabeceraConts)
                    .ToListAsync();

            }

            if (objBusqueda.NumeroComprobante != null)
            {
                var secuencial = await _context.SecuencialCabeceraConts
                    .Where(y => y.Secuencial == ((int)objBusqueda.NumeroComprobante)).FirstOrDefaultAsync();

                objRepositorio = await _context.EncabezadoContabilidads.Where(x => x.IdEncCont == secuencial.IdEncCont 
                 && x.IdConjunto == objBusqueda.IdConjunto)
					.Include(x => x.SecuencialCabeceraConts).ToListAsync();
            }



            return objRepositorio;
        }


    }
}
