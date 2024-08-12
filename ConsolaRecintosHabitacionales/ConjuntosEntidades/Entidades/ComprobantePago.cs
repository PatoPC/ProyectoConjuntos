using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class ComprobantePago
    {
        public ComprobantePago()
        {
            DetalleComprobantePagos = new HashSet<DetalleComprobantePago>();
            SecuencialComprobantePagos = new HashSet<SecuencialComprobantePago>();
        }

        public Guid IdComprobantePago { get; set; }
        public Guid IdTipoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal ValorPago { get; set; }
        public string UrlConsumaTablaDeuda { get; set; } = null!;
        public string Concepto { get; set; } = null!;
        public string? Observacion { get; set; }
        public bool EstadoImpreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public virtual ICollection<DetalleComprobantePago> DetalleComprobantePagos { get; set; }
        public virtual ICollection<SecuencialComprobantePago> SecuencialComprobantePagos { get; set; }
    }
}
