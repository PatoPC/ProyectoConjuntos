using ConjuntosEntidades.Entidades;
using DTOs.Departamento;
using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageDepartamento
    {
        public Task<Departamento> obtenerPorIDDepartamento(Guid idDepartamento);
        public Task<List<Departamento>> obtenerDepartamentoPorNombre(string nombreDepartamento);    
        public Task<List<Departamento>> busquedaAvanzadaDepartamento(DepartamentoBusquedaDTO objBusquedaDepartamento);
    }
}
