using EntidadesPapelera.Entidades;
using Microsoft.EntityFrameworkCore;
using RepositorioPapelera.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitarios;

namespace RepositorioPapelera.Implementacion
{
    public class ManageConsultasPepelera : IManageConsultasPepelera
    {
        public readonly ContextoDB_Papelera _context;
        public ManageConsultasPepelera(ContextoDB_Papelera context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<List<DatosEliminado>> GetAllCatalogoPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<DatosEliminado> listaRepositorio = new List<DatosEliminado>();
            fechaFin = fechaFin.AddHours(23);
            fechaFin = fechaFin.AddMinutes(59);
            fechaFin = fechaFin.AddSeconds(59);

            listaRepositorio = await _context.DatosEliminados.Where(x => x.FechaEliminacion >= fechaInicio && x.FechaEliminacion <= fechaFin).ToListAsync();

            return listaRepositorio;
        }

        public async Task<DatosEliminado> GetCatalogoById(Guid idPapelera)
        {
            DatosEliminado objRepositorio = new DatosEliminado();

            objRepositorio = await _context.DatosEliminados.Where(x => x.IdDatosEliminados == idPapelera).FirstOrDefaultAsync();

            return objRepositorio;
        }
    }
}
