using ConjuntosEntidades.Entidades;
using DTOs.ReservaArea;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageReservaArea
    {
        public Task<ReservaArea> obtenerPorIDReservaArea(Guid idReservaArea);
        public Task<List<ReservaArea>> obtenerAvanzado(BusquedaReservaAreaDTO objBusqueda);
        public  Task<List<ReservaArea>> obtenerReservaAreaPorIdArea(Guid idAreaComunal);
    }
}
