using ConjuntosEntidades.Entidades;
using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageTorre
    {
        public Task<Torre> obtenerPorIDTorre(Guid idTorre);
        public Task<List<Torre>> obtenerTorrePorNombre(string nombreTorre);    
        public Task<List<Torre>> busquedaAvanzada(BusquedaTorres objBusquedaTorreo);
    }
}
