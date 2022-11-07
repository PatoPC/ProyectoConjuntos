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
                var conjuntos = await _context.Departamentos.Where(x => x.IdDepto == objBusquedaDepartamento.IdDepto).ToListAsync();

                return conjuntos;
            }
            catch (Exception ex)
            {

            }

            return default;
        }

        public async Task<List<Departamento>> obtenerDepartamentoPorNombre(string nombreDepartamento)
        {
            try
            {
                var departamento = await _context.Departamentos.Where(x => x.CoigoDepto.ToUpper().Trim().Contains(nombreDepartamento.ToUpper().Trim())).ToListAsync();

                return departamento;
            }
            catch (Exception ex)
            {
            }

            return default;
        }

        public async Task<Departamento> obtenerPorIDDepartamento(Guid idDepartamento)
        {
            try
            {
                var departamento = await _context.Departamentos.Where(x => x.IdDepto == idDepartamento).FirstOrDefaultAsync();

                return departamento;
            }
            catch (Exception ex)
            {

            }

            return default;
        }
    }
}
