using ConjuntosEntidades.Entidades;
using DTOs.Comunicado;
using DTOs.Torre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageComunicado
    {
        public Task<Comunicado> obtenerPorIDComunicado(Guid idComunicado);
        public Task<List<Comunicado>> obtenerPorIDComunicado(BusquedaComunicadoDTO objBusqueda);
    }
}
