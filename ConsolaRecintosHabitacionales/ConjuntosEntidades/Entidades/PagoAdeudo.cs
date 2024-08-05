using System;
using System.Collections.Generic;

namespace ConjuntosEntidades.Entidades
{
    public partial class PagoAdeudo
    {
        public Guid IdPagoAdeudo { get; set; }
        public Guid IdAdeudos { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal ValorPago { get; set; }
        public Guid IdTipoPago { get; set; }
        public string? Observacion { get; set; }
        public bool EstadoImpreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public virtual Adeudo IdAdeudosNavigation { get; set; } = null!;
    }
}
