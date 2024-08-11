using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    /// <summary>
    /// ID_TABLA_DEUDA -&gt; esta pensada para colocar el ID de la tabla que se esta pagando, ejemplo para Pagar los adeudos, se coloca el ID_ADEUDO que es en donde esta la deuda o el monto a pagar. 
    /// </summary>
    public partial class DetalleComprobantePago
    {
        public Guid IdDetalleComprobante { get; set; }
        public Guid IdComprobantePago { get; set; }
        public Guid IdTablaDeuda { get; set; }
        public decimal ValorPendiente { get; set; }

        public virtual ComprobantePago IdComprobantePagoNavigation { get; set; } = null!;
    }
}
