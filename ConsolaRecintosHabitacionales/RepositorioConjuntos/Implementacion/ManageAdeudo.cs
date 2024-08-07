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
                if (objBusqueda.fechaADeudoActual == DateTime.MinValue && objBusqueda.IdConjunto == null)
                {
                    listaAdeudos = await _context.Adeudos
                    .Where(x => x.IdDepartamento == objBusqueda.IdDepartamento
                    && x.EstadoAdeudos== objBusqueda.EstadoPago)
                    .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .ToListAsync();
                }
                else if (objBusqueda.EstadoPago == null && objBusqueda.fechaADeudoActual == DateTime.MinValue)
                {
                    listaAdeudos = await _context.Adeudos
                    .Where(x => x.IdDepartamentoNavigation.IdTorresNavigation.IdConjunto == objBusqueda.IdConjunto)
                    .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .ToListAsync();
                }
                else if (objBusqueda.EstadoPago == null)
                {
                    listaAdeudos = await _context.Adeudos
                    .Where(x => x.IdDepartamentoNavigation.IdTorresNavigation.IdConjunto == objBusqueda.IdConjunto &&
                    x.FechaAdeudos == objBusqueda.fechaADeudoActual)
                    .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .ToListAsync();
                }
                else if(objBusqueda.fechaADeudoActual == DateTime.MinValue && objBusqueda.IdDepartamento != null )
                {
                    listaAdeudos = await _context.Adeudos.
                        Where(x => x.EstadoAdeudos == objBusqueda.EstadoPago
                        && x.IdDepartamento == objBusqueda.IdDepartamento)
                    .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .ToListAsync();
                }
                else
                {
                    listaAdeudos = await _context.Adeudos.
                        Where(x => x.EstadoAdeudos == objBusqueda.EstadoPago
                        && x.IdDepartamentoNavigation.IdTorresNavigation.IdConjunto == objBusqueda.IdConjunto 
                        && x.FechaAdeudos == objBusqueda.fechaADeudoActual)
                    .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .ToListAsync();
                }

                if (!string.IsNullOrEmpty(objBusqueda.numeroDepartamento))
                {
                    listaAdeudos = listaAdeudos.Where(x => x.IdDepartamentoNavigation.CodigoDepartamento.Trim() == objBusqueda.numeroDepartamento.Trim()).ToList();
                }

                if (objBusqueda.IdTorre != null)
                {
                    listaAdeudos = listaAdeudos.Where(x => x.IdDepartamentoNavigation.IdTorres == objBusqueda.IdTorre).ToList();
                }

                return listaAdeudos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<Adeudo> obtenerAdeudosAvanzado(Guid IdAdeudos)
        {
            var objAdeudo = await _context.Adeudos.Where(x => x.IdAdeudos == IdAdeudos)
                .Include(x => x.IdDepartamentoNavigation)
                    .ThenInclude(x => x.IdTorresNavigation)
                    .ThenInclude(x => x.IdConjuntoNavigation)
                    .Include(x => x.IdPersonaNavigation)
                    .Include(x => x.PagoAdeudos)
                .FirstOrDefaultAsync();

            return objAdeudo;
        }
    }
}
