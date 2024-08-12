using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comprobantes
{
    public class SecuencialComprobantePagoDTO
    {
        public Guid IdSecuencialComprobantePago { get; set; }
        public Guid IdComprobantePago { get; set; }
        public int SecuencialComprobante { get; set; }
    }
}
