using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class SecuencialComprobantePago
    {
        public Guid IdSecuencialComprobantePago { get; set; }
        public Guid IdComprobantePago { get; set; }
        public int SecuencialComprobante { get; set; }

        public virtual ComprobantePago IdComprobantePagoNavigation { get; set; } = null!;
    }
}
