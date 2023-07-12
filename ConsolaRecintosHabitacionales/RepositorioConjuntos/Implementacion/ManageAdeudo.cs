using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using Microsoft.EntityFrameworkCore;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Implementacion
{  

    public class ManageAdeudo : IManageAdeudo
    {
        public readonly ContextoDB_Condominios _context;

        public ManageAdeudo(ContextoDB_Condominios context)
        {
            _context = context;
        }

        public async Task<List<Adeudo>> obtenerAdeudosAvanzado(GenerarAdeudo objBusqueda)
        {
            try
            {
                List<Adeudo> listaAdeudos = new List<Adeudo>();

                //listaAdeudos = await _context.Adeudos.ToListAsync();

                listaAdeudos = await _context.Adeudos.Where(x => x.FechaAdeudos.ToString("yyyy-MM-dd") == ((DateTime)objBusqueda.fechaADeudoActual).ToString("yyyyy-MM-dd")
            ).Include(x => x.IdDepartamentoNavigation)
            .ThenInclude(x => x.IdTorresNavigation)
            .ToListAsync();

                listaAdeudos = await _context.Adeudos.Where(x => x.IdDepartamentoNavigation.IdTorresNavigation.IdConjunto == objBusqueda.IdConjunto
           ).Include(x => x.IdDepartamentoNavigation)
           .ThenInclude(x => x.IdTorresNavigation)
           .ToListAsync();


                //     listaAdeudos = await _context.Adeudos.Where(x => x.IdDepartamentoNavigation.IdTorresNavigation.IdConjunto == objBusqueda.IdConjunto
                //&& x.FechaAdeudos.ToString("MMyyyy") == ((DateTime)objBusqueda.fechaADeudoActual).ToString("MMyyyy")
                //).Include(x => x.IdDepartamentoNavigation)
                //.ThenInclude(x => x.IdTorresNavigation)
                //.ToListAsync();



                return listaAdeudos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
