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
    }
}
