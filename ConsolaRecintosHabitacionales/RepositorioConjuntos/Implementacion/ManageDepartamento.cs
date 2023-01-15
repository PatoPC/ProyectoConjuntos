using ConjuntosEntidades.Entidades;
using DTOs.Torre;
using RepositorioConjuntos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using DTOs.Departamento;

namespace RepositorioConjuntos.Implementacion
{
    public class ManageDepartamento : IManageDepartamento
    {
        public readonly ContextoDB_Condominios _context;

        public ManageDepartamento(ContextoDB_Condominios context)
        {
            this._context = context ?? throw new ArgumentException(nameof(context));
        }
     
        public async Task<List<Departamento>> busquedaAvanzadaDepartamento(DepartamentoBusquedaDTO objBusquedaDepartamento)
        {
            try
            {
                var listaRepositorio = await _context.Departamentos.Where(x => x.IdTorres == objBusquedaDepartamento.IdTorres).ToListAsync();

                if(!string.IsNullOrEmpty(objBusquedaDepartamento.CoigoDepto))
                    listaRepositorio = listaRepositorio.Where(x => x.CodigoDepartamento == objBusquedaDepartamento.CoigoDepto).ToList();

                if(objBusquedaDepartamento.IdDepto!=Guid.NewGuid())
                    listaRepositorio = listaRepositorio.Where(x => x.IdDepartamento==objBusquedaDepartamento.IdDepto).ToList();


                return listaRepositorio;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Departamento>> obtenerDepartamentoPorNombre(string nombreDepartamento)
        {
            throw new NotImplementedException();
        }

        public async Task<Departamento> obtenerPorIDDepartamento(Guid idDepartamento)
        {
            var objRepositorio = await _context.Departamentos
                .Where(x => x.IdDepartamento == idDepartamento)
                .Include(x => x.AreasDepartamentos)
                .Include(x => x.TipoPersonas)
                .ThenInclude(x => x.IdPersonaNavigation)
                .FirstOrDefaultAsync();

            return objRepositorio;
        }

        public async Task<List<Departamento>> obtenerPorDeparta_IDTorre(Guid idTorre)
        {
            var objRepositorio = await _context.Departamentos.Where(x => x.IdTorres == idTorre).ToListAsync();

            return objRepositorio;
        }
    }
}
