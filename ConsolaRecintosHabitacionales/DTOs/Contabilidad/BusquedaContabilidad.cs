using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Contabilidad
{
    public class BusquedaContabilidad
    {
        public int? NumeroComprobante { set; get; } = null;
        public Guid? IdConjunto { set; get; } = Guid.Empty;
        public Guid? TipoDocNEncCont { get; set; } = Guid.Empty;
        public DateTime? FechaInicioEncCont { get; set; } 
        public DateTime? FechaFinEncCont { get; set; }
    }
}
