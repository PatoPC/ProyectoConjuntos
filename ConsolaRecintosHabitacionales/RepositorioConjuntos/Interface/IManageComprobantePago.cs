using ConjuntosEntidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageComprobantePago
    {
        public Task<ComprobantePago> obtenerComprobanteID(Guid IdComprobantePago);        

        
    }
}
