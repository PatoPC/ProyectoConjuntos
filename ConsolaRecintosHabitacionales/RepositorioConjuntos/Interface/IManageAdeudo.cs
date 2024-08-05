using ConjuntosEntidades.Entidades;
using DTOs.Adeudo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageAdeudo
    {
        public Task<List<Adeudo>> obtenerAdeudosAvanzado(GenerarAdeudo objBusqueda);
        public Task<Adeudo> obtenerAdeudosAvanzado(Guid IdAdeudos);
    }
}
