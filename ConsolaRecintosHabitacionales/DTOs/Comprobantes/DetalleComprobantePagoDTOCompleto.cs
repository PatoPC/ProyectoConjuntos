using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Comprobantes
{
    public class DetalleComprobantePagoDTOCompleto
    {
        public Guid IdDetalleComprobante { get; set; }
        public Guid IdComprobantePago { get; set; }
        public Guid IdTablaDeuda { get; set; }
        public decimal ValorPendiente { get; set; }
        public DateTime FechaDetalleDeuda { get; set; }
    }
}
