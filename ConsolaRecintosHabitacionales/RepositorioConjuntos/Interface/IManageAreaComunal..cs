using ConjuntosEntidades.Entidades;
using DTOs.AreaComunal;
using DTOs.Comunicado;
using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageAreaComunal
    {
        public Task<AreaComunal> obtenerPorIDAreaComunal(Guid idAreaComunal);
        public Task<List<AreaComunal>> obtenerAvanzado(BusquedaAreaComunal objBusqueda);
        public  Task<List<AreaComunal>> obtenerAreasComunalesPorIdConjunto(Guid IdConjunto);
    }
}
