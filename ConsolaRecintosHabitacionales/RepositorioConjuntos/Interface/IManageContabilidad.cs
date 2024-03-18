using ConjuntosEntidades.Entidades;
using DTOs.Contabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioConjuntos.Interface
{
    public interface IManageContabilidad
    {
        public Task<EncabezadoContabilidad> EncabezadoContabilidadPorID(Guid IdEncCont);
        public int GetSecuencialMaximoCabecera();
        public Task<List<EncabezadoContabilidad>> GetBusquedaAvanzadaContabilidad(BusquedaContabilidad objBusqueda);


    }
}
